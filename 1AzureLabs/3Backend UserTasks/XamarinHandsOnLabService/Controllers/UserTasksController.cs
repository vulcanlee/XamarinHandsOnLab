using Microsoft.Azure.Mobile.Server.Config;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using XamarinHandsOnLabService.DataObjects;
using XamarinHandsOnLabService.Models;

namespace XamarinHandsOnLabService.Controllers
{
    /// <summary>
    /// 使用者指派工作的相關 API 服務
    /// </summary>
    [MobileAppController]
    public class UserTasksController : ApiController
    {
        APIResult fooAPIResult = new APIResult();
        private XamarinHandsOnLabContext db = new XamarinHandsOnLabContext();

        /// <summary>
        /// 依據指定的時間，查詢該使用者的所有分配工作
        /// </summary>
        /// <param name="account">使用者帳號</param>
        /// <param name="startDate">開始時間</param>
        /// <param name="lastDate">結束時間</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Filter")]
        public APIResult Get(string account, DateTime startDate, DateTime lastDate)
        {
            // 使用 Entity Framework + LINQ 查詢使用者在指定時間內的所有工作
            var fooTasks = db.UserTasks.Where(x => x.Account == account
                           && DbFunctions.TruncateTime(x.TaskDateTime) >= startDate
                           && DbFunctions.TruncateTime(x.TaskDateTime) <= lastDate).ToList();

            fooAPIResult.Success = true;
            fooAPIResult.Message = "";
            fooAPIResult.Payload = fooTasks;
            return fooAPIResult;
        }

        /// <summary>
        /// 取得該使用者尚未完成的工作清單
        /// </summary>
        /// <param name="account">使用者帳號</param>
        /// <returns></returns>
        [HttpGet]
        // GET: api/UserTasks
        public APIResult Get(string account)
        {
            DateTime fooDt = new DateTime(1900, 1, 1).Date;
            // 使用 Entity Framework + LINQ 查詢使用者尚未完成的所有工作
            var fooTasks = db.UserTasks.Where(x => x.Account == account
                           && DbFunctions.TruncateTime(x.ReportedDatetime) == fooDt).ToList();

            fooAPIResult.Success = true;
            fooAPIResult.Message = "";
            fooAPIResult.Payload = fooTasks;
            return fooAPIResult;
        }

        /// <summary>
        /// 依據使用者工作編號，查詢該工作紀錄
        /// </summary>
        /// <param name="id">工作紀錄編號</param>
        /// <returns></returns>
        [HttpGet]
        [Route("FilterById")]
        public APIResult Get(long id)
        {
            var fooTask = db.UserTasks.FirstOrDefault(x => x.Id == id);
            if (fooTask == null)
            {
                fooAPIResult.Success = false;
                fooAPIResult.Message = "沒有發現指定的工作紀錄";
                fooAPIResult.Payload = null;
            }
            else
            {
                fooAPIResult.Success = true;
                fooAPIResult.Message = "";
                fooAPIResult.Payload = fooTask;
            }
            return fooAPIResult;
        }

        /// <summary>
        /// Post 方法，在這個練習中沒有用到
        /// </summary>
        /// <param name="value"></param>
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// 更新App傳入的工作紀錄內容
        /// </summary>
        /// <param name="userTasks">工作紀錄內容</param>
        /// <returns></returns>
        public APIResult Put([FromBody]UserTasks userTasks)
        {
            string foot = HttpContext.Current.Server.MapPath("~/Uploads");

            int fooNumRec = 0;
            var fooDBObj = db.UserTasks.FirstOrDefault(x => x.Id == userTasks.Id);
            if (fooDBObj != null)
            {
                switch (userTasks.Status)
                {
                    case TaskStatus.NOT_START:
                        #region 工作尚未開始
                        fooAPIResult.Success = false;
                        fooAPIResult.Message = "指定狀態不正確";
                        fooAPIResult.Payload = userTasks;
                        #endregion
                        break;

                    case TaskStatus.CHECKIN:
                        #region 打卡
                        UpdateStatus_CHECKIN(userTasks, fooDBObj);
                        // 通知 Entity Framework，這個紀錄有異動了
                        db.Entry(fooDBObj).State = EntityState.Modified;
                        fooNumRec = db.SaveChanges();
                        if (fooNumRec > 0)
                        {
                            fooAPIResult.Success = true;
                            fooAPIResult.Message = "成功更新資料";
                            fooAPIResult.Payload = fooDBObj;
                        }
                        else
                        {
                            fooAPIResult.Success = false;
                            fooAPIResult.Message = "沒有成功更新任何資料";
                            fooAPIResult.Payload = userTasks;
                        }
                        #endregion
                        break;

                    case TaskStatus.INPUT:
                        #region 工作內容輸入
                        UpdateStatus_CHECKIN(userTasks, fooDBObj);
                        UpdateStatus_INPUT(userTasks, fooDBObj);
                        // 通知 Entity Framework，這個紀錄有異動了
                        db.Entry(fooDBObj).State = EntityState.Modified;
                        fooNumRec = db.SaveChanges();
                        if (fooNumRec > 0)
                        {
                            fooAPIResult.Success = true;
                            fooAPIResult.Message = "成功更新資料";
                            fooAPIResult.Payload = fooDBObj;
                        }
                        else
                        {
                            fooAPIResult.Success = false;
                            fooAPIResult.Message = "沒有成功更新任何資料";
                            fooAPIResult.Payload = userTasks;
                        }
                        #endregion
                        break;

                    case TaskStatus.UPLOAD_IMAGE:
                        #region 圖片上傳
                        UpdateStatus_CHECKIN(userTasks, fooDBObj);
                        UpdateStatus_INPUT(userTasks, fooDBObj);
                        UpdateStatus_UPLOAD_IMAGE(userTasks, fooDBObj);
                        // 通知 Entity Framework，這個紀錄有異動了
                        db.Entry(fooDBObj).State = EntityState.Modified;
                        fooNumRec = db.SaveChanges();
                        if (fooNumRec > 0)
                        {
                            fooAPIResult.Success = true;
                            fooAPIResult.Message = "成功更新資料";
                            fooAPIResult.Payload = fooDBObj;
                        }
                        else
                        {
                            fooAPIResult.Success = false;
                            fooAPIResult.Message = "沒有成功更新任何資料";
                            fooAPIResult.Payload = userTasks;
                        }
                        #endregion
                        break;

                    case TaskStatus.REPORTED:
                        #region 完工與回報
                        UpdateStatus_CHECKIN(userTasks, fooDBObj);
                        UpdateStatus_INPUT(userTasks, fooDBObj);
                        UpdateStatus_UPLOAD_IMAGE(userTasks, fooDBObj);
                        UpdateStatus_REPORTED(userTasks, fooDBObj);
                        // 通知 Entity Framework，這個紀錄有異動了
                        db.Entry(fooDBObj).State = EntityState.Modified;
                        fooNumRec = db.SaveChanges();
                        if (fooNumRec > 0)
                        {
                            fooAPIResult.Success = true;
                            fooAPIResult.Message = "成功更新資料";
                            fooAPIResult.Payload = fooDBObj;
                        }
                        else
                        {
                            fooAPIResult.Success = false;
                            fooAPIResult.Message = "沒有成功更新任何資料";
                            fooAPIResult.Payload = userTasks;
                        }
                        #endregion
                        break;

                    default:
                        fooAPIResult.Success = false;
                        fooAPIResult.Message = $"沒有符合的工作狀態馬 = {userTasks.Status}";
                        fooAPIResult.Payload = userTasks;
                        break;
                }
            }
            else
            {
                fooAPIResult.Success = false;
                fooAPIResult.Message = $"沒有發現工作紀錄 ID = {userTasks.Id}";
                fooAPIResult.Payload = userTasks;
            }
            return fooAPIResult;
        }

