using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class RequisitionDetails
    {
        private string itemId;
        private int requestedQty;
        private int deliveredQty;
        private string status;
        private string itemName;

        public RequisitionDetails() { }

        [DataMember]
        public string ItemId { get { return itemId; } set { itemId = value; } }

        [DataMember]
        public int RequestedQty { get { return requestedQty; } set { requestedQty = value; } }

        [DataMember]
        public int DeliveredQty { get { return deliveredQty; } set { deliveredQty = value; } }

        [DataMember]
        public string Status { get { return status; } set { status = value; } }

        [DataMember]
        public string ItemName { get { return itemName; } set { itemName = value; } }

    }
}
