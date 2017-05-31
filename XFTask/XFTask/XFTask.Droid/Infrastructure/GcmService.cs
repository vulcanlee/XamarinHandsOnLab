using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gcm.Client;
using XFTask.Helpers;
using Android.Util;
using Microsoft.WindowsAzure.MobileServices;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using XFTask.Models;
using Newtonsoft.Json;
using Android.Media;

namespace XFTask.Droid.Infrastructure
{
    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]
    public class PushHandlerBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
        // 這裡要填入 Google Console 內的 專案編號
        public static string[] SENDER_IDS = new string[] { "359571220039" };
    }

    [Service]
    public class GcmService : GcmServiceBase
    {
        public static string RegistrationID { get; private set; }

        public GcmService()
            : base(PushHandlerBroadcastReceiver.SENDER_IDS) { }

        /// <summary>
        /// 當 App 取得遠端推播 PNS Token 後，要執行的委派事件
        /// </summary>
        /// <param name="context"></param>
        /// <param name="registrationId">這個參數，是PNS提供的一個唯一推播識別代碼</param>
        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose("PushHandlerBroadcastReceiver", "GCM Registered: " + registrationId);
            RegistrationID = registrationId;

            #region 進行 Azure Mobile 服務註冊
            // 取得 Azure Mobile Client 物件
            var push = PCLGlobalHelper.AzureMobileClient.GetPush();

            // 使用主執行續，進行 Azure Mobile 註冊
            MainActivity.CurrentActivity.RunOnUiThread(() => Register(push, null));
            #endregion
        }

        /// <summary>
        /// 進行Azure 推播中樞註冊
        /// </summary>
        /// <param name="push"></param>
        /// <param name="tags"></param>
        public async void Register(Microsoft.WindowsAzure.MobileServices.Push push, IEnumerable<string> tags)
        {
            #region 進行Azure 推播中樞註冊
            try
            {
                // 設定要接收來自於 Azure 推播中樞的推播訊息格式
                string templateBodyGCM = "{\"data\":{\"message\":\"$(messageParam)\", \"title\":\"$(titleParam)\", \"args\":\"$(argsParam)\"}}"; ;

                JObject templates = new JObject();
                templates["genericMessage"] = new JObject
                {
                    { "body", templateBodyGCM}
                };

                // 註冊要接收的推播訊息格式
                await push.RegisterAsync(RegistrationID, templates);
                Log.Info("Push Installation Id", push.InstallationId.ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                //Debugger.Break();
            }
            #endregion
        }

        /// <summary>
        /// 當推播訊息抵達的時候，要處理的委派事件
        /// </summary>
        /// <param name="context"></param>
        /// <param name="intent"></param>
        protected override void OnMessage(Context context, Intent intent)
        {
            #region 當推播通知訊息接收到之後，接下來要處理的工作
            Log.Info("PushHandlerBroadcastReceiver", "GCM Message Received!");

            #region 建立本地訊息通知物件
            string message = intent.Extras.GetString("message");
            string title = intent.Extras.GetString("title");
            string args = intent.Extras.GetString("args");
            if (!string.IsNullOrEmpty(message))
            {
                var fooNotificationPayload = new NotificationPayload()
                {
                    Page = args,
                    Title = title,
                    Message = message,
                };
                createNotification(fooNotificationPayload);
            }
            #endregion
            #endregion
        }

        /// <summary>
        /// 產生本地端的推播通知
        /// </summary>
        /// <param name="notificationPayload"></param>
        void createNotification(NotificationPayload notificationPayload)
        {
            #region 產生本地端的推播通知

            #region 產生一個 Intent ，並且將要傳遞過去的推播參數，使用 PutExtra 放進去
            var uiIntent = new Intent(this, typeof(MainActivity));
            // 設定當使用點選這個通知之後，要傳遞過去的資料
            var fooPayload = JsonConvert.SerializeObject(notificationPayload);
            uiIntent.PutExtra("NotificationPayload", fooPayload);
            #endregion

            //建立 Notification Builder 物件
            Notification.Builder builder = new Notification.Builder(this);

            //設定本地端推播的內容
            var notification = builder
                // 設定當點選這個本地端通知項目後，要顯示的 Activity
                .SetContentIntent(PendingIntent.GetActivity(this, 0, uiIntent, PendingIntentFlags.OneShot))
                .SetSmallIcon(Android.Resource.Drawable.SymActionEmail)
                .SetTicker(notificationPayload.Title)
                .SetContentTitle(notificationPayload.Title)
                .SetContentText(notificationPayload.Message)
                //設定推播聲音
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                //當使用者點選這個推播通知，將會自動移除這個通知
                .SetAutoCancel(true).Build();

            //產生 notification 物件
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            // 顯示本地通知:
            notificationManager.Notify(1, notification);
            #endregion

        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Error("PushHandlerBroadcastReceiver", "Unregistered RegisterationId : " + registrationId);
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error("PushHandlerBroadcastReceiver", "GCM Error: " + errorId);
        }
    }
}