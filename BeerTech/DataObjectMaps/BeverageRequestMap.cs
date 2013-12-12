//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using BeerTech.DataObjects;
using FluentNHibernate.Mapping;
namespace BeerTech.DataObjectMaps
{
    internal sealed class BeverageRequestMap : ClassMap<BeverageRequest>
    {
        public BeverageRequestMap()
        {
            Table("BeverageRequests");
            Id(x => x.ID);
            Map(x => x.FridgeID).CustomType("AnsiString").Length(20);
            Map(x => x.BeverageTitle).CustomType("AnsiString").Length(75);
            Map(x => x.BeerAPIID).CustomType("AnsiString").Length(50);
            Map(x => x.Status).CustomType("AnsiString").Length(50);
            Map(x => x.RequestDate);
            Map(x => x.Email).CustomType("AnsiString").Length(150);

        }
    }
}