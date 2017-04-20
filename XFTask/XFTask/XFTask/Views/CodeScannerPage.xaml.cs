using System.Collections.Generic;
using Xamarin.Forms;
using XFTask.Models;
using XFTask.ViewModels;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace XFTask.Views
{
    public partial class CodeScannerPage : ContentPage
    {
        // 本頁面的檢視模型
        CodeScannerPageViewModel foo本頁面的檢視模型;
        //https://components.xamarin.com/gettingstarted/zxing.net.mobile.forms
        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;

        public CodeScannerPage()
        {
            InitializeComponent();

            // 取得這個頁面的檢視模型(ViewModel)物件
            foo本頁面的檢視模型 = this.BindingContext as CodeScannerPageViewModel;
            #region 指定要掃描的條碼類型
            var fooMobileBarcodeScanningOptions = new MobileBarcodeScanningOptions();
            fooMobileBarcodeScanningOptions.PossibleFormats = new List<ZXing.BarcodeFormat>(){
                BarcodeFormat.QR_CODE,
                BarcodeFormat.CODE_128,
                BarcodeFormat.CODE_39,
                BarcodeFormat.CODABAR
            };
            #endregion
  
            #region 建立條碼掃描控制項
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingScannerView",
            };

            #region 設定完成條碼掃描後，要進行處理的工作
            zxing.OnScanResult += (result) =>
            Device.BeginInvokeOnMainThread(async () =>
            {
                // 停止分析掃描條碼工作
                zxing.IsAnalyzing = false;
                // 將掃描結果回傳到首頁
                foo本頁面的檢視模型._eventAggregator.GetEvent<ScanResultEvent>().Publish(new ScanResultPayload
                {
                     Result = result.Text,
                });
                // 回到上一頁
                await foo本頁面的檢視模型._navigationService.GoBackAsync();
            });
            #endregion
     
            #endregion
  
            #region 建立條碼掃描遮罩
            overlay = new ZXingDefaultOverlay
            {
                ShowFlashButton = zxing.HasTorch,
            };

            // 當按下 Flash 按鈕，就會顯示燈光，幫助更順利讀取條碼
            overlay.FlashButtonClicked += (sender, e) =>
            {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };
            #endregion
     
            // 將條碼掃描與分析控制項，加入到頁面上
            ScannerGrid.Children.Add(zxing);
            // 將遮罩加入到頁面上
            ScannerGrid.Children.Add(overlay);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // 只要顯示這個頁面，就會開始進行條碼掃描
            zxing.IsScanning = true;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // 離開頁面的時候，停止條碼掃描
            zxing.IsScanning = false;
        }
    }
}
