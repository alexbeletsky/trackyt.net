using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Trackyourtasks.Core.Views;
using Trackyourtasks.Core.DAL;
using Trackyourtasks.Core.DAL.DataModel;
using Trackyourtasks.Core.DAL.Repositories;

namespace Trackyourtasks.Core.BLL
{
    public class Registration
    {
        private IRegistrationView _view;
        private IUsersRepository _repository;

        public Registration(IRegistrationView view, IUsersRepository data)
        {
            _view = view;
            _repository = data;
        }

        public void RegisterUser(string email, string secret, string password)
        {
            try
            {
                if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(secret) || String.IsNullOrEmpty(password))
                {
                    _view.Fail("Sorry, but user provider input values are not valid.");
                    return;
                }

                if (_repository.FindUserByEmail(email) != null)
                {
                    _view.Fail("Sorry, but user with same e-mail is already registered on site.");
                    return;
                }

                var user = new User()
                {
                    Email = email,
                    SecretPhrase = secret,
                    Password = password
                };

                _repository.SaveUser(user);
                _view.Success();
            }
            catch (Exception )
            {
                _view.Fail("Sorry, but unexpected exception happened during operation.");
            }
        }
    }
}
