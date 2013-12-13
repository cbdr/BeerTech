using BeerTech.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerTech.Controllers
{
    public class FridgeController : Controller
    {
        //
        // GET: /Fridge/

        public ActionResult GetAllFridges()
        {
            return Json(new FridgeRepository().GetAllFridges(), JsonRequestBehavior.AllowGet);
        }

    }
}
