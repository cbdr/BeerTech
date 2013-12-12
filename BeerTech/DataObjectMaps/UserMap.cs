using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeerTech.DataObjects;
using FluentNHibernate.Mapping;

namespace BeerTech.DataObjectMaps {

    public class UserMap : ClassMap<User> {

        public UserMap() {
            Table("Users");
            Id(x => x.UserID).CustomType("AnsiString").Length(20).GeneratedBy.Assigned();
            Map(x => x.Email).CustomType("AnsiString").Length(150);
            Map(x => x.Password).CustomType("AnsiString").Length(200);
            Map(x => x.PasswordSalt).CustomType("AnsiString").Length(200);
        }
    }
}