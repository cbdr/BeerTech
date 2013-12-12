using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeerTech.DataObjects;

namespace BeerTech.Authentication
{
    public class AuthenticationService
    {
        public SaltedHash CreateCredentials(string Password)
        { 
            return new SaltedHash(Password);
        }

        public bool PasswordMatch(string Password, User user)
        {
            return SaltedHash.Verify(user.PasswordSalt, user.Password, Password);
        }
    }
}