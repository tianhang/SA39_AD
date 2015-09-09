using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class Item
    {
        private string itemId;
        private string description;
        private string categoryId;
        private string categoryName;
        private string uomId;
        private string uomName;
        private int uomNumber;
        private string binNumber;
        private int stockBalance;
        private int reorderLevel;
        private int reorderQty;
        private string status;

        public Item()
        {

        }

        [DataMember]
        public string ItemId { get { return itemId; } set { itemId = value; } }

        [DataMember]
        public string Description { get { return description; } set { description = value; } }

        [DataMember]
        public string CategoryId { get { return categoryId; } set { categoryId = value; } }

        [DataMember]
        public string CategoryName { get { return categoryName; } set { categoryName = value; } }

        [DataMember]
        public string UomId { get { return uomId; } set { uomId = value; } }

        [DataMember]
        public string UomName { get { return uomName; } set { uomName = value; } }

        [DataMember]
        public int UomNumber { get { return uomNumber; } set { uomNumber = value; } }

        [DataMember]
        public int StockBalance { get { return stockBalance; } set { stockBalance = value; } }

        [DataMember]
        public int ReorderLevel { get { return reorderLevel; } set { reorderLevel = value; } }

        [DataMember]
        public int ReorderQty { get { return reorderQty; } set { reorderQty = value; } }

        [DataMember]
        public string Status { get { return status; } set { status = value; } }

        [DataMember]
        public string BinNumber { get { return binNumber; } set { binNumber = value; } }
    }
}
