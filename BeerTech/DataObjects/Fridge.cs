using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerTech.DataObjects {

    public class Fridge {

        public virtual string ID { get; protected set; }
        public virtual string Name { get; set; }
    }
}