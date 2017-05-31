using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using XFTask.Helpers;
using XFTask.Repositories;

namespace XFTask.ViewModels
{

    public class SigninPageViewModel : BindableBase, INavigationAware
    {
        #region Repositories (遠端或本地資料存取)

        #endregion

        #region ViewModel Property (用於在 View 中作為綁定之用)

        #region 基本型別與類別的 Property

        #region 帳號
        private string _帳號;
        /// <summary>
        /// 帳號
        /// </summary>
        public string 帳號
        {
            get { return this._帳號; }
            set { this.SetProperty(ref this._帳號, value); }
        }
        #endregion

        #region 密碼
        private string _密碼;
        /// <summary>
        /// 密碼
        /// </summary>
        public string 密碼
        {
            get { return this._密碼; }
            set { this.SetProperty(ref this._密碼, value); }
        }
        #endregion

        #region 忙碌中遮罩
        private bool _忙碌中遮罩 = false;
        /// <summary>
        /// 忙碌中遮罩
        /// </summary>
        public bool 忙碌中遮罩
        {
            get { return this._忙碌中遮罩; }
            set { this.SetProperty(ref this._忙碌中遮罩, value); }
        }
        #endregion


        #region ProcessingMask
        private ProcessingMaskVM _ProcessingMask = new ProcessingMaskVM();
        /// <summary>
        /// ProcessingMaskVM
        /// </summary>
        public ProcessingMaskVM ProcessingMask
        {
            get { return this._ProcessingMask; }
            set { this.SetProperty(ref this._ProcessingMask, value); }
        }
        #endregion

        #endregion

        #region 集合類別的 Property

        #endregion

        #endregion

        #region Field 欄位

        #region ViewModel 內使用到的欄位
        #endregion

        #region 命令物件欄位

        public DelegateCommand 登入Command { get; set; }

        #endregion

        #region 注入物件欄位
        public readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        #endregion

        #endregion

        #region Constructor 建構式
        public SigninPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
            IPageDialogService dialogService)
        {

            #region 相依性服務注入的物件

            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            #endregion

            #region 頁面中綁定的命令
            登入Command = new DelegateCommand(async () =>
            {
                ProcessingMask.IsRunning = true;
                ProcessingMask.ProcessingTitle = "請稍後，正在忙碌中";
                ProcessingMask.ProcessingContent = "進行使用者身分驗證...";
                忙碌中遮罩 = true;
                return;

                //使用者身分驗證：登入
                var fooResult = await PCLGlobalHelper.foo使用者登入Repository.GetAsync(帳號, 密碼);
                if (fooResult.Success == false)
                {
                    await _dialogService.DisplayAlertAsync("警告", fooResult.Message, "確定");
                }
                else
                {
                    PCLGlobalHelper.foo使用者工作內容Repository.Items = new List<Models.UserTasks>();
                    PCLGlobalHelper.foo使用者歷史工作內容Repository.Items = new List<Models.UserTasks>();
                    await PCLGlobalHelper.foo使用者工作內容Repository.Write();
                    await PCLGlobalHelper.foo使用者歷史工作內容Repository.Write();
                    await _navigationService.NavigateAsync("xf:///MDPage/NaviPage/MainPage");
                }
                ProcessingMask.IsRunning = false;
                忙碌中遮罩 = false;
            });
            #endregion

            #region 事件聚合器訂閱

            #endregion
        }

        #endregion

        #region Navigation Events (頁面導航事件)
        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            await ViewModelInit();
            //PCLGlobalHelper.Init();
            //var foo1 = new 使用者登入Repository();
            //var foostr = MainHelper.BaseUrl;


            //PCLGlobal.foo使用者登入Repository = new 使用者登入Repository();

            try
            {
                var fooIt = 0;
                await PCLGlobalHelper.foo使用者登入Repository.Read();
                if (string.IsNullOrEmpty(PCLGlobalHelper.foo使用者登入Repository.Item.Account) == false)
                {
                    await _navigationService.NavigateAsync("xf:///MDPage/NaviPage/MainPage");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

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
#if DEBUG
            帳號 = "user1";
            密碼 = "pw1";
#endif
            await Task.Delay(100);
        }
        #endregion

    }
}
