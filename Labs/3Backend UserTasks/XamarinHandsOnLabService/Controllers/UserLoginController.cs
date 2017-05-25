using System.Linq;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using XamarinHandsOnLabService.Models;

namespace XamarinHandsOnLabService.Controllers
{
    /// <summary>
    /// 提供使用者登入、身分驗證的服務
    /// </summary>
    [MobileAppController]
    public class UserLoginController : ApiController
    {
        APIResult fooAPIResult = new APIResult();
        private XamarinHandsOnLabContext db = new XamarinHandsOnLabContext();

        // GET: api/UserLogin/5
        public APIResult Get(string account, string password)
        {
            // 依據所提供的帳號與密碼，查詢是否存在
            var fooObject = db.Users.FirstOrDefault(x => x.Account == account && x.Password == password);
            if (fooObject != null)
            {
                fooAPIResult.Success = true;
                fooAPIResult.Message = "";
                fooAPIResult.Payload = fooObject;
            }
            else
            {
                fooAPIResult.Success = false;
                fooAPIResult.Message = "帳號或者密碼不正確";
                fooAPIResult.Payload = null;
            }
            return fooAPIResult;
        }

    }
}
