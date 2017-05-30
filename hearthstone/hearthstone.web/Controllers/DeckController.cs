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
using log4net;

namespace hearthstone.web.Controllers
{
    [Authorize]
    public class DeckController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        public ActionResult Index()
        {
            log.Info("GET - Deck - Index");
            /// shows a list of available decks for current logged on user
            string username = User.Identity.Name;
            List<DeckOverviewModel> model = new List<DeckOverviewModel>();

            try
            {
                List<Deck> userDecks = DeckAdministration.GetUserDecks(username);

                foreach (var userDeck in userDecks)
                {
                    model.Add(new DeckOverviewModel()
                    {
                        ID_Deck = userDeck.ID,
                        Name = userDeck.Name,
                        ImageUrl = Url.Content("~/Content/images/deck.png"), /// static image 
                        NumberOfCardsInDeck = userDeck.AllDeckCards.Count
                    });
                }
            }
            catch (Exception)
            {
                TempData[Constants.MessageType.ERROR] = Messages.ERROR_COMMON;
            }
            
            return View(model);
        }
    }
}