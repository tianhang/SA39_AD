using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helper
{
    public class newReorderItem
    {
        private string description;
        private string supplierName;
        private int qty;
        private int amount;

        public newReorderItem()
        {
        }
        public string Description { get { return description; } set { description = value; } }

        public string SupplierName { get { return supplierName; } set { supplierName = value; } }

        public int Qty { get { return qty; } set { qty = value; } }

        public int Amount { get { return amount; } set { amount = value; } }

    }
}
