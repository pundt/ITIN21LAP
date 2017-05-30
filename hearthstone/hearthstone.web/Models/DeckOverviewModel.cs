using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hearthstone.web.Models
{
    public class DeckOverviewModel
    {
        public int ID_Deck { get; set; }

        public string Name { get; set; }

        public int NumberOfCardsInDeck { get; set; }

        public string ImageUrl { get; set; }
    }
}