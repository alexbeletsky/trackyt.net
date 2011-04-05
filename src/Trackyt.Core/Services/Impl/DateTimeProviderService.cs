using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trackyt.Core.Services.Impl
{
    public class DateTimeProviderService : IDateTimeProviderService
    {
        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}
