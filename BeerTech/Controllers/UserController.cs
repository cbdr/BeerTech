using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BeerTech.Authentication;
using BeerTech.DataObjects;
using BeerTech.Repository;
using BeerTech.Utility;
using System.Configuration;
using System.Net.Mail;

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
                return Json(new { signedin = false, msg = "You need a CB email address!"});
            }
            
            var txtPassword = Request.Form.Get("password");
            var hashAndSalt = new AuthenticationService().CreateCredentials(txtPassword);

            user.Password = hashAndSalt.Hash;
            user.PasswordSalt = hashAndSalt.Salt;

            var repo = new UserRepository();

            var userCheck = repo.LoadByEmail(user.Email);
            if (userCheck != null)
            {
                return Json(new { signedin = false, msg = "User already exists." });
            }
            else
            {
                repo.Save(user);
                user = repo.LoadByKey(user.UserID);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return Redirect("/BeverageRequest");
                }
                else
                {
                    return Json(new { signedin = false, msg = "There was a problem logging you in. If this persists... Try again later?" });
                }
            }   
        }

        [HttpGet]
        public ActionResult CheckLogin()
        {
            if (Request.IsAuthenticated)
            {
                string email = HttpContext.User.Identity.Name;
                return Json(new { signedin = true, username = email}, JsonRequestBehavior.AllowGet);
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
                FormsAuthentication.SetAuthCookie(user.Email, false);
                return Redirect("/BeverageRequest");
            }
            else
            {
                return Json(new { signedin = false, msg = "Login Unsuccesful, check credentials!" });
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

        public ActionResult PasswordRequest()
        {
            return View();
        }

        public ActionResult ResetPassword(string digest)
        {
            var parts = PasswordHelper.ValidateResetCode(HttpUtility.UrlDecode(digest));

            if (!parts.IsValid)
            {
                ViewBag.Message = "Invalid or expired link. Please try again.";
                ViewBag.ValidRequest = false;
                return View();
            }

            ViewBag.Message = "Thank you. You may enter your new password and confirm it below. Try not to forget this time.";
            ViewBag.Email = parts.User.Email;
            ViewBag.ValidRequest = true;
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword()
        {
            var repo = new UserRepository();
            var email = Request.Form.Get("email");
            var user = repo.LoadByEmail(email);
            var txtPassword = Request.Form.Get("password");
            var hashAndSalt = new AuthenticationService().CreateCredentials(txtPassword);

            user.Password = hashAndSalt.Hash;
            user.PasswordSalt = hashAndSalt.Salt;
            if (user != null && txtPassword != null)
            {
                repo.Update(user);
                return Json(new { success = true, msg = "Password successfully reset! Try logging in above." });
            }

            return Json(new { success = false, msg = "Error, password was not reset." });
        }
 

        [HttpPost]
        public ActionResult PasswordResetRequest()
        {
            var email = Request.Form.Get("email");
            var user = new UserRepository().LoadByEmail(email);

            if (user != null)
            {
                bool sent = PasswordHelper.SendResetEmail(user, HttpContext.Request.Url.ToString());
                if (!sent)
                {
                    return Json(new
                    {
                        success = false,
                        msg = "Error sending password reset request. Please try again later."
                    });
                }
            }
            //return this regardless so spammers cannot ascertaine real addresses
            return Json(new { success = true, msg = "Password reset request was sent to " + email.ToString() + 
                ", be sure to follow the directions in the email before your token expires in " + PasswordHelper.MinutesToExpiration + " minutes!" });
        }
    }
}
