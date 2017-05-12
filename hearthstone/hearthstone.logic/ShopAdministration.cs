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
    public class ShopAdministration
    {
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

    }
}
