﻿using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hearthstone.logic;
using hearthstone.data;
using hearthstone.web.App_Code;
using Resources;
using hearthstone.web.Models;

namespace hearthstone.web.Controllers
{
    public class ShopController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Packs()
        {
            log.Info("GET - Shop - Packs");
            PackOverviewModel model = null;


            try
            {
                /// get data from logic
                List<CardPack> packs = ShopAdministration.GetCardPacks();
                User currentUser = UserAdministration.GetUser(User.Identity.Name);

                /// MAP data to view model
                /// in this case mapping will be done manually
                /// BUT you may make use of an automapper too
                model = new PackOverviewModel()
                {
                    AmountMoney = currentUser.AmountMoney
                };

                List<PackModel> packsModel = new List<PackModel>();

                foreach (var pack in packs)
                {
                    packsModel.Add(new PackModel()
                    {
                        Description = pack.Name,
                        Name = pack.Name,
                        Price = pack.Price,
                        ID = pack.ID
                    });
                }
                model.Packs = packsModel;

            }
            catch (Exception)
            {
                TempData[Constants.MessageType.ERROR] = Messages.ERROR_COMMON;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Buy(int id)
        {
            ActionResult result = RedirectToAction("Packs", "Shop");

            if (id <= 0)
            {
                TempData[Constants.MessageType.WARNING] = Messages.INVALID_PACK_NUMBER;
            }

            string username = User.Identity.Name;
            try
            {
                switch (ShopAdministration.BuyPack(id, username))
                {
                    case BuyResult.Success:
                        TempData[Constants.MessageType.SUCCESS] = Messages.BUY_PACK_SUCCESS;
                        break;
                    case BuyResult.NotEnoughMoney:
                        TempData[Constants.MessageType.WARNING] = Messages.NOT_ENOUGH_MONEY;
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

        [HttpGet]
        [Authorize]
        public ActionResult Pay()
        {
            return View();
        }
    }
}