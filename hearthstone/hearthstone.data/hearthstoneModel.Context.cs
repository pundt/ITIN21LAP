﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class clonestoneEntities : DbContext
    {
        public clonestoneEntities()
            : base("name=clonestoneEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Card> AllCards { get; set; }
        public virtual DbSet<CardPack> AllCardPacks { get; set; }
        public virtual DbSet<CardType> AllCardTypes { get; set; }
        public virtual DbSet<Deck> AllDecks { get; set; }
        public virtual DbSet<DeckCard> AllDeckCards { get; set; }
        public virtual DbSet<User> AllUsers { get; set; }
        public virtual DbSet<UserCard> AllUserCards { get; set; }
        public virtual DbSet<UserRole> AllUserRoles { get; set; }
        public virtual DbSet<VirtualPurchase> AllVirtualPurchases { get; set; }
        public virtual DbSet<Product> AllProducts { get; set; }
        public virtual DbSet<ProductPurchase> AllProductPurchases { get; set; }
    }
}
