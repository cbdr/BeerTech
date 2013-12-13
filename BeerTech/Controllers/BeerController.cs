using BeerTech.API;
using BeerTech.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerTech.Controllers
{
    public class BeerController : Controller
    {
        //
        // GET: /Beer/

        public ActionResult GetBeer()
        {
            var id = Request.QueryString.Get("id");

            var request = new RestRequest();
            request.Resource = "v2/beer/{id}/";
            request.AddParameter("id", id, ParameterType.UrlSegment);

            var client = new APIClient<BeerInfo>();

            return Json(client.Execute<BeerInfo>(request), JsonRequestBehavior.AllowGet);
        }

    }
}
