using hearthstone.logic;
using hearthstone.web.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

            switch (UserAdministration.Register(
                model.FirstName, 
                model.LastName, 
                model.Mail, 
                model.Username, 
                model.Password, 
                model.Avatar))
            {
                case RegisterResult.NotSet:
                    break;
                case RegisterResult.Successful:
                    break;
                case RegisterResult.UsernameExists:
                    break;
                default:
                    break;
            }

            return View();
        }
    }
}