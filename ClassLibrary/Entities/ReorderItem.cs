using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class ReorderItem
    {
        private string reorderItemId;
        private string itemId;
        private string supplierId;
        private string userId;
        private int qtyToOrder;
        private double amount;
        private string status;
        private string rejectReason;
        private string description;
        private string supplierName;
        private string userName;
        private string itemName;               // modify in 3/11
        private double price;

        public ReorderItem() { }

        [DataMember]
        public string ReorderItemId { get { return reorderItemId; } set { reorderItemId = value; } }

        [DataMember]
        public string ItemId { get { return itemId; } set { itemId = value; } }

        [DataMember]
        public string SupplierId { get { return supplierId; } set { supplierId = value; } }

        [DataMember]
        public string UserId { get { return userId; } set { userId = value; } }

        [DataMember]
        public int QtyToOrder { get { return qtyToOrder; } set { qtyToOrder = value; } }

        [DataMember]
        public double Amount { get { return amount; } set { amount = value; } }

        [DataMember]
        public string Status { get { return status; } set { status = value; } }

        [DataMember]
        public string RejectReason { get { return rejectReason; } set { rejectReason = value; } }

        [DataMember]
        public string UserName { get { return userName; } set { userName = value; } }
        [DataMember]
        public string SupplierName { get { return supplierName; } set { supplierName = value; } }

        [DataMember]
        public string Description { get { return description; } set { description = value; } }

        [DataMember]
        public string ItemName { get { return itemName; } set { itemName = value; } }

        [DataMember]
        public double Price { get { return price; } set { price = value; } }

    }
}
