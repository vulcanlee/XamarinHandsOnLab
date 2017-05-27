using Microsoft.Azure.Mobile.Server.Config;
using System.Linq;
using System.Web.Http;
using XamarinHandsOnLabService.Models;

namespace XamarinHandsOnLabService.Controllers
{
    /// <summary>
    /// 將原有資料都進行刪除，並且產生新的使用者帳號
    /// 規則：
    ///    帳號名稱：user#
    ///    密碼：pw#
    /// </summary>
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

        /// <summary>
        /// 清除所有的資料
        /// </summary>
        public void CleanAllData()
        {
            // 將工作資料全部刪除掉
            db.UserTasks.RemoveRange(db.UserTasks.ToList());
            // 將使用者的資料全部刪除掉
            var fooUsers = db.Users.ToList();
            db.Users.RemoveRange(fooUsers);

            db.SaveChanges();
        }

        /// <summary>
        /// 進行使用者資料初始化
        /// </summary>
        public void UserInit()
        {
            #region 建立管理者的帳號
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
            #endregion

            fooAdmin = db.Users.FirstOrDefault(x => x.Account == "admin");
            fooAdmin.ManagerId = fooAdmin.Id;

            #region 建立四十個使用者帳號
            for (int i = 0; i < 40; i++)
            {
                var fooUser = new DataObjects.Users
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
            #endregion

            db.SaveChanges();
        }
    }
}
