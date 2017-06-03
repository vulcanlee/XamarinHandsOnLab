using System;
using System.ComponentModel;

namespace XFTask.ViewModels
{
    public class UserTasksVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Repositories (遠端或本地資料存取)

        #endregion

        #region ViewModel Property (用於在 View 中作為綁定之用)

        #region 基本型別與類別的 Property

        public long Id { get; set; }
        /// <summary>
        /// 工作分配的帳號
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 工作產生時間
        /// </summary>
        public DateTime TaskDateTime { get; set; }
        /// <summary>
        /// 工作執行進度
        /// </summary>
        public XFTask.Models.TaskStatus Status { get; set; }
        /// <summary>
        /// 工作的主題
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 工作說明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 簽到核對文字
        /// </summary>
        public string CheckinId { get; set; }
        /// <summary>
        /// 緯度
        /// </summary>
        public double Checkin_Latitude { get; set; }
        /// <summary>
        /// 經度
        /// </summary>
        public double Checkin_Longitude { get; set; }
        /// <summary>
        /// 打卡時間
        /// </summary>
        public DateTime CheckinDatetime { get; set; }
        /// <summary>
        /// 第1項子工作主題
        /// </summary>
        public string Condition1_Ttile { get; set; }
        /// <summary>
        /// 第1項子工作的回報內容
        /// </summary>
        public string Condition1_Result { get; set; }
        /// <summary>
        /// 第2項子工作主題
        /// </summary>
        public string Condition2_Ttile { get; set; }
        /// <summary>
        /// 第2項子工作的回報內容
        /// </summary>
        public string Condition2_Result { get; set; }
        /// <summary>
        /// 第3項子工作主題
        /// </summary>
        public string Condition3_Ttile { get; set; }
        /// <summary>
        /// 第3項子工作的回報內容
        /// </summary>
        public string Condition3_Result { get; set; }
        /// <summary>
        /// 工作回報的照片
        /// </summary>
        public string PhotoURL { get; set; }
        /// <summary>
        /// 該指派工作是否已經完成且回報
        /// </summary>
        public bool Reported { get; set; }
        /// <summary>
        /// 該指派工作的回報時間
        /// </summary>
        public DateTime ReportedDatetime { get; set; }

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
