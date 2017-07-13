using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFTask.Helpers;

namespace XFTask.ViewModels
{
    public class MDPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Repositories (遠端或本地資料存取)

        #endregion

        #region ViewModel Property (用於在 View 中作為綁定之用)

        #region 基本型別與類別的 Property

        public string UserPhoto { get; set; }

        public string UserName { get; set; }

        public bool 管理者模式 { get; set; }

        public DrawerMenuVM DrawerMenuVM代處理工作 { get; set; } = new DrawerMenuVM();
        public DrawerMenuVM DrawerMenuVM歷史工作 { get; set; } = new DrawerMenuVM();
        public DrawerMenuVM DrawerMenuVM模擬可掃描QRCode { get; set; } = new DrawerMenuVM();
        public DrawerMenuVM DrawerMenuVM更新App { get; set; } = new DrawerMenuVM();
        public DrawerMenuVM DrawerMenuVM登出 { get; set; } = new DrawerMenuVM();

        #endregion

        #region 集合類別的 Property

        #endregion

        #endregion

        #region Field 欄位

        #region ViewModel 內使用到的欄位
        #endregion

        #region 命令物件欄位

        public DelegateCommand 尚未完成派工單Command { get; set; }
        public DelegateCommand 歷史派工單Command { get; set; }
        public DelegateCommand 管理者模式命令Command { get; set; }
        public DelegateCommand 模擬可掃描的QRCodeCommand { get; set; }
        public DelegateCommand 更新AppCommand { get; set; }
        public DelegateCommand 登出Command { get; set; }

        #endregion

        #region 注入物件欄位
        public readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;

        #endregion

        #endregion

        #region Constructor 建構式
        public MDPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
            IPageDialogService dialogService)
        {

            #region 相依性服務注入的物件

            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            #endregion

            #region 頁面中綁定的命令

            #endregion

            #region 事件聚合器訂閱
            #endregion

            #region 選單項目初始化
            DrawerMenuVM代處理工作.MenuName = "代處理工作";
            DrawerMenuVM代處理工作.IconName = "fa-tasks";
            DrawerMenuVM代處理工作.DrawMenuCommand = new DelegateCommand(async () =>
            {
                // 這裡使用絕對 URI 的導航路徑
                await _navigationService.NavigateAsync("xf:///MDPage/NaviPage/MainPage");
            });

            DrawerMenuVM歷史工作.MenuName = "歷史工作";
            DrawerMenuVM歷史工作.IconName = "fa-history";
            DrawerMenuVM歷史工作.DrawMenuCommand = new DelegateCommand(async () =>
            {
                // 這裡使用絕對 URI 的導航路徑
                await _navigationService.NavigateAsync("xf:///MDPage/NaviPage/TaskHistoryPage");
            });

            DrawerMenuVM模擬可掃描QRCode.MenuName = "模擬可掃描 QR Code";
            DrawerMenuVM模擬可掃描QRCode.IconName = "fa-qrcode";
            DrawerMenuVM模擬可掃描QRCode.DrawMenuCommand = new DelegateCommand(() =>
            {
                //顯示網頁，裡面有每個工作打卡會用到的 QRCode 圖片
                Device.OpenUri(new Uri($"http://xamarinhandsonlab.azurewebsites.net/DoTasks?account={PCLGlobalHelper.foo使用者登入Repository.Item.Account}"));
            });

            DrawerMenuVM更新App.MenuName = "更新App";
            DrawerMenuVM更新App.IconName = "fa-plus-square";
            DrawerMenuVM更新App.DrawMenuCommand = new DelegateCommand(() =>
            {
                //顯示網頁，裡面有每個工作打卡會用到的 QRCode 圖片
                Device.OpenUri(new Uri($"http://bit.ly/2nUjgUq"));
            });

            DrawerMenuVM登出.MenuName = "登出";
            DrawerMenuVM登出.IconName = "fa-sign-out";
            DrawerMenuVM登出.DrawMenuCommand = new DelegateCommand(async () =>
            {
                //切換到登出頁面，並且清空已經登入的使用者資訊
                await PCLGlobalHelper.foo使用者登入Repository.Read();
                PCLGlobalHelper.foo使用者登入Repository.Item = new Models.Users();
                PCLGlobalHelper.foo使用者登入Repository.Item.Account = "";
                await PCLGlobalHelper.foo使用者登入Repository.Write();
                // 這裡使用絕對 URI 的導航路徑
                await _navigationService.NavigateAsync("xf:///SigninPage");
            });

            //    
            //
            //
            //

            #endregion
        }

        #endregion

        #region Navigation Events (頁面導航事件)
        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            await ViewModelInit();
        }
        #endregion

        #region 設計時期或者執行時期的ViewModel初始化
        #endregion

        #region 相關事件
        #endregion

        #region 相關的Command定義
        #endregion

        #region 其他方法

        /// <summary>
        /// ViewModel 資料初始化
        /// </summary>
        /// <returns></returns>
        private async Task ViewModelInit()
        {
            if (string.IsNullOrEmpty(PCLGlobalHelper.foo使用者登入Repository.Item.PhotoUrl) == false)
            {
                UserPhoto = PCLGlobalHelper.foo使用者登入Repository.Item.PhotoUrl;
            }
            UserName = PCLGlobalHelper.foo使用者登入Repository.Item.Name;
            if (PCLGlobalHelper.foo使用者登入Repository.Item.Account.ToLower() == "admin")
            {
                管理者模式 = true;
            }
            else
            {
                管理者模式 = false;
            }

            await Task.Delay(100);
        }
        #endregion

    }
}
