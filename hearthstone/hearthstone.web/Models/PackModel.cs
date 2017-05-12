using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hearthstone.web.Models
{
    public class PackModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string DescriptionShort
        {
            get
            {
                return Description.Length > 100 ? Description.Substring(0, 100) : Description;
            }
        }
        public int Price { get; set; }
    }
}