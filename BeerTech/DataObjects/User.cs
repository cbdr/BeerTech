using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeerTech.Utility;

namespace BeerTech.DataObjects {

    public class User {

        public virtual string UserID { get; protected set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string PasswordSalt { get; set; }

        public User() {
            UserID = new IDGenerator().GetNewID("UI");
        }
    }
}