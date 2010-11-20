using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    //TODO: add validation data notation
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}