using Microsoft.Azure.Mobile.Server.Config;
using System;
using System.IO;
using System.Web;
using System.Web.Http;
using XamarinHandsOnLabService.Models;

namespace XamarinHandsOnLabService.Controllers
{
    /// <summary>
    /// 提供 App 可以上傳圖片的 API
    /// </summary>
    [MobileAppController]
    public class UploadImageController : ApiController
    {
        APIResult fooAPIResult = new APIResult();

        [HttpPost]
        public APIResult Post()
        {
            string fileName = "";  // 產生在 Server 上的圖片檔案名稱(將會配置 Guid
            string path = "";

            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;

            if (file != null && file.ContentLength > 0)
            {
                // 產生在 Server 上的圖片檔案名稱(將會配置 Guid
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); ;

                // 將圖片儲存到 Web Server 的 Uploads 目錄下
                path = Path.Combine(
                    HttpContext.Current.Server.MapPath("~/Uploads"),
                    fileName
                );

                file.SaveAs(path);

                fooAPIResult.Success = true;
                fooAPIResult.Message = "";
                fooAPIResult.Payload = fileName;
            }
            else
            {
                fooAPIResult.Success = false;
                fooAPIResult.Message = "沒有任何檔案上傳";
                fooAPIResult.Payload = null;
            }

            return fooAPIResult;
        }
    }
}
