using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hearthstone.data
{
    partial class Deck
    {
        static string[] deckNames = { "attack deck", "defense deck", "murloc deck", "ragnaros killer deck", "angry looser deck", "happy sheep deck" };
        static Random rng = new Random();
        public static string GetRandomizedDeckName
        {
            get
            {
                return deckNames[rng.Next(0, deckNames.Length)];
            }
        }
    }
}
