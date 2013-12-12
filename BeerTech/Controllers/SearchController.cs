using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeerTech.API;
using BeerTech.Models;
using RestSharp;

namespace BeerTech.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult SearchBeers()
        {
            var keyword = Request.QueryString["keyword"];
            var page = Request.QueryString["page"];
            var request = new RestRequest();
            request.Resource = "/v2/search/";
            

            if (!string.IsNullOrEmpty(keyword))
            {
                request.AddParameter("q", keyword);
            }
            if (!string.IsNullOrEmpty(page))
            {
                request.AddParameter("p", page);
            }

            var client = new APIClient<SearchResults>();

            return Json(client.Execute<SearchResults>(request), JsonRequestBehavior.AllowGet);
        }

    }
}
