using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class PurchaseOrderDetails
    {
        private string itemId;
        private string itemName;
        private int qty;
        private double price;
        private double amount;

        public PurchaseOrderDetails()
        {

        }

        [DataMember]
        public string ItemId { get { return itemId; } set { itemId = value; } }

        [DataMember]
        public string ItemName { get { return itemName; } set { itemName = value; } }


        [DataMember]
        public int Qty { get { return qty; } set { qty = value; } }

        [DataMember]
        public double Price { get { return price; } set { price = value; } }

        [DataMember]
        public double Amount { get { return amount; } set { amount = value; } }

    }
}
