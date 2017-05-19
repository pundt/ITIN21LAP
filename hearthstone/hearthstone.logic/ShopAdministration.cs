using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hearthstone.data;
using log4net;
using System.Diagnostics;

namespace hearthstone.logic
{
    public enum BuyResult
    {
        Success,
        NotEnoughMoney
    }
    public class ShopAdministration
    {
        private static Random RandomNumberGenerator = new Random();
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<CardPack> GetCardPacks()
        {
            log.Info("ShopAdministration - GetCardPacks()");
            List<CardPack> allCardPacks = null;

            try
            {
                using (var context = new clonestoneEntities())
                {
                    allCardPacks = context.AllCardPacks.OrderBy(x => x.NumberOfCards).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("ShopAdministration - GetCardPacks() - Exception", ex);
                if (ex.InnerException != null)
                    log.Error("ShopAdministration - GetCardPacks() - Exception (inner)", ex.InnerException);

                Debugger.Break();
                throw ex;
            }

            return allCardPacks;
        }

        public static CardPack GetCardPack(int id)
        {
            log.Info("ShopAdministration - GetCardPack(id)");
            CardPack cardPack = null;

            if (id < 1)
                throw new ArgumentException("invalid pack id", nameof(id));

            try
            {
                using (var context = new clonestoneEntities())
                {
                    cardPack = context.AllCardPacks.FirstOrDefault(x => x.ID == id);
                }
            }
            catch (Exception ex)
            {
                log.Error("ShopAdministration - GetCardPack(id) - Exception", ex);
                if (ex.InnerException != null)
                    log.Error("ShopAdministration - GetCardPack(id) - Exception (inner)", ex.InnerException);

                Debugger.Break();
                throw ex;
            }

            return cardPack;
        }

        /// <summary>
        /// creates a pack with a number of cards
        /// cards will be chosen at random
        /// random cards may be duplicates
        /// assign cards to user
        /// decrease money of user by price of pack
        /// add new virtual purchase entry
        /// </summary>
        /// <param name="idCardPack"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public static BuyResult BuyPack(int idCardPack, string username)
        {
            log.Info("ShopAdministration - BuyPack(idCardPack, username)");
            BuyResult result = BuyResult.Success;

            if (idCardPack <= 0)
                throw new ArgumentException("Invalid Value", nameof(idCardPack));
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            try
            {
                using (var context = new clonestoneEntities())
                {
                    User user = context.AllUsers.FirstOrDefault(x => x.Username == username);
                    if (user == null)
                        throw new ArgumentException("Invalid value", nameof(username));

                    CardPack cardPack = context.AllCardPacks.FirstOrDefault(x => x.ID == idCardPack);
                    if (cardPack == null)
                        throw new ArgumentException("Invalid value", nameof(idCardPack));

                    if (user.AmountMoney < cardPack.Price)
                    {
                        log.Debug($"User {username} has not enough money for Pack {idCardPack}");
                        result = BuyResult.NotEnoughMoney;
                    }
                    else
                    {
                        log.Debug($"Reduce money of user by {cardPack.Price}");
                        user.AmountMoney -= cardPack.Price;

                        VirtualPurchase purchase = new VirtualPurchase()
                        {
                            ID_CardPack = idCardPack,
                            ID_User = user.ID,
                            NumberOfPacks = 1,
                            PurchaseDate = DateTime.Now
                        };
                        context.AllVirtualPurchases.Add(purchase);
                        log.Debug($"Added new VirtualPurchas for user {username}");

                        int count = context.AllCards.Count();

                        log.Debug($"Start creating random cards for pack {idCardPack}");
                        /// create cards at random
                        for (int numberOfCard = 0; numberOfCard < cardPack.NumberOfCards; numberOfCard++)
                        {
                            /// get a valid idCard (generated by random)
                            Card randomCard = context.AllCards.OrderBy(x => x.ID).Skip(RandomNumberGenerator.Next(0, count)).Take(1).Single();
                            log.Debug($"\tRandomCard {randomCard.Name} (ID: {randomCard.ID})");

                            /// save new card to userCards

                            /// if card is already an userCard
                            /// increase number
                            UserCard userCard = user.AllUserCards.Where(x => x.ID_Card == randomCard.ID).FirstOrDefault();
                            if (userCard != null)
                            {
                                log.Debug($"\t\tUserCard already exists - increase numberOfCards");
                                userCard.NumberOfCards++;
                            }
                            else       /// else - add new userCard
                            {
                                log.Debug($"\t\tAdd NEW UserCard");
                                userCard = new UserCard()
                                {
                                    ID_User = user.ID,
                                    ID_Card = randomCard.ID,
                                    NumberOfCards = 1
                                };
                                context.AllUserCards.Add(userCard);
                            }
                        }
                        context.SaveChanges();
                        log.Debug($"Finished adding random cards!");                        
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("ShopAdministration - BuyPack(idCardPack, username) - Exception", ex);
                if (ex.InnerException != null)
                    log.Error("ShopAdministration - BuyPack(idCardPack, username) - Exception (inner)", ex.InnerException);

                Debugger.Break();
                throw ex;
            }

            return result;
        }
    }
}
