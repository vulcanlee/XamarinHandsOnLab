using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFTask.Helpers
{
    /// <summary>
    /// 這是整個應用程式都可以存取的一個支援類別屬性與方法
    /// </summary>
    public class PCLGlobalHelper
    {
        #region 常用的變數 Constant
        /// <summary>
        /// Web API 的網址，最好使用 Https
        /// </summary>
        public static string BaseUrl = "http://xamarinhandsonlab.azurewebsites.net/";
        /// <summary>
        /// 呼叫 API 的最上層名稱
        /// </summary>
        public static string BaseAPIUrl = $"{BaseUrl}api/";
        /// <summary>
        /// 使用者身分驗證的 API 名稱
        /// </summary>
        public static string UserLoginAPIName = $"UserLogin";
        public static string UserLoginAPIUrl = $"{BaseAPIUrl}{UserLoginAPIName}";
        /// <summary>
        /// 指派工作紀錄的的 API 名稱
        /// </summary>
        public static string UserTasksAPIName = $"UserTasks";
        /// <summary>
        /// 查詢指定時段內的工作紀錄的 API 名稱
        /// </summary>
        public static string UserTasksHistoryAPIName = $"Filter";
        public static string UserTasksCompletionFileName = $"UserTasksCompletion";
        public static string UserTasksAPIUrl = $"{BaseAPIUrl}{UserTasksAPIName}";
        public static string UserTasksHistoryAPIUrl = $"{BaseAPIUrl}{UserTasksAPIName}/{UserTasksHistoryAPIName}";
        public static string UploadImageAPIName = $"UploadImage";
        public static string UploadImageAPIUrl = $"{BaseAPIUrl}{UploadImageAPIName}";
        public static string 資料主目錄 = $"Data";

        #endregion

        #region Repository (此處為方便開發，所以，所有的 Repository 皆為全域靜態可存取

        #endregion

        #region Azure Mobile App 相關設定
        /// <summary>
        /// 指向 Azure Mobile App 服務的主要網址
        /// </summary>
        public static string MainURL = "http://xamarinhandsonlab.azurewebsites.net";
        /// <summary>
        /// Azure Mobile App 線上版本的用戶端
        /// </summary>
        public static MobileServiceClient AzureMobileClient = new MobileServiceClient(MainURL);
        #endregion
    }
}
