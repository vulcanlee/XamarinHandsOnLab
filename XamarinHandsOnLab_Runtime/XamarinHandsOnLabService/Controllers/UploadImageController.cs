using Microsoft.Azure.Mobile.Server.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace XamarinHandsOnLabService.Controllers
{
    [MobileAppController]
    public class UploadImageController : ApiController
    {
        [HttpPost]
        public string Post()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName) + Path.GetExtension(file.FileName);
                fileName = Guid.NewGuid().ToString();

                var path = Path.Combine(
                    HttpContext.Current.Server.MapPath("~/Uploads"),
                    fileName
                );

                file.SaveAs(path);
            }

            return file != null ? "/Uploads/" + file.FileName : "";
        }
    }
}
