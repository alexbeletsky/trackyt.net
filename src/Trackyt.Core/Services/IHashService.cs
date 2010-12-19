using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trackyt.Core.Services
{
    public interface IHashService
    {
        string CreateMD5Hash(string value);
        bool ValidateMD5Hash(string value, string hash);

        string CreateApiToken(string email, string password);
    }
}
