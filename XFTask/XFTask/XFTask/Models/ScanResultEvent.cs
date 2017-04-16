using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFTask.Models
{

    public class ScanResultEvent : PubSubEvent<ScanResultPayload>
    {

    }

    public class ScanResultPayload
    {
        public string Result { get; set; }
    }
}
