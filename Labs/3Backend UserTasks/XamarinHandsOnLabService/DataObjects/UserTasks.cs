using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XamarinHandsOnLabService.DataObjects
{
    /// <summary>
    /// 指派的工作
    /// </summary>
    public class UserTasks
    {
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
        public TaskStatus Status { get; set; }
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
    }

    /// <summary>
    /// 工作處理進度
    /// </summary>
    public enum TaskStatus
    {
        /// <summary>
        /// 尚未開始
        /// </summary>
        NOT_START,
        /// <summary>
        /// 已經打卡
        /// </summary>
        CHECKIN,
        /// <summary>
        /// 完成資料輸入
        /// </summary>
        INPUT,
        /// <summary>
        /// 圖片已經上傳
        /// </summary>
        UPLOAD_IMAGE,
        /// <summary>
        /// 已經回報
        /// </summary>
        REPORTED,
    }
}