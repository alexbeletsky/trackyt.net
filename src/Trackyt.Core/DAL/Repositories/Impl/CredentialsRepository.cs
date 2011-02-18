using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.DataModel;
using System.Configuration;

namespace Trackyt.Core.DAL.Repositories.Impl
{
    public class CredentialsRepository : ICredentialsRepository
    {
        private TrackytDataContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public CredentialsRepository() : this(
            new TrackytDataContext(ConfigurationManager.ConnectionStrings["tracytdb"].ConnectionString))
        {

        }

        /// <summary>
        /// Constructor used in unit tests
        /// </summary>
        /// <param name="context">Context</param>
        public CredentialsRepository(TrackytDataContext context)
        {
            _context = context;
        }

        public IQueryable<DataModel.Credential> Credentials
        {
            get 
            {
                return _context.Credentials;
            }
        }
    }
}
