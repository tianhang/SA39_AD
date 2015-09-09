using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helper
{
    public class newItem
    {
        private string description;
        private string uom;
        private int stockBalance;
        private int reorderLevel;
        private int reorderQty;

        public newItem()
        {
        }
        public string Description { get { return description; } set { description = value; } }

        public string Uom { get { return uom; } set { uom = value; } }

        public int StockBalance { get { return stockBalance; } set { stockBalance = value; } }

        public int ReorderLevel { get { return reorderLevel; } set { reorderLevel = value; } }

        public int ReorderQty { get { return reorderQty; } set { reorderQty = value; } }

    }
}
