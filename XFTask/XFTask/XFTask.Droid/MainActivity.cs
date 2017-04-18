using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Prism.Unity;
using Microsoft.Practices.Unity;
using Plugin.Permissions;
using ImageCircle.Forms.Plugin.Droid;
using Gcm.Client;
using XFTask.Droid.Infrastructure;
using Acr.UserDialogs;

[assembly: UsesFeature("android.hardware.location", Required = false)]
[assembly: UsesFeature("android.hardware.location.gps", Required = false)]
[assembly: UsesFeature("android.hardware.location.network", Required = false)]
[assembly: UsesFeature("android.hardware.camera", Required = false)]
[assembly: UsesFeature("android.hardware.camera.autofocus", Required = false)]
// 底下的用法，可以參考 https://developer.xamarin.com/releases/android/mono_for_android_4/mono_for_android_4.0.0/
[assembly: UsesPermission(Name = Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission(Name = Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesPermission(Name = Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Name = Android.Manifest.Permission.ReadExternalStorage)]
[assembly: UsesPermission(Name = Android.Manifest.Permission.WriteExternalStorage)]
[assembly: UsesPermission(Android.Manifest.Permission.Flashlight)]
[assembly: UsesPermission(Android.Manifest.Permission.Camera)]
namespace XFTask.Droid
{
    [Activity(Label = "派工與回報", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        #region MainActivity Instance
        // Create a new instance field for this activity.
        static MainActivity instance = null;

        // Return the current activity instance.
        public static MainActivity CurrentActivity
        {
            get
            {
                return instance;
            }
        }

        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            #region 設定該應用程式的主要 Activity
            instance = this;
            #endregion

            global::Xamarin.Forms.Forms.Init(this, bundle);

            #region 第三方套件／插件的初始化
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            var rendererAssemblies = new[] { typeof(ImageCircleRenderer) };
            UserDialogs.Init(this);
            #endregion

            #region 進行 Azure Mobile Client 套件初始化
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            #endregion

            LoadApplication(new App(new AndroidInitializer()));

            #region Firebase 的推播設定用程式碼
            try
            {
                // 確定 GcmClinet 的需求都有設定完成
                GcmClient.CheckDevice(this);
                GcmClient.CheckManifest(this);

                // 進行遠端推播通知的註冊(含註冊 Azure Mobile App)
                System.Diagnostics.Debug.WriteLine("Registering...");
                GcmClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);


            }
            catch (Java.Net.MalformedURLException)
            {
                CreateAndShowDialog("There was an error creating the client. Verify the URL.", "Error");
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e.Message, "Error");
            }
            #endregion
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #region Firebase 的推播設定用程式碼
        private void CreateAndShowDialog(String message, String title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }
        #endregion

    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}

