//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace hearthstone.data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductPurchase
    {
        public int ID { get; set; }
        public int NumberOfDiamonds { get; set; }
        public decimal Price { get; set; }
        public int ID_User { get; set; }
        public int ID_Product { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
