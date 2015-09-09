using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helper
{
    public class newPrice
    {
        private string description;
        private string itemId;
        private double price;

        public newPrice()
        {
        }
        public string Description { get { return description; } set { description = value; } }

        public string ItemId { get { return itemId; } set { itemId = value; } }

        public double Price { get { return price; } set { price = value; } }

    }
}
