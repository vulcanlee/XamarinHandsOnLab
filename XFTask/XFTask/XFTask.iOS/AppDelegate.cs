using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Prism.Unity;
using Microsoft.Practices.Unity;
using ImageCircle.Forms.Plugin.iOS;
using XFTask.Helpers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MobileServices;
using AudioToolbox;

namespace XFTask.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            #region 第三方套件／插件的初始化
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            // Initialize the Azure Mobile Client SDK
            // http://stackoverflow.com/questions/24521355/azure-mobile-services-invalid-operation-exception-platform-specific-assembly-n
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            var rendererAssemblies = new[] { typeof(ImageCircleRenderer) };

            //Console.WriteLine(PCLGlobalHelper.BaseAPIUrl);
            #endregion

            global::Xamarin.Forms.Forms.Init();

            #region 要使用者允許接收通知之設定
            //  系統版本是否大於或等於指定的主要和次要值.
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
                );

                app.RegisterUserNotificationSettings(notificationSettings);
                // 這能夠支援遠端通知，並要求推播註冊
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
            }
            #endregion

            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        /// <summary>
        /// 這台裝置與 APNS 註冊完成後，會執行底下方法，需要將 deviceToken 送到 NotificationHub 推播中樞 來進行 Azure Notificatio Hub 的註冊
        /// </summary>
        /// <param name="application"></param>
        /// <param name="deviceToken"></param>
        public override async void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            #region 建立要接收的推播格式
            string templateBodyAPNS = "{\"aps\":{\"alert\":\"$(messageParam)\"}}";

            JObject templates = new JObject();
            templates["genericMessage"] = new JObject
            {
                { "body", templateBodyAPNS}
            };
            #endregion

            #region 註冊新的 DeviceToken 到 Azure 推播中樞內
            #region 取得現在的 DeviceToken
            var DeviceToken = deviceToken.Description;
            if (!string.IsNullOrWhiteSpace(DeviceToken))
            {
                DeviceToken = DeviceToken.Trim('<').Trim('>');
                DeviceToken = DeviceToken.Replace(" ", String.Empty);
            }
            #endregion

            #region 取得先前的 DeviceToken
            // https://developer.apple.com/reference/foundation/nsuserdefaults/1416603-standarduserdefaults
            var oldDeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey("PushDeviceToken");
            NSUserDefaults.StandardUserDefaults.SetString(DeviceToken, "PushDeviceToken");
            #endregion

            #region DeviceToken 是否有變動過
            if (string.IsNullOrEmpty(oldDeviceToken) || !oldDeviceToken.Equals(DeviceToken))
            {
                // 在這裡可以處理當DeviceToken沒有異動時，無須再進行註冊
                //Push push = GlobalHelper.AzureMobileClient.GetPush();
                //await push.RegisterAsync(deviceToken, templates);
            }
            Push push = PCLGlobalHelper.AzureMobileClient.GetPush();
            await push.RegisterAsync(deviceToken, templates);
            #endregion
            #endregion
        }

        /// <summary>
        /// 若與 APNS 註冊失敗，則會呼叫這個方法
        /// </summary>
        /// <param name="application"></param>
        /// <param name="error"></param>
        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            var alert = new UIAlertView("警告", "註冊 APNS 失敗:" + error.ToString(), null, "OK", null);
            alert.Show();
        }

        /// <summary>
        /// 當應用程式執行時，此方法會處理傳入的通知
        /// </summary>
        /// <param name="application"></param>
        /// <param name="userInfo"></param>
        /// <param name="completionHandler"></param>
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            #region 取出 aps 推播內容，進行處理
            try
            {
                if (userInfo.ContainsKey(new NSString("aps")))
                {
                    try
                    {
                        NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as NSDictionary;

                        #region 取出相關推播通知的 Payload
                        string alert = string.Empty;
                        if (aps.ContainsKey(new NSString("alert")))
                            alert = (aps[new NSString("alert")] as NSString).ToString();
                        #endregion

                        #region 因為應用程式正在前景，所以，顯示一個提示訊息對話窗
                        if (!string.IsNullOrEmpty(alert))
                        {
                            SystemSound.Vibrate.PlaySystemSound();
                            UIAlertView avAlert = new UIAlertView("Notification", alert, null, "OK", null);
                            avAlert.Show();
                        }
                        #endregion
                    }
                    catch { }
                }
            }
            catch { }

            completionHandler(UIBackgroundFetchResult.NoData);

            #endregion
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }

}
