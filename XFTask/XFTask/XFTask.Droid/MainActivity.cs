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

[assembly: UsesFeature("android.hardware.location", Required = false)]
[assembly: UsesFeature("android.hardware.location.gps", Required = false)]
[assembly: UsesFeature("android.hardware.location.network", Required = false)]
// 底下的用法，可以參考 https://developer.xamarin.com/releases/android/mono_for_android_4/mono_for_android_4.0.0/
[assembly: UsesPermission(Name = Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission(Name = Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesPermission(Name = Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Name = Android.Manifest.Permission.ReadExternalStorage)]
[assembly: UsesPermission(Name = Android.Manifest.Permission.WriteExternalStorage)]

namespace XFTask.Droid
{
    [Activity(Label = "XFTask", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}

