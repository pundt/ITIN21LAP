using hearthstone.data;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace hearthstone.logic
{
    public class DeckAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<Deck> GetUserDecks(string username)
        {
            log.Info("DeckAdministration - GetUserDecks(username)");
            List<Deck> userDecks = null;

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            try
            {
                using (var context = new clonestoneEntities())
                {
                    User user = context.AllUsers
                        .Include(x => x.AllDecks)
                        .Include(x => x.AllDecks.Select(y => y.AllDeckCards))
                        .FirstOrDefault(x => x.Username.Equals(username));

                    if (user == null)
                        throw new ArgumentException("Invalid username", nameof(username));

                    userDecks = user.AllDecks.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("DeckAdministration - GetUserDecks(username) - Exception", ex);
                if (ex.InnerException != null)
                    log.Error("DeckAdministration - GetUserDecks(username) - Exception (inner)", ex.InnerException);

                Debugger.Break();
                throw ex;
            }

            return userDecks;
        }

        public static bool AddCardToDeck(int idDeck, int idCard)
        {
            log.Info("DeckAdministration - AddCardToDeck(idDeck, idCard)");
            bool result = false;

            if (idDeck < 1)
                throw new ArgumentException("Invalid idDeck");
            if (idCard < 1)
                throw new ArgumentException("Invalid idCard");

            try
            {
                using (var context = new clonestoneEntities())
                {
                    Deck deck = context.AllDecks.FirstOrDefault(x => x.ID == idDeck);

                    if (deck == null)
                        throw new ArgumentException("Invalid idDeck");

                    Card card = context.AllCards.FirstOrDefault(x => x.ID == idCard);

                    if (card == null)
                        throw new ArgumentException("Invalid idCard");

                    /// check if card is already present in deck
                    DeckCard deckCard = deck.AllDeckCards.FirstOrDefault(x => x.ID_Card == idCard);

                    /// if so
                    if (deckCard != null)
                    {
                        /// increase number
                        deckCard.NumberOfCards++;
                    }
                    else
                    {
                        /// else, add new entry
                        deckCard = new DeckCard()
                        {
                            Card = card,
                            Deck = deck
                        };
                        deck.AllDeckCards.Add(deckCard);
                    }

                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                log.Error("DeckAdministration - AddCardToDeck(idDeck, idCard) - Exception", ex);
                if (ex.InnerException != null)
                    log.Error("DeckAdministration - AddCardToDeck(idDeck, idCard) - Exception (inner)", ex.InnerException);

                Debugger.Break();
                throw ex;
            }


            return result;
        }

        public static bool RemoveCardFromDeck(int idDeck, int idCard)
        {
            log.Info("DeckAdministration - RemoveCardFromDeck(idDeck, idCard)");
            bool result = false;

            if (idDeck < 1)
                throw new ArgumentException("Invalid idDeck");
            if (idCard < 1)
                throw new ArgumentException("Invalid idCard");

            try
            {
                using (var context = new clonestoneEntities())
                {
                    Deck deck = context.AllDecks.FirstOrDefault(x => x.ID == idDeck);

                    if (deck == null)
                        throw new ArgumentException("Invalid idDeck");

                    Card card = context.AllCards.FirstOrDefault(x => x.ID == idCard);

                    if (card == null)
                        throw new ArgumentException("Invalid idCard");

                    /// get existing deckCard
                    DeckCard deckCard = deck.AllDeckCards.FirstOrDefault(x => x.ID_Card == idCard);

                    /// if so
                    if (deckCard != null)
                    {
                        /// decrease number
                        deckCard.NumberOfCards--;

                        /// remove entry if necessary
                        if (deckCard.NumberOfCards == 0)
                        {
                            deck.AllDeckCards.Remove(deckCard);
                        }
                    }

                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                log.Error("DeckAdministration - RemoveCardFromDeck(idDeck, idCard) - Exception", ex);
                if (ex.InnerException != null)
                    log.Error("DeckAdministration - RemoveCardFromDeck(idDeck, idCard) - Exception (inner)", ex.InnerException);

                Debugger.Break();
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Returns a list of deckCards assigned to a given idDeck
        /// </summary>
        /// <param name="idDeck"></param>
        /// <returns></returns>
        public static List<DeckCard> GetDeckCardsForDeck(int idDeck)
        {
            log.Info("DeckAdministration - GetDeckCardsForDeck(idDeck)");
            List<DeckCard> deckCards = null;

            if (idDeck < 1)
                throw new ArgumentException("Invalid idDeck");

            try
            {
                using (var context = new clonestoneEntities())
                {
                    Deck deck = context.AllDecks
                        .Include(x => x.AllDeckCards) /// eager loading of deckCards
                        .Include(x => x.AllDeckCards.Select(y => y.Card)) /// eager loading of cards
                        .FirstOrDefault(x => x.ID == idDeck);

                    if (deck == null)
                        throw new ArgumentException("Invalid idDeck");

                    deckCards = deck.AllDeckCards.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("DeckAdministration -GetDeckCardsForDeck(idDeck) - Exception", ex);
                if (ex.InnerException != null)
                    log.Error("DeckAdministration - GetDeckCardsForDeck(idDeck) - Exception (inner)", ex.InnerException);

                Debugger.Break();
                throw ex;
            }

            return deckCards;
        }

        /// <summary>
        /// Returns a list of UserCards available for a given idDeck
        /// </summary>
        /// <param name="idDeck"></param>
        /// <returns></returns>
        public static List<UserCard> GetCardsForDeck(string username, int idDeck)
        {
            log.Info("DeckAdministration - GetDeckCards(username, idDeck)");
            List<UserCard> userCards = null;

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));
            if (idDeck < 1)
                throw new ArgumentException("Invalid idDeck");


            try
            {
                using (var context = new clonestoneEntities())
                {
                    User user = context.AllUsers.FirstOrDefault(x => x.Username.Equals(username));

                    if (user == null)
                        throw new ArgumentException("Invalid username");

                    /// get ALL cards assigned to this user
                    userCards = user.AllUserCards.ToList();

                    /// get deck for given idDeck
                    Deck deck = context.AllDecks.FirstOrDefault(x => x.ID == idDeck);

                    if (deck == null)
                        throw new ArgumentException("Invalid idDeck");

                    /// iterate over all userCards
                    foreach (var userCard in userCards)
                    {
                        /// check if current userCard is already present in current deck
                        DeckCard deckCard = deck.AllDeckCards.FirstOrDefault(x => x.ID_Card == userCard.ID_Card);

                        /// if card is already in deck
                        if (deckCard!=null)
                            /// decrease number of cards available
                            userCard.NumberOfCards -= deckCard.NumberOfCards;
                    }

                    /// return only those cards, whose number is greater than 0
                    userCards = userCards.Where(x => x.NumberOfCards > 0).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("DeckAdministration - GetDeckCards(username, idDeck) - Exception", ex);
                if (ex.InnerException != null)
                    log.Error("DeckAdministration - GetDeckCards(username, idDeck) - Exception (inner)", ex.InnerException);

                Debugger.Break();
                throw ex;
            }

            return userCards;
        }
    }
}
