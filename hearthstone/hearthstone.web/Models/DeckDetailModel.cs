using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hearthstone.web.Models
{
    public class DeckDetailModel
    {
        public DeckOverviewModel DeckOverview { get; set; }

        public List<CardModel> CardsForDeck { get; set; }

        public List<CardModel> CardsInDeck { get; set; }
    }
}