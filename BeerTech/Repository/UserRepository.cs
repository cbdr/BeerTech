using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeerTech.DataObjects;

namespace BeerTech.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository() : base() { }

        public User LoadByEmail(string Email)
        {
            return Session.QueryOver<User>().Where(x => x.Email == Email).SingleOrDefault();
        }
    }
}