using System.Web;

namespace Trackyt.Core.Services.Impl
{
    public class PathHelper : IPathHelper
    {
        public string VirtualToAbsolute(string virtualPath)
        {
            return VirtualPathUtility.ToAbsolute(virtualPath);
        }
    }
}