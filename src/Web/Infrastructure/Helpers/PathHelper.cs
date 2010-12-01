using System.Web;

namespace Web.Infrastructure.Helpers
{
    [CoverageExcludeAttribute]
    public class PathHelper : IPathHelper
    {
        public string VirtualToAbsolute(string virtualPath)
        {
            return VirtualPathUtility.ToAbsolute(virtualPath);
        }
    }
}