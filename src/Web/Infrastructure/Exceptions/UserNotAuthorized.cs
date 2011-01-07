using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Infrastructure.Exceptions
{
    public class UserNotAuthorized : Exception
    {
        public UserNotAuthorized() : base("User is not authorized on Trackyt.net.")
        {

        }
    }
}