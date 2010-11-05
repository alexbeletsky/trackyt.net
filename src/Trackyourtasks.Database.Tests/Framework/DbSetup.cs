using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL.DataModel;
using System.Transactions;
using System.Configuration;

namespace Trackyourtasks.Core.Tests.Framework
{
    public class DbSetup : IDisposable
    {
        private TrackyDataContext _model = new TrackyDataContext(ConfigurationManager.ConnectionStrings["testdb"].ConnectionString);
        private TransactionScope _transaction = new TransactionScope();
 
        public DbSetup()
        {
            Init();
        }

        public TrackyDataContext Context { get { return _model; } }

        private void Init()
        {
            AddTestUser();
        }

        private void AddTestUser()
        {
            User = new User()
            {
                Email = "exists@test.com",
                Password = "test_pass2"
            };

            _model.Users.InsertOnSubmit(User);
            _model.SubmitChanges();
        }

        public User User { get; private set; }

        #region IDisposable Members

        public void Dispose()
        {
            _transaction.Dispose();
            _transaction = null;
        }

        #endregion
    }
}
