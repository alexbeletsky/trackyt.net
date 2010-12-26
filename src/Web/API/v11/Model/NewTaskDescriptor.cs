using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.API.v11.Model
{
    public class TaskDescriptor
    {
        public int Id { set; get; }
        public string Description { set; get; }
        public int Status { set; get; }
        public DateTime? CreatedDate { set; get; }
        public DateTime? StartedDate { set; get; }
        public DateTime? StoppedDate { set; get; }
    }

    public class OperationResult
    {
        public int Id { set; get; }
        public DateTime? CreatedDate { set; get; }
    }
}