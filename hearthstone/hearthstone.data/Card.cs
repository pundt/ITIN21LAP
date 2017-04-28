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
    
    public partial class Card
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Card()
        {
            this.AllDeckCards = new HashSet<DeckCard>();
            this.AllUserCardCollections = new HashSet<UserCardCollection>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public int ManaCost { get; set; }
        public int Attack { get; set; }
        public int Life { get; set; }
        public Nullable<int> ID_CardType { get; set; }
        public byte[] Image { get; set; }
    
        public virtual tblCardType CardType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeckCard> AllDeckCards { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCardCollection> AllUserCardCollections { get; set; }
    }
}
