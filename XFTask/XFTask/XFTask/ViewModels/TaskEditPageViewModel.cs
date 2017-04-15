using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XFTask.Helpers;
using XFTask.Models;

namespace XFTask.ViewModels
{

    public class TaskEditPageViewModel : BindableBase, INavigationAware
    {
        #region Repositories (遠端或本地資料存取)

        #endregion

        #region ViewModel Property (用於在 View 中作為綁定之用)

        #region 基本型別與類別的 Property

        #region CurrentUserTasksVM
        private UserTasksVM _CurrentUserTasksVM = new UserTasksVM();
        /// <summary>
        /// CurrentUserTasksVM
        /// </summary>
        public UserTasksVM CurrentUserTasksVM
        {
            get { return this._CurrentUserTasksVM; }
            set { this.SetProperty(ref this._CurrentUserTasksVM, value); }
        }
        #endregion

        #endregion

        #region 集合類別的 Property

        #endregion

        #endregion

        #region Field 欄位

        #region ViewModel 內使用到的欄位
        public long Id = 0;
        #endregion

        #region 命令物件欄位
        #endregion

        #region 注入物件欄位
        public readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        #endregion

        #endregion

        #region Constructor 建構式
        public TaskEditPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
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
            if (parameters.ContainsKey("ID"))
            {
                Id = Convert.ToInt64(parameters["ID"] as string);
                await ViewModelInit();
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
            var fooItem = PCLGlobal.使用者工作內容Repository.Items.FirstOrDefault(x => x.Id == Id);
            if (fooItem != null)
            {
                UpdateCurrentUserTaskVM(fooItem);
            }
            await Task.Delay(100);
        }

        void UpdateCurrentUserTaskVM(UserTasks userTasks)
        {
            CurrentUserTasksVM.Id = userTasks.Id;
            CurrentUserTasksVM.Account = userTasks.Account;
            CurrentUserTasksVM.CheckinDatetime = userTasks.CheckinDatetime;
            CurrentUserTasksVM.CheckinId = userTasks.CheckinId;
            CurrentUserTasksVM.Checkin_Latitude = userTasks.Checkin_Latitude;
            CurrentUserTasksVM.Checkin_Longitude = userTasks.Checkin_Longitude;
            CurrentUserTasksVM.Condition1_Ttile = userTasks.Condition1_Ttile;
            CurrentUserTasksVM.Condition1_Result = userTasks.Condition1_Result;
            CurrentUserTasksVM.Condition2_Ttile = userTasks.Condition2_Ttile;
            CurrentUserTasksVM.Condition2_Result = userTasks.Condition2_Result;
            CurrentUserTasksVM.Condition3_Ttile = userTasks.Condition3_Ttile;
            CurrentUserTasksVM.Condition3_Result = userTasks.Condition3_Result;
            CurrentUserTasksVM.Description = userTasks.Description;
            CurrentUserTasksVM.PhotoURL = userTasks.PhotoURL;
            CurrentUserTasksVM.Reported = userTasks.Reported;
            CurrentUserTasksVM.ReportedDatetime = userTasks.ReportedDatetime;
            CurrentUserTasksVM.Status = userTasks.Status;
            CurrentUserTasksVM.TaskDateTime = userTasks.TaskDateTime;
            CurrentUserTasksVM.Title = userTasks.Title;
        }


        UserTasks UpdateUserTasks(UserTasksVM userTaskVM)
        {
            var fooUserTasks = new UserTasks();
            fooUserTasks.Id = userTaskVM.Id;
            fooUserTasks.Account = userTaskVM.Account;
            fooUserTasks.CheckinDatetime = userTaskVM.CheckinDatetime;
            fooUserTasks.CheckinId = userTaskVM.CheckinId;
            fooUserTasks.Checkin_Latitude = userTaskVM.Checkin_Latitude;
            fooUserTasks.Checkin_Longitude = userTaskVM.Checkin_Longitude;
            fooUserTasks.Condition1_Ttile = userTaskVM.Condition1_Ttile;
            fooUserTasks.Condition1_Result = userTaskVM.Condition1_Result;
            fooUserTasks.Condition2_Ttile = userTaskVM.Condition2_Ttile;
            fooUserTasks.Condition2_Result = userTaskVM.Condition2_Result;
            fooUserTasks.Condition3_Ttile = userTaskVM.Condition3_Ttile;
            fooUserTasks.Condition3_Result = userTaskVM.Condition3_Result;
            fooUserTasks.Description = userTaskVM.Description;
            fooUserTasks.PhotoURL = userTaskVM.PhotoURL;
            fooUserTasks.Reported = userTaskVM.Reported;
            fooUserTasks.ReportedDatetime = userTaskVM.ReportedDatetime;
            fooUserTasks.Status = userTaskVM.Status;
            fooUserTasks.TaskDateTime = userTaskVM.TaskDateTime;
            fooUserTasks.Title = userTaskVM.Title;
            return fooUserTasks;
        }
        #endregion

    }
}
