using Microsoft.Azure.Mobile.Server.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XamarinHandsOnLabService.Models;

namespace XamarinHandsOnLabService.Controllers
{
    [MobileAppController]
    public class UserLoginController : ApiController
    {
        APIResult fooAPIResult = new APIResult();
        private XamarinHandsOnLabContext db = new XamarinHandsOnLabContext();

        // GET: api/UserLogin/5
        public APIResult Get(string account, string password)
        {
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
                fooAPIResult.Message = "";
                fooAPIResult.Payload = null;
            }
            return fooAPIResult;
        }

    }
}
