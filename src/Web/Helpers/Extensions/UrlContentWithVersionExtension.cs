using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

namespace Web.Helpers.Extensions
{
    public static class UrlContentWithVersionExtension
    {
        private static string _currentAssemblyVersion;

        public static string ContentWithVersion(this UrlHelper helper, string path)
        {
            var contentPath = helper.Content(path);
            var assemblyVersionString = GetAssemblyVersionString();

            return string.Format("{0}?ver={1}", contentPath, assemblyVersionString);
        }

        private static string GetAssemblyVersionString()
        {
            if (_currentAssemblyVersion == null)
            {
                var currentAssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
                _currentAssemblyVersion = currentAssemblyVersion.ToString().Replace(".", "");
            }

            return _currentAssemblyVersion;
        }
    }
}