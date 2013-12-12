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
        // GET: /GetRequest/

        public ActionResult GetRequest()
        {
            var id = Request.QueryString["id"];
            var repo = new BeverageRequestRepository();
            var bevRequest = repo.LoadByKey(id);
            return Json(bevRequest, JsonRequestBehavior.AllowGet);
        }

        
        //GET: /GetAllRequests
        public ActionResult GetAllRequests()
        {
            return Json(new BeverageRequestRepository().GetAllRequests(), JsonRequestBehavior.AllowGet);
        }


        //POST: /CreateRequest
        [HttpPost]
        public ActionResult CreateRequest()
        {
            var bevRequest = new BeverageRequest();
            bevRequest.UserID = Request.Form.Get("UserID");
            bevRequest.FridgeID = Request.Form.Get("FridgeID");
            bevRequest.BeverageTitle = Request.Form.Get("BeverageTitle");
            bevRequest.BeerAPIID = Request.Form.Get("BeerAPIID");
            bevRequest.Status = BeverageRequest.Statuses.Submitted.ToString();
            bevRequest.RequestDate = DateTime.Now;

            var repo = new BeverageRequestRepository();
            repo.Save(bevRequest);

            bevRequest = repo.LoadByKey(bevRequest.ID);

            return Json(bevRequest);
        }





    }
}
