using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class SupplierPrice
    {
        private string supplierId;
        private string itemId;
        private double price;
        private string itemName;

        public SupplierPrice() { }

        [DataMember]
        public string ItemId { get { return itemId; } set { itemId = value; } }

        [DataMember]
        public double Price { get { return price; } set { price = value; } }

        [DataMember]
        public string ItemName { get { return itemName; } set { itemName = value; } }

        [DataMember]
        public string SupplierId { get { return supplierId; } set { supplierId = value; } }
    }
}
