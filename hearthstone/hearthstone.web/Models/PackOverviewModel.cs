using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hearthstone.web.Models
{
    public class PackOverviewModel
    {
        public int AmountMoney { get; set; }

        public List<PackModel> Packs { get; set; }
    }
}