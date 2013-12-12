using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeerTech.DataObjects;
using FluentNHibernate.Mapping;

namespace BeerTech.DataObjectMaps {

    internal sealed class FridgeMap : ClassMap<Fridge> {

        public FridgeMap() {
            Table("Fridges");
            Id(x => x.ID);
            Map(x => x.Name).CustomType("AnsiString").Length(75);
        }
    }
}