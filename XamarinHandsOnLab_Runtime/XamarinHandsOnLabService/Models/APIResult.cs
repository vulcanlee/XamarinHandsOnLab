using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XamarinHandsOnLabService.Models
{
    public class APIResult
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";
        public object Payload { get; set; }
    }
}