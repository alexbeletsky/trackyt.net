using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Trackyt.Core.Services
{
    public interface ICredentialsService
    {
        NetworkCredential GetCredentialsForAccount(string account); 
    }
}
