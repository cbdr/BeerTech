using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeerTech.Utility;

namespace BeerTech.DataObjects
{
    [Serializable]
    public class BeverageRequest
    {
        public virtual string ID { get; protected set; }
        public virtual string UserID { get; set; }
        public virtual string FridgeID { get; set; }
        public virtual string BeverageTitle { get; set; }
        public virtual string BeerAPIID { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime RequestDate { get; set; }

        public enum Statuses
        {
            Submitted, Bought, Rejected, NotFound
        }

        public BeverageRequest()
        {
            ID = new IDGenerator().GetNewID("BR");
        }
    }
}