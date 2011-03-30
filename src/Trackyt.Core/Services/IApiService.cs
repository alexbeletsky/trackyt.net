using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.Services
{
    public interface IApiService
    {
        string GetApiToken(string email, string password);
        User GetUserByApiToken(string apiToken);
    }
}
