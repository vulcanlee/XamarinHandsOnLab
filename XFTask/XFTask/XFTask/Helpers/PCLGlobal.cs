using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFTask.Helpers
{
    public class PCLGlobal
    {
        #region Constant
        public static string BaseUrl = "http://xamarinhandsonlab.azurewebsites.net/";
        public static string BaseAPIUrl = $"{BaseUrl}api/";
        public static string UsersAPIName = $"{BaseAPIUrl}UserLogin";
        public static string UsersAPIUrl = $"{BaseAPIUrl}{UsersAPIName}";
        public static string 資料主目錄 = $"Data";
        
        #endregion
    }
}
