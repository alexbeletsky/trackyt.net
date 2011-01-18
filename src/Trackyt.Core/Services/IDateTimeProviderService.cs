using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trackyt.Core.Services
{
    public interface IDateTimeProviderService
    {
        DateTime UtcNow { get; }
    }
}
