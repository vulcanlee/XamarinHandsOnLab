using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XFTask.Helpers;
using XFTask.Models;

namespace XFTask.ViewModels
{

    public class TaskHistoryDetailPageViewModel : BindableBase, INavigationAware
    {
        #region Repositories (遠端或本地資料存取)
        public APIResult fooAPIResult { get; set; } = new APIResult();
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
        private readonly INavigationService _navigationService;
        #endregion

        #endregion

        #region Constructor 建構式
        public TaskHistoryDetailPageViewModel(INavigationService navigationService)
        {

            #region 相依性服務注入的物件

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
            var fooItem = PCLGlobalHelper.foo使用者歷史工作內容Repository.Items.FirstOrDefault(x => x.Id == Id);
            if (fooItem != null)
            {
                UpdateCurrentUserTaskVM(fooItem);
            }
            await Task.Delay(100);
        }

        /// <summary>
        /// 將 API 的工作紀錄模型資料，更新到 ViewModel 使用的工作紀錄屬性物件上
        /// </summary>
        /// <param name="userTasks"></param>
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
        #endregion

    }
}
