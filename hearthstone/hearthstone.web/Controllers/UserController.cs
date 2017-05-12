using hearthstone.data;
using hearthstone.logic;
using hearthstone.web.App_Code;
using hearthstone.web.Models;
using log4net;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace hearthstone.web.Controllers
{
    public class UserController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        public ActionResult Index()
        {
            log.Info("GET - UserController - Index");
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            log.Info("GET - UserController - Register");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            log.Info("POST - UserController - Register");
            ActionResult result = View(model);

            try
            {
                switch (UserAdministration.Register(
                    model.FirstName,
                    model.LastName,
                    model.GamerTag,
                    model.Username,
                    model.Password))
                {
                    case RegisterResult.Successful:
                        TempData[Constants.MessageType.SUCCESS] = Messages.SUCCESS_REGISTER;
                        result = RedirectToAction("Index", "Shop");
                        break;
                    case RegisterResult.UsernameExists:
                        TempData[Constants.MessageType.WARNING] = Messages.WARNING_USERNAME_EXISTS;
                        break;
                    case RegisterResult.NotSet:
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                TempData[Constants.MessageType.ERROR] = Messages.ERROR_REGISTER;
            }
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            log.Info("POST - UserController - Login");
            ActionResult result = RedirectToAction("Index", "Shop");

            string returnUrl = Request.Params["ReturnUrl"];
            if (returnUrl != null)
                result = Redirect(returnUrl);

            try
            {
                switch (UserAdministration.Login(model.Username, model.Password))
                {
                    case LoginResult.Successful:
                        #region get user roles, create authentication ticket, encrypt it
                        /// setze auth cookie
                        UserRole userRole = RolesAdministration.GetUserRole(model.Username);
                        if (userRole != null)
                        {
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                                model.Username,
                                DateTime.Now,
                                DateTime.Now.AddMinutes(30),
                                true,
                                userRole.Name,
                                FormsAuthentication.FormsCookiePath);

                            // Encrypt the ticket.
                            string encTicket = FormsAuthentication.Encrypt(ticket);

                            // Create the cookie.
                            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                        }

                        TempData[Constants.MessageType.SUCCESS] = Messages.SUCCESS_LOGIN;
                        break;
                    #endregion
                    case LoginResult.InvalidPassword:
                    case LoginResult.InvalidUsername:
                        TempData[Constants.MessageType.WARNING] = Messages.ERROR_LOGIN;
                        break;
                    case LoginResult.NotSet:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                TempData[Constants.MessageType.ERROR] = Messages.ERROR_COMMON;
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Logout()
        {
            log.Info("POST - UserController - Logout");
            ActionResult result = RedirectToAction("Index", "Shop");

            /// set authentication cookie as expired!
            FormsAuthentication.SignOut();

            return result;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit()
        {
            log.Info("GET - UserController - Edit");
            ActionResult result = View();

            string username = User.Identity.Name;

            try
            {
                User currentUser = UserAdministration.GetUser(username);

                /// map userinformation to UserEditModel

                /// pass it to view()

            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException || ex is ArgumentException)
                    TempData[Constants.MessageType.ERROR] = Messages.ERROR_UNKNOWN_USER;
                else if (ex is Exception)
                    TempData[Constants.MessageType.ERROR] = Messages.ERROR_COMMON;

                result = RedirectToAction("Index", "Shop");
            }

            return result;
        }
    }
}