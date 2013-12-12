using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeerTech.DataObjects;
using BeerTech.Repository;

namespace BeerTech.Controllers
{
    public class BeverageRequestController : Controller
    {
        //
        // POST: /BeverageRequest/

        public ActionResult Request()
        {
            //var bevRequest = new BeverageRequest();
            //bevRequest.UserID = "TESTUSER";
            //bevRequest.FridgeID = "TESTFRIDGE";
            //bevRequest.BeverageTitle = "BEER";
            //bevRequest.BeerAPIID = "TESTID";
            //bevRequest.Status = "Submitted";
            //bevRequest.RequestDate = DateTime.Now;
            //var repo = new BeverageRequestRepository();
            //repo.Save(bevRequest);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

    }
}
