using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeerTech.Utility;

namespace BeerTech.DataObjects {

    public class Fridge {

        public virtual string ID { get; protected set; }
        public virtual string Name { get; set; }

        public Fridge() {
            ID = new IDGenerator().GetNewID("FG");
        }
    }
}