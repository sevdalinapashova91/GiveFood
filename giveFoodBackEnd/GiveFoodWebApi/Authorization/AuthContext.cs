using GiveFoodServices.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveFoodWebApi.Authorization
{
    public class AuthContext
    {
        public ProfileDto UserProfile { get; set; }

        public List<SimpleClaim> Claims { get; set; }
    }

    public class SimpleClaim
    {
        public string Value { get; set; }
        public string Type  { get; set; }
    }
}
