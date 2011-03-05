using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.API.v11.Model
{
    public class StartStopOperationResult
    {
        public int id { get; set; }
        public DateTime? startedDate { get; set; }
        public DateTime? stoppedDate { get; set; }
    }
}