using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trackyt.Core.Services
{
    public interface IShareService
    {
        string CreateShareLink(string email);
        bool ValidateShareKey(string email, string key);
    }
}
