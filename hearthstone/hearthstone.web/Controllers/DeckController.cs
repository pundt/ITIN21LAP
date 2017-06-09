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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            log.Info("GET - Deck - Edit");
            ActionResult editResult = null;
            DeckDetailModel model = new DeckDetailModel();

            if (id < 1)
                editResult = HttpNotFound();
            else
            {

                string username = User.Identity.Name;
                int idDeck = id;

                try
                {
                    /// get cards available for this deck
                    List<UserCard> userCardsForDeck = DeckAdministration.GetCardsForDeck(username, idDeck);

                    model.CardsForDeck = new List<CardModel>();
                    foreach (var cardForDeck in userCardsForDeck)
                    {
                        model.CardsForDeck.Add(new CardModel()
                        {
                            ID = cardForDeck.ID_Card,
                            Name = cardForDeck.Card.Name,
                            Mana = cardForDeck.Card.ManaCost,
                            Attack = cardForDeck.Card.Attack
                        });
                    }

                    /// get cards already in this deck
                    List<DeckCard> userCardsInDeck = DeckAdministration.GetCardsInDeck(idDeck);

                    model.CardsInDeck = new List<CardModel>();
                    foreach (var cardInDeck in userCardsInDeck)
                    {
                        model.CardsInDeck.Add(new CardModel()
                        {
                            ID = cardInDeck.ID_Card,
                            Name = cardInDeck.Card.Name,
                            Mana = cardInDeck.Card.ManaCost,
                            Attack = cardInDeck.Card.Attack
                        });
                    }

                    /// get current deck
                    Deck deck = DeckAdministration.GetUserDecks(username).Where(x => x.ID == id).FirstOrDefault();
                    model.DeckOverview = new DeckOverviewModel()
                    {
                        ID_Deck = deck.ID,
                        Name = deck.Name,
                        NumberOfCardsInDeck = deck.AllDeckCards.Count,
                        ImageUrl = Url.Content("~/Content/images/deck.png"), /// static image 
                    };

                    /// return view with model containing cards available and cards already assigned
                    editResult = View(model);
                }
                catch (Exception ex)
                {
                    TempData[Constants.MessageType.ERROR] = Messages.ERROR_COMMON;
                }
            }

            return editResult;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int idCard, int idDeck)
        {
            log.Info("POST - Deck - Add");

            try
            {
                bool result = DeckAdministration.AddCardToDeck(idDeck, idCard);

                if (result)
                    TempData[Constants.MessageType.SUCCESS] = Messages.CARD_ADD_SUCCESS;
                else
                    TempData[Constants.MessageType.SUCCESS] = Messages.CARD_ADD_ERROR;
            }
            catch (Exception)
            {
                TempData[Constants.MessageType.SUCCESS] = Messages.ERROR_COMMON;
            }

            return RedirectToAction("Edit", new { id = idDeck });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int idCard, int idDeck)
        {
            log.Info("POST - Deck - Remove");


            TempData[Constants.MessageType.SUCCESS] = Messages.CARD_REMOVE_SUCCESS;

            try
            {
                bool result = DeckAdministration.RemoveCardFromDeck(idDeck, idCard);

                if (result)
                    TempData[Constants.MessageType.SUCCESS] = Messages.CARD_REMOVE_SUCCESS;
                else
                    TempData[Constants.MessageType.SUCCESS] = Messages.CARD_REMOVE_ERROR;
            }
            catch (Exception)
            {
                TempData[Constants.MessageType.SUCCESS] = Messages.ERROR_COMMON;
            }

            return RedirectToAction("Edit", new { id = idDeck });
        }
    }
}