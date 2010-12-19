using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trackyt.Core.Services
{
    public interface IApiService
    {
        string GetApiToken(string email, string password);
        int GetUserIdByApiToken(string apiToken);
    }
}
