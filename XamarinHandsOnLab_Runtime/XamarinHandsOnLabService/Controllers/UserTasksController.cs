using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using XamarinHandsOnLabService.DataObjects;
using XamarinHandsOnLabService.Models;

namespace XamarinHandsOnLabService.Controllers
{
    [MobileAppController]
    public class UserTasksController : ApiController
    {
        APIResult fooAPIResult = new APIResult();
        private XamarinHandsOnLabContext db = new XamarinHandsOnLabContext();

        [HttpGet]
        [Route("Filter")]
        // GET: api/UserTasks
        public APIResult Get(string account, DateTime startDate, DateTime lastDate)
        {
            var fooTasks = db.UserTasks.Where(x => x.Account == account
                           && DbFunctions.TruncateTime(x.TaskDateTime) >= startDate
                           && DbFunctions.TruncateTime(x.TaskDateTime) <= lastDate).ToList();

            fooAPIResult.Success = true;
            fooAPIResult.Message = "";
            fooAPIResult.Payload = fooTasks;
            return fooAPIResult;
        }

        [HttpGet]
        // GET: api/UserTasks
        public APIResult Get(string account)
        {
            DateTime fooDt = new DateTime(1900, 1, 1).Date;
            var fooTasks = db.UserTasks.Where(x => x.Account == account
                           && DbFunctions.TruncateTime(x.ReportedDatetime) == fooDt).ToList();

            fooAPIResult.Success = true;
            fooAPIResult.Message = "";
            fooAPIResult.Payload = fooTasks;
            return fooAPIResult;
        }

        // GET: api/UserTasks/5
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

        // POST: api/UserTasks
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UserTasks/5
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
                        #region NOT_START
                        fooAPIResult.Success = false;
                        fooAPIResult.Message = "指定狀態不正確";
                        fooAPIResult.Payload = userTasks;
                        #endregion
                        break;
                    case TaskStatus.CHECKIN:
                        #region CHECKIN
                        UpdateStatus_CHECKIN(userTasks, fooDBObj);
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
                        #region INPUT
                        UpdateStatus_CHECKIN(userTasks, fooDBObj);
                        UpdateStatus_INPUT(userTasks, fooDBObj);
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
                        #region UPLOAD_IMAGE
                        UpdateStatus_CHECKIN(userTasks, fooDBObj);
                        UpdateStatus_INPUT(userTasks, fooDBObj);
                        UpdateStatus_UPLOAD_IMAGE(userTasks, fooDBObj);
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
                        #region REPORTED
                        UpdateStatus_CHECKIN(userTasks, fooDBObj);
                        UpdateStatus_INPUT(userTasks, fooDBObj);
                        UpdateStatus_UPLOAD_IMAGE(userTasks, fooDBObj);
                        UpdateStatus_REPORTED(userTasks, fooDBObj);
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

        // DELETE: api/UserTasks/5
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
        /// <param name="userTasks"></param>
        /// <param name="targetDB"></param>
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
        /// <param name="userTasks"></param>
        /// <param name="fooDBObj"></param>
        private void UpdateStatus_UPLOAD_IMAGE(UserTasks userTasks, UserTasks targetDB)
        {
            targetDB.Status = TaskStatus.UPLOAD_IMAGE;
        }

        /// <summary>
        /// 使用者已經回報工作了，將狀態更新到回報工作狀態
        /// </summary>
        /// <param name="userTasks"></param>
        /// <param name="fooDBObj"></param>
        private void UpdateStatus_REPORTED(UserTasks userTasks, UserTasks targetDB)
        {
            targetDB.Status = TaskStatus.REPORTED;
            targetDB.ReportedDatetime = DateTime.UtcNow.AddHours(8);
        }

    }
}
