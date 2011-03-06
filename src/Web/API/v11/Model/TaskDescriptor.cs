using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.API.v11.Model
{
    public enum TaskStatus
    {
        None = 0,
        Started = 1,
        Stopped = 2
    }

    public class TaskDescriptor
    {
        public int id { set; get; }
        public string description { set; get; }
        public int? status { set; get; }
        public DateTime? createdDate { set; get; }
        public DateTime? startedDate { set; get; }
        public DateTime? stoppedDate { set; get; }
        public int spent { set; get; }
        public int position { get; set; }
    }
}