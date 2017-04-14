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
    public class DBResetController : ApiController
    {
        APIResult fooAPIResult = new APIResult();
        private XamarinHandsOnLabContext db = new XamarinHandsOnLabContext();

        public APIResult Get()
        {
            CleanAllData();
            UserInit();

            fooAPIResult.Success = true;
            fooAPIResult.Message = "資料庫的測試資料已經重新產生完成";
            fooAPIResult.Payload = null;
            return fooAPIResult;
        }

        public void CleanAllData()
        {
            var fooUsers = db.Users.ToList();
            db.Users.RemoveRange(fooUsers);
            db.SaveChanges();
        }

        public void UserInit()
        {
            var fooAdmin = new DataObjects.Users
            {
                Account = "admin",
                Password = "admin",
                Department = "總經理室",
                Name = "管理者",
                PhotoUrl = "http://xamarinhandsonlab.azurewebsites.net/Images/Admin.png",
            };
            db.Users.Add(fooAdmin);
            db.SaveChanges();

            fooAdmin = db.Users.FirstOrDefault(x => x.Account == "admin");
            fooAdmin.ManagerId = fooAdmin.Id;

            for (int i = 0; i < 40; i++)
            {
               var fooUser= new DataObjects.Users
                {
                    Account = $"user{i}",
                    Password = $"pw{i}",
                    Department = $"Dept{i}",
                    Name = $"Name{i}",
                    PhotoUrl = "http://xamarinhandsonlab.azurewebsites.net/Images/Man.png",
                    ManagerId = fooAdmin.Id,
                };

                if (i % 2 == 0)
                {
                    fooUser.PhotoUrl = "http://xamarinhandsonlab.azurewebsites.net/Images/Woman.png";
                }

                db.Users.Add(fooUser);
            }
            db.SaveChanges();
        }
    }
}
