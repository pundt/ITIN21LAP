using hearthstone.data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hearthstone.logic
{
    public enum BuyState
    {
        Success,
        CreditCard_DataInvalid,
        CreditCard_Locked
    }
    public class ProductAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<Product> GetAllActiveProducts()
        {
            log.Info("ProductAdministration - GetAllActiveProducts()");
            List<Product> allActiveProducts = null;


            return allActiveProducts;
        }

        public static Product GetProduct(int idProduct)
        {
            log.Info("ProductAdministration - GetProduct(idProdcut)");
            Product product = null;

            return product;
        }

        public static BuyState Buy(int idProduct, string username, CreditCardData creditCardData)
        {
            log.Info("ProductAdministration - Buy(idProduct, username, creditCardData)");
            BuyState state = BuyState.Success;

            return state;
        }
    }
}
