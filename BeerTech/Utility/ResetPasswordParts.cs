using BeerTech.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerTech.Utility
{
    public class ResetPasswordParts
    {

        public ResetPasswordParts() { IsValid = false; }

        public bool IsValid { get; set; }

        public User User { get; set; }

        public DateTime Expires { get; set; }

    }
}