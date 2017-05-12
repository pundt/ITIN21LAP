using hearthstone.logic;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hearthstone.data;

namespace hearthstone.web.Controllers
{
    public class ImageController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Pack(int id = -1)
        {
            log.Info("GET - Image - Pack");
            ActionResult result = HttpNotFound();

            if (id > 0)
            {
                /// get according pack from datastorage
                try
                {
                    CardPack pack = ShopAdministration.GetCardPack(id);
                    if (pack!=null && pack.Image!=null)
                    {
                        result = File(pack.Image, "image/jpg");
                    }
                }
                catch (Exception)
                {}
            }           

            return result;
        }
    }
}