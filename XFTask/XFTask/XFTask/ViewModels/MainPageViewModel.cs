using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using XFTask.Helpers;
using XFTask.Models;

namespace XFTask.ViewModels
{

    public class MainPageViewModel : BindableBase, INavigationAware
    {
        #region Repositories (遠端或本地資料存取)
        public APIResult fooAPIResult { get; set; } = new APIResult();

        #endregion

        #region ViewModel Property (用於在 View 中作為綁定之用)

        #region 基本型別與類別的 Property

        #region UserTasksListSelected
        private UserTasksVM _UserTasksListSelected;
        /// <summary>
        /// UserTasksListSelected
        /// </summary>
        public UserTasksVM UserTasksListSelected
        {
            get { return this._UserTasksListSelected; }
            set { this.SetProperty(ref this._UserTasksListSelected, value); }
        }
        #endregion

        #region IsRefreshing
        private bool _IsRefreshing = false;
        /// <summary>
        /// IsRefreshing
        /// </summary>
        public bool IsRefreshing
        {
            get { return this._IsRefreshing; }
            set { this.SetProperty(ref this._IsRefreshing, value); }
        }
        #endregion

        #endregion

        #region 集合類別的 Property
        #region UserTasksList
        private ObservableCollection<UserTasksVM> _UserTasksList = new ObservableCollection<UserTasksVM>();
        /// <summary>
        /// UserTasksList
        /// </summary>
        public ObservableCollection<UserTasksVM> UserTasksList
        {
            get { return _UserTasksList; }
            set { SetProperty(ref _UserTasksList, value); }
        }
        #endregion

        #endregion

        #endregion

        #region Field 欄位

        #region ViewModel 內使用到的欄位
        #endregion

        #region 命令物件欄位

        public DelegateCommand ItemTappedCommand { get; set; }

        public DelegateCommand DoRefreshCommand { get; set; }

        #endregion

        #region 注入物件欄位
        public readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        #endregion

        #endregion

        #region Constructor 建構式
        public MainPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
            IPageDialogService dialogService)
        {

            #region 相依性服務注入的物件

            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            #endregion

            #region 頁面中綁定的命令
            DoRefreshCommand = new DelegateCommand(async () =>
            {
                fooAPIResult = await PCLGlobal.使用者工作內容Repository.GetDateRangeAsync(
                    PCLGlobal.使用者登入Repository.Item.Account,
                    DateTime.Now.Date,DateTime.Now.Date);

                if (fooAPIResult.Success == true)
                {
                    UserTasksList.Clear();
                    foreach (var item in PCLGlobal.使用者工作內容Repository.Items)
                    {
                        AddViewModel(item);
                    }
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("警告", fooAPIResult.Message, "確定");
                }
                IsRefreshing = false;
            });

            ItemTappedCommand = new DelegateCommand(async () =>
            {
                await _navigationService.NavigateAsync($"TaskEditPage?ID={UserTasksListSelected.Id}");
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
            var fooItems = await PCLGlobal.使用者工作內容Repository.Read();
            UserTasksList.Clear();
            foreach (var item in fooItems)
            {
                AddViewModel(item);
            }
            await Task.Delay(100);
        }
        #endregion

        void AddViewModel(UserTasks userTask)
        {
            UserTasksVM fooUserTasksVM = new UserTasksVM
            {
                Account = userTask.Account,
                CheckinDatetime = userTask.CheckinDatetime,
                CheckinId = userTask.CheckinId,
                Checkin_Latitude = userTask.Checkin_Latitude,
                Checkin_Longitude = userTask.Checkin_Longitude,
                Condition1_Result = userTask.Condition1_Result,
                Condition1_Ttile = userTask.Condition1_Ttile,
                Condition2_Result = userTask.Condition2_Result,
                Condition2_Ttile = userTask.Condition2_Ttile,
                Condition3_Result = userTask.Condition3_Result,
                Condition3_Ttile = userTask.Condition3_Ttile,
                Description = userTask.Description,
                Id = userTask.Id,
                PhotoURL = userTask.PhotoURL,
                Reported = userTask.Reported,
                ReportedDatetime = userTask.ReportedDatetime,
                Status = userTask.Status,
                TaskDateTime = userTask.TaskDateTime,
                Title = userTask.Title, 
            };
            UserTasksList.Add(fooUserTasksVM);
        }
    }
}
