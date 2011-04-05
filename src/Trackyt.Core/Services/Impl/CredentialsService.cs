using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.Repositories;
using System.Net;

namespace Trackyt.Core.Services.Impl
{
    public class CredentialsService : ICredentialsService
    {
        private ICredentialsRepository _credentialsRepository;

        public CredentialsService(ICredentialsRepository credentialsRepository)
        {
            _credentialsRepository = credentialsRepository;
        }

        public NetworkCredential GetCredentialsForAccount(string account)
        {
            var credential = _credentialsRepository.Credentials.Where(c => c.Account == account).SingleOrDefault();
            if (credential != null)
            {
                return new NetworkCredential(credential.Account, credential.Password);
            }

            return null;
        }
    }
}
