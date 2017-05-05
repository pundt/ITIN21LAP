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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            log.Info("POST - UserController - Login");
            ActionResult result = RedirectToAction("Index", "Shop");

            try
            {
                switch (UserAdministration.Login(model.Username, model.Password))
                {

                    case LoginResult.Successful:
                        TempData[Constants.MessageType.SUCCESS] = Messages.SUCCESS_LOGIN;

                        /// setze auth cookie

                        break;
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
    }
}