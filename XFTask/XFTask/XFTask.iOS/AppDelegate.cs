using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Prism.Unity;
using Microsoft.Practices.Unity;
using ImageCircle.Forms.Plugin.iOS;
using XFTask.Helpers;

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
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }

}
