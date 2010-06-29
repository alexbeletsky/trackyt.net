using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL;

namespace Trackyourtasks.Core.BLL
{
    public class ForgotPassword
    {
        private IUsersRepository _data;

        public ForgotPassword(IUsersRepository data)
        {
            _data = data;
        }

        public string RestorePassword(string email, string secret)
        {
            var user = _data.FindUserByEmail(email);

            if (user == null)
                return null;

            if (user.SecretPhrase == secret)
                return user.Password;

            return null;
        }
    }
}
