using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeerTech.DataObjects;

namespace BeerTech.Repository
{
    public class BeverageRequestRepository : BaseRepository<BeverageRequest>
    {
        public BeverageRequestRepository()
            : base()
        {
        }

        public void Save(BeverageRequest request)
        {
            Session.SaveOrUpdate(request);
        }
    }
}