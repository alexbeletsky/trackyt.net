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
        public int id { get; set; }
        public string description { get; set; }
        public int? status { get; set; }
        public DateTime? createdDate { get; set; }
        public DateTime? startedDate { get; set; }
        public DateTime? stoppedDate { get; set; }
        public DateTime? plannedDate { get; set; }
        public int spent { get; set; }
        public int position { get; set; }
        public bool done { get; set; }
    }
}