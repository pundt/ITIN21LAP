using hearthstone.data;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hearthstone.logic
{
    public class DeckAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public List<Deck> GetUserDecks(string username)
        {
            log.Info("DeckAdministration - GetUserDecks(username)");
            List<Deck> userDecks = null;

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            try
            {
                using (var context = new clonestoneEntities())
                {
                    User user = context.AllUsers.FirstOrDefault(x => x.Username.Equals(username));

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

        public bool AddCardToDeck(int idDeck, int idCard)
        {
            log.Info("DeckAdministration - AddCardToDeck(idDeck, idCard)");
            bool result = false;


            return result;
        }

        public bool RemoveCardFromDeck(int idDeck, int idCard)
        {
            log.Info("DeckAdministration - RemoveCardFromDeck(idDeck, idCard)");
            bool result = false;


            return result;
        }

        public List<Card> GetUserCardsForDeck(int idDeck)
        {
            log.Info("DeckAdministration - GetUserCardsForDeck(idDeck)");
            List<Card> userCards = null;

            return userCards;
        }

        public List<Card> GetDeckCards(int idDeck)
        {
            log.Info("DeckAdministration - GetDeckCards(idDeck)");
            List<Card> deckCards = null;

            return deckCards;
        }
    }
}
