using System;
using System.Linq;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Data.Entity;
using XamarinHandsOnLabService.DataObjects;
using XamarinHandsOnLabService.Models;
using System.Text;

namespace XamarinHandsOnLabService.Controllers
{
    /// <summary>
    /// 產生今天的工作紀錄，每個使用者都會有五個新的工作
    /// </summary>
    [MobileAppController]
    public class TodayTaskController : ApiController
    {
        APIResult fooAPIResult = new APIResult();
        private XamarinHandsOnLabContext db = new XamarinHandsOnLabContext();
        private static Random random = new Random((int)DateTime.Now.Ticks);

        public APIResult Get()
        {
            var fooToday = DateTime.Now.Date;
            // 查詢今天產生的工作有那些
            var fooTasks = db.UserTasks.Where(x => DbFunctions.TruncateTime(x.TaskDateTime) == fooToday).ToList();
            if (fooTasks.Count > 0)
            {
                #region 今天的工作已經有產生了
                fooAPIResult.Success = false;
                fooAPIResult.Message = DateTime.Now.ToString("yyyy.MM.dd") + " 已經有存在的的代辦工作";
                fooAPIResult.Payload = null;
                #endregion
            }
            else
            {
                #region 幫每個使用者，產生今天要用到的五個工作
                for (int i = 0; i < 40; i++)
                {
                    var fooAccount = $"user{i}";
                    for (int j = 0; j < 5; j++)
                    {
                        var fooTask = new UserTasks()
                        {
                            Account = fooAccount,
                            Title = $"請前往地點 {j} 進行簽到與調查作業環境",
                            Description = "請到指定地點掃描 QR Code，並且填寫當時工作環境數據",
                            TaskDateTime = DateTime.Now.Date,
                            CheckinId = RandomString(30),
                            Condition1_Ttile = "請讀取儀表1的數據",
                            Condition1_Result = "",
                            Condition2_Ttile = "請讀取儀表2的數據",
                            Condition2_Result = "",
                            Condition3_Ttile = "請讀取儀表3的數據",
                            Condition3_Result = "",
                            Checkin_Latitude = 0,
                            Checkin_Longitude = 0,
                            PhotoURL = "",
                            Reported = false,
                            ReportedDatetime = new DateTime(1900, 1, 1),
                            CheckinDatetime = new DateTime(1900, 1, 1),
                        };

                        db.UserTasks.Add(fooTask);
                    }
                }
                #endregion
                db.SaveChanges();

                fooAPIResult.Success = true;
                fooAPIResult.Message = DateTime.Now.ToString("yyyy.MM.dd") + " 的代辦工作已經產生完成";
                fooAPIResult.Payload = null;
            }
            return fooAPIResult;
        }

        /// <summary>
        /// 產生隨機字串，用於 QR Code 的內容
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
