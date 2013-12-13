using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BeerTech.Authentication;
using BeerTech.DataObjects;
using BeerTech.Repository;

namespace BeerTech.Controllers
{
    public class UserController : Controller
    {
        //
        // POST: /Create/
        [HttpPost]
        public ActionResult Create()
        {
            var user = new User();
            user.Email = Request.Form.Get("email");

            if (!user.Email.ToLower().EndsWith("@careerbuilder.com"))
            {
                return Json("non cb email not allowed");
            }
            
            var txtPassword = Request.Form.Get("password");
            var hashAndSalt = new AuthenticationService().CreateCredentials(txtPassword);

            user.Password = hashAndSalt.Hash;
            user.PasswordSalt = hashAndSalt.Salt;

            var repo = new UserRepository();

            var userCheck = repo.LoadByEmail(user.Email);
            if (userCheck != null)
            {
                return Json(new { signedin = false, msg = "User already exists" });
            }
            else
            {
                repo.Save(user);
                user = repo.LoadByKey(user.UserID);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.UserID, false);
                    return Json(new { signedin = true, msg = user.UserID });
                }
                else
                {
                    return Json(new { signedin = false, msg = "unknown error" });
                }
            }   
        }

        [HttpGet]
        public ActionResult CheckLogin()
        {
            if (Request.IsAuthenticated)
            {
                return Json(new { signedin = true, username = HttpContext.User.Identity}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { signedin = false, username = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Login()
        {
            var email = Request.Form.Get("email");
            var txtPassword = Request.Form.Get("password");

            var user = new UserRepository().LoadByEmail(email);

            if (user != null && new AuthenticationService().PasswordMatch(txtPassword, user))
            {
                FormsAuthentication.SetAuthCookie(user.UserID, false);
                return Json(new { signedin = true });
            }
            else
            {
                return Json(new { signedin = false });
            }

        }

        public ActionResult Logout()
        {
            if (Request.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                return Json(new { signedin = false, msg = "done" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { signedin = false, msg = "already logged out" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
