using Microsoft.Azure.Mobile.Server.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using XamarinHandsOnLabService.Models;

namespace XamarinHandsOnLabService.Controllers
{
    [MobileAppController]
    public class UploadImageController : ApiController
    {
        APIResult fooAPIResult = new APIResult();

        [HttpPost]
        public APIResult Post()
        {
            string fileName = "";
            string path = "";

            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;

            if (file != null && file.ContentLength > 0)
            {
                fileName = Path.GetFileName(file.FileName) + Path.GetExtension(file.FileName);
                fileName = Path.GetFileName(file.FileName);
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); ;

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
