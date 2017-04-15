using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFTask.Repositories;

namespace XFTask.Helpers
{
    public class PCLGlobal
    {
        #region Constant
        public static string BaseUrl = "http://xamarinhandsonlab.azurewebsites.net/";
        public static string BaseAPIUrl = $"{BaseUrl}api/";
        public static string UserLoginAPIName = $"UserLogin";
        public static string UserLoginAPIUrl = $"{BaseAPIUrl}{UserLoginAPIName}";
        public static string 資料主目錄 = $"Data";

        #endregion

        #region Repository
        public static 使用者登入Repository 使用者登入Repository = new 使用者登入Repository();
        #endregion
    }
}
