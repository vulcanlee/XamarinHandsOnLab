using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFTask.Models
{
    /// <summary>
    /// QR Code 控制項讀取到 QR Code 內容之後，會發送的事件定義
    /// </summary>
    public class ScanResultEvent : PubSubEvent<ScanResultPayload>
    {

    }

    /// <summary>
    ///  QR Code 控制項讀取到 QR Code 內容之後，會透過事件傳遞的內容
    /// </summary>
    public class ScanResultPayload
    {
        public string Result { get; set; }
    }
}
