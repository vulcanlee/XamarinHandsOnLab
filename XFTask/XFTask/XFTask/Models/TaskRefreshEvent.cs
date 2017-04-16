using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFTask.Models
{

    public class TaskRefreshEventEvent : PubSubEvent<TaskRefreshEventPayload>
    {

    }

    public class TaskRefreshEventPayload
    {
        public string Account { get; set; } = "";
    }
}
