using System;
using System.Web;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using ZXing;
using ZXing.Common;

namespace XamarinHandsOnLabService.Helpers
{
    public static class QRHelper
    {
        /// <summary>
        /// 產生一個 QR Code 圖片
        /// </summary>
        /// <param name="html"></param>
        /// <param name="url"></param>
        /// <param name="alt"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="margin"></param>
        /// <returns></returns>
        public static IHtmlString GenerateQrCode(this HtmlHelper html, string url, string alt = "QR code", int height = 200, int width = 200, int margin = 0)
        {
            var qrWriter = new BarcodeWriter();
            qrWriter.Format = BarcodeFormat.QR_CODE;
            qrWriter.Options = new EncodingOptions() { Height = height, Width = width, Margin = margin };

            using (var q = qrWriter.Write(url))
            {
                using (var ms = new MemoryStream())
                {
                    q.Save(ms, ImageFormat.Png);
                    var img = new TagBuilder("img");
                    img.Attributes.Add("src", String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray())));
                    img.Attributes.Add("alt", alt);
                    return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
                }
            }
        }
    }
}