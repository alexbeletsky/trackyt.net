using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.DAL.Repositories
{
    public interface ICredentialsRepository
    {
        IQueryable<Credential> Credentials { get; }
    }
}
