using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Admin.Models
{
    public class TaskSummaryModel
    {
        public int TotalTasks { get; set; }
        public int TotalLoggedTime { get; set; }
    }
}