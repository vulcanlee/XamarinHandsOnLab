using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFTask.ViewModels
{

    public class UserTasksVM : BindableBase
    {
        #region Repositories (遠端或本地資料存取)

        #endregion

        #region ViewModel Property (用於在 View 中作為綁定之用)

        #region 基本型別與類別的 Property

        #region Id
        private long _Id;
        /// <summary>
        /// Id
        /// </summary>
        public long Id
        {
            get { return this._Id; }
            set { this.SetProperty(ref this._Id, value); }
        }
        #endregion

        #region Account
        private string _Account;
        /// <summary>
        /// 工作分配的帳號
        /// </summary>
        public string Account
        {
            get { return this._Account; }
            set { this.SetProperty(ref this._Account, value); }
        }
        #endregion

        #region TaskDateTime
        private DateTime _TaskDateTime;
        /// <summary>
        /// 工作產生時間
        /// </summary>
        public DateTime TaskDateTime
        {
            get { return this._TaskDateTime; }
            set { this.SetProperty(ref this._TaskDateTime, value); }
        }
        #endregion

        #region Status
        private XFTask.Models.TaskStatus _Status;
        /// <summary>
        /// 工作執行進度
        /// </summary>
        public XFTask.Models.TaskStatus Status
        {
            get { return this._Status; }
            set { this.SetProperty(ref this._Status, value); }
        }
        #endregion

        #region Title
        private string _Title;
        /// <summary>
        /// 工作的主題
        /// </summary>
        public string Title
        {
            get { return this._Title; }
            set { this.SetProperty(ref this._Title, value); }
        }
        #endregion

        #region Description
        private string _Description;
        /// <summary>
        /// 工作說明
        /// </summary>
        public string Description
        {
            get { return this._Description; }
            set { this.SetProperty(ref this._Description, value); }
        }
        #endregion

        #region CheckinId
        private string _CheckinId;
        /// <summary>
        /// 簽到核對文字
        /// </summary>
        public string CheckinId
        {
            get { return this._CheckinId; }
            set { this.SetProperty(ref this._CheckinId, value); }
        }
        #endregion

        #region Checkin_Latitude
        private double _Checkin_Latitude;
        /// <summary>
        /// 緯度
        /// </summary>
        public double Checkin_Latitude
        {
            get { return this._Checkin_Latitude; }
            set { this.SetProperty(ref this._Checkin_Latitude, value); }
        }
        #endregion

        #region Checkin_Longitude
        private double _Checkin_Longitude;
        /// <summary>
        /// 經度
        /// </summary>
        public double Checkin_Longitude
        {
            get { return this._Checkin_Longitude; }
            set { this.SetProperty(ref this._Checkin_Longitude, value); }
        }
        #endregion

        #region CheckinDatetime
        private DateTime _CheckinDatetime;
        /// <summary>
        /// 打卡時間
        /// </summary>
        public DateTime CheckinDatetime
        {
            get { return this._CheckinDatetime; }
            set { this.SetProperty(ref this._CheckinDatetime, value); }
        }
        #endregion

        #region Condition1_Ttile
        private string _Condition1_Ttile;
        /// <summary>
        /// 第1項子工作主題
        /// </summary>
        public string Condition1_Ttile
        {
            get { return this._Condition1_Ttile; }
            set { this.SetProperty(ref this._Condition1_Ttile, value); }
        }
        #endregion

        #region Condition1_Result
        private string _Condition1_Result;
        /// <summary>
        /// 第1項子工作的回報內容
        /// </summary>
        public string Condition1_Result
        {
            get { return this._Condition1_Result; }
            set { this.SetProperty(ref this._Condition1_Result, value); }
        }
        #endregion

        #region Condition2_Ttile
        private string _Condition2_Ttile;
        /// <summary>
        /// 第2項子工作主題
        /// </summary>
        public string Condition2_Ttile
        {
            get { return this._Condition2_Ttile; }
            set { this.SetProperty(ref this._Condition2_Ttile, value); }
        }
        #endregion

        #region Condition2_Result
        private string _Condition2_Result;
        /// <summary>
        /// 第2項子工作的回報內容
        /// </summary>
        public string Condition2_Result
        {
            get { return this._Condition2_Result; }
            set { this.SetProperty(ref this._Condition2_Result, value); }
        }
        #endregion

        #region Condition3_Ttile
        private string _Condition3_Ttile;
        /// <summary>
        /// 第3項子工作主題
        /// </summary>
        public string Condition3_Ttile
        {
            get { return this._Condition3_Ttile; }
            set { this.SetProperty(ref this._Condition3_Ttile, value); }
        }
        #endregion

        #region Condition3_Result
        private string _Condition3_Result;
        /// <summary>
        /// 第3項子工作的回報內容
        /// </summary>
        public string Condition3_Result
        {
            get { return this._Condition3_Result; }
            set { this.SetProperty(ref this._Condition3_Result, value); }
        }
        #endregion

        #region PhotoURL
        private string _PhotoURL;
        /// <summary>
        /// 工作回報的照片
        /// </summary>
        public string PhotoURL
        {
            get { return this._PhotoURL; }
            set { this.SetProperty(ref this._PhotoURL, value); }
        }
        #endregion

        #region Reported
        private bool _Reported;
        /// <summary>
        /// 該指派工作是否已經完成且回報
        /// </summary>
        public bool Reported
        {
            get { return this._Reported; }
            set { this.SetProperty(ref this._Reported, value); }
        }
        #endregion

        #region ReportedDatetime
        private DateTime _ReportedDatetime;
        /// <summary>
        /// 該指派工作的回報時間
        /// </summary>
        public DateTime ReportedDatetime
        {
            get { return this._ReportedDatetime; }
            set { this.SetProperty(ref this._ReportedDatetime, value); }
        }
        #endregion

        #endregion

        #region 集合類別的 Property

        #endregion

        #endregion

        #region Field 欄位

        #endregion

        #region Constructor 建構式
        public UserTasksVM()
        {

            #region 相依性服務注入的物件

            #endregion

            #region 頁面中綁定的命令

            #endregion

            #region 事件聚合器訂閱

            #endregion
        }

        #endregion

        #region 設計時期或者執行時期的ViewModel初始化
        #endregion

        #region 相關事件
        #endregion

        #region 相關的Command定義
        #endregion

        #region 其他方法

        #endregion

    }
}
