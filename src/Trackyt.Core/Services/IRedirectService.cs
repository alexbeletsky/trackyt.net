using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Trackyt.Core.Services
{
    public interface IRedirectService
    {
        RedirectResult ToDashboard(string email);
        RedirectResult ToRegistrationSuccess();
        RedirectResult ToUrl(string url);
    }
}
