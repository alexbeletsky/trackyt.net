using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trackyt.Core.DAL.DataModel
{
    partial class Task
    {
        partial void OnCreated()
        {
            Position = 1;
        }
    }
}
