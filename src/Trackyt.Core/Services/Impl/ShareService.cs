using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trackyt.Core.Services.Impl
{
    public class ShareService : IShareService
    {
        private IPathHelper _pathHelper;
        private IHashService _hashService;

        public ShareService(IPathHelper pathHelper, IHashService hashService)
        {
            _pathHelper = pathHelper;
            _hashService = hashService;
        }

        public string CreateShareLink(string email)
        {
            var pathToShare = _pathHelper.VirtualToAbsolute("~/user/share");
            var shareKey = _hashService.CreateMD5Hash(CreateShareKey(email));
            
            return string.Format(@"{0}/{1}?key={2}", pathToShare, email, shareKey);
        }

        public bool ValidateShareKey(string email, string key)
        {
            return _hashService.ValidateMD5Hash(CreateShareKey(email), key);
        }

        private string CreateShareKey(string email)
        {
            return email + "shared_tasks";
        }
    }
}