        // DELETE 方法，在這個練習中沒有用到
        public void Delete(int id)
        {
        }

        /// <summary>
        /// 使用者已經打卡了，將狀態更新到打卡狀態
        /// </summary>
        /// <param name="source"></param>
        /// <param name="targetDB"></param>
        void UpdateStatus_CHECKIN(UserTasks source, UserTasks targetDB)
        {
            targetDB.Status = TaskStatus.CHECKIN;
            targetDB.CheckinDatetime = DateTime.UtcNow.AddHours(8);
            targetDB.Checkin_Latitude = source.Checkin_Latitude;
            targetDB.Checkin_Longitude = source.Checkin_Longitude;
        }

        /// <summary>
        /// 使用者已經填寫工作內容了，將狀態更新到資料已經輸入狀態
        /// </summary>
        /// <param name="userTasks">App傳入的工作紀錄</param>
        /// <param name="targetDB">要更新到資料庫內的工作紀錄</param>
        private void UpdateStatus_INPUT(UserTasks userTasks, UserTasks targetDB)
        {
            targetDB.Status = TaskStatus.INPUT;
            targetDB.Condition1_Result = userTasks.Condition1_Result;
            targetDB.Condition2_Result = userTasks.Condition2_Result;
            targetDB.Condition3_Result = userTasks.Condition3_Result;
        }

        /// <summary>
        /// 使用者已經上傳圖片了，將狀態更新到上傳圖片狀態
        /// </summary>
        /// <param name="userTasks">App傳入的工作紀錄</param>
        /// <param name="fooDBObj">要更新到資料庫內的工作紀錄</param>
        private void UpdateStatus_UPLOAD_IMAGE(UserTasks userTasks, UserTasks targetDB)
        {
            string fooPath1 = "";
            string fooPath2 = "";
            targetDB.Status = TaskStatus.UPLOAD_IMAGE;
            if (userTasks.PhotoURL.IndexOf("http://") >= 0 || userTasks.PhotoURL.IndexOf("https://") >= 0)
            {
                // 因為該欄位是個網址，所以，圖片已經上傳且處理好了
            }
            else
            {
                #region 將上傳後的圖片，從暫存區 Uploads 搬移到 Images 目錄下
                fooPath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads"), userTasks.PhotoURL);
                fooPath2 = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), userTasks.PhotoURL);
                File.Move(fooPath1, fooPath2);
                #endregion
                // 將上傳圖片檔名，更換成為一個 URL
                targetDB.PhotoURL = $"http://xamarinhandsonlab.azurewebsites.net/Images/{userTasks.PhotoURL}";
            }
        }

        /// <summary>
        /// 使用者已經回報工作了，將狀態更新到回報工作狀態
        /// </summary>
        /// <param name="userTasks">App傳入的工作紀錄</param>
        /// <param name="fooDBObj">要更新到資料庫內的工作紀錄</param>
        private void UpdateStatus_REPORTED(UserTasks userTasks, UserTasks targetDB)
        {
            targetDB.Status = TaskStatus.REPORTED;
            targetDB.ReportedDatetime = DateTime.UtcNow.AddHours(8);
        }

    }
}