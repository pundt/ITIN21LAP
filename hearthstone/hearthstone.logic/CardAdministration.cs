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
    public class CardAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Card GetCard(int id)
        {
            log.Info("CardAdministration - GetCard(id)");
            Card card = null;

            if (id < 1)
                throw new ArgumentException("invalid card id", nameof(id));

            try
            {
                using (var context = new clonestoneEntities())
                {
                    card = context.AllCards.FirstOrDefault(x => x.ID == id);
                }
            }
            catch (Exception ex)
            {
                log.Error("CardAdministration - GetCard(id) - Exception", ex);
                if (ex.InnerException != null)
                    log.Error("CardAdministration - GetCard(id) - Exception (inner)", ex.InnerException);

                Debugger.Break();
                throw ex;
            }

            return card;
        }
    }
}
