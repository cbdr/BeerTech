using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeerTech.DataObjects;

namespace BeerTech.Repository {

    public class FridgeRepository : BaseRepository<Fridge> {

        public FridgeRepository()
            : base() {
        }

        public void Save(Fridge fridge) {
            Session.SaveOrUpdate(fridge);
        }

        public List<Fridge> getAllFridges() {
            return Session.CreateCriteria<Fridge>().List<Fridge>().ToList<Fridge>();
        }

        public Fridge getFridge(int fridgeID) {
            return Session.Get<Fridge>(fridgeID);
        }
    }
}