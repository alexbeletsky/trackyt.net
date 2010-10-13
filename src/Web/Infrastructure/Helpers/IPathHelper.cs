using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Infrastructure.Helpers
{
    public interface IPathHelper
    {
        string VirtualToAbsolute(string virtualPath);
    }
}