using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL.DataModel;

namespace WebApplication.Tests.Framework
{
    public class DbScript : IDisposable
    {
        private TrackYourTasksDataContext _model = new TrackYourTasksDataContext();

        public DbScript()
        {
            Init();
        }

        private void Init()
        {
            AddTestUser();
        }

        private void AddTestUser()
        {
            var user = new User()
            {
                Email = "exists@test.com",
                SecretPhrase = "bla-bla",
                Password = "test_pass2"
            };

            _model.Users.InsertOnSubmit(user);
            _model.SubmitChanges();
        }

        #region IDisposable Members

        public void Dispose()
        {
            Clean();
        }

        private void Clean()
        {
            DeleteTestUsers();
        }

        private void DeleteTestUsers()
        {
            var users = from u in _model.Users where u.Password.StartsWith(@"test") select u;
            _model.Users.DeleteAllOnSubmit<User>(users);
            _model.SubmitChanges();
        }

        #endregion
    }
}
