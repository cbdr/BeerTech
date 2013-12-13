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

        public ActionResult Index()
        {
            return View();
        }
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

        //POST: /UpdateStatus
        [HttpPost]
        public ActionResult UpdateStatus()
        {
            var id = Request.Form.Get("id");
            var status = Request.Form.Get("status");
            var repo = new BeverageRequestRepository();
            var bevRequest = repo.LoadByKey(id);
            if(bevRequest != null)
            {
                bevRequest.Status = status;
                repo.Update(bevRequest);
            }
            return Json(bevRequest);
        }


        //POST: /CreateRequest
        [HttpPost]
        [Authorize]
        public ActionResult CreateRequest()
        {
            var bevRequest = new BeverageRequest();
            bevRequest.FridgeID = Request.Form.Get("FridgeID");
            bevRequest.BeverageTitle = Request.Form.Get("BeverageTitle");
            bevRequest.BeerAPIID = Request.Form.Get("BeerAPIID");
            bevRequest.Status = BeverageRequest.Statuses.Submitted.ToString();
            bevRequest.RequestDate = DateTime.Now;
            bevRequest.Email = HttpContext.User.Identity.Name;

            var repo = new BeverageRequestRepository();
            repo.Save(bevRequest);

            bevRequest = repo.LoadByKey(bevRequest.ID);

            return Json(bevRequest);
        }
    }
}
