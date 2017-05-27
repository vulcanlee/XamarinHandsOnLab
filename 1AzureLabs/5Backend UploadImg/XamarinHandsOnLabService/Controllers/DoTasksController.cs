using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using XamarinHandsOnLabService.DataObjects;
using XamarinHandsOnLabService.Models;
using Microsoft.Azure.Mobile.Server.Config;
using System.Threading.Tasks;

namespace XamarinHandsOnLabService.Controllers
{
    /// <summary>
    /// 顯示網頁，展示出該使用者要打卡的 QR Code 圖片
    /// </summary>
    [MobileAppController]
    public class DoTasksController : Controller
    {
        private XamarinHandsOnLabContext db = new XamarinHandsOnLabContext();

        // GET: DoTasks
        public async Task<ActionResult> Index(string account)
        {
            // 查詢該使用的未完成工作有哪些
            var fooToday = new DateTime(1900, 1, 1).Date;
            var foo = await db.UserTasks.Where(x => DbFunctions.TruncateTime(x.ReportedDatetime) == fooToday
                                    && x.Account == account).ToListAsync();
            return View(await db.UserTasks.Where(x => DbFunctions.TruncateTime(x.ReportedDatetime) == fooToday
                                  && x.Account == account).ToListAsync());
        }

        // GET: DoTasks/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTasks userTasks = await db.UserTasks.FindAsync(id);
            if (userTasks == null)
            {
                return HttpNotFound();
            }
            return View(userTasks);
        }

        // GET: DoTasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoTasks/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Account,TaskDateTime,Status,Title,Description,CheckinId,Checkin_Latitude,Checkin_Longitude,CheckinDatetime,Condition1_Ttile,Condition1_Result,Condition2_Ttile,Condition2_Result,Condition3_Ttile,Condition3_Result,PhotoURL,Reported,ReportedDatetime")] UserTasks userTasks)
        {
            if (ModelState.IsValid)
            {
                db.UserTasks.Add(userTasks);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(userTasks);
        }

        // GET: DoTasks/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTasks userTasks = await db.UserTasks.FindAsync(id);
            if (userTasks == null)
            {
                return HttpNotFound();
            }
            return View(userTasks);
        }

        // POST: DoTasks/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Account,TaskDateTime,Status,Title,Description,CheckinId,Checkin_Latitude,Checkin_Longitude,CheckinDatetime,Condition1_Ttile,Condition1_Result,Condition2_Ttile,Condition2_Result,Condition3_Ttile,Condition3_Result,PhotoURL,Reported,ReportedDatetime")] UserTasks userTasks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userTasks).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userTasks);
        }

        // GET: DoTasks/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTasks userTasks = await db.UserTasks.FindAsync(id);
            if (userTasks == null)
            {
                return HttpNotFound();
            }
            return View(userTasks);
        }

        // POST: DoTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            UserTasks userTasks = await db.UserTasks.FindAsync(id);
            db.UserTasks.Remove(userTasks);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
