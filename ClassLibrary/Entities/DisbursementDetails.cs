using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class DisbursementDetails
    {
        private string itemId;
        private string itemName;
        private int requestedQty;
        private int deliveredQty;

        public DisbursementDetails()
        {

        }

        [DataMember]
        public string ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        [DataMember]
        public string ItemName 
        {
            get { return itemName; }
            set { itemName = value; }
        }

        [DataMember]
        public int RequestedQty
        {
            get { return requestedQty; }
            set { requestedQty = value; }
        }

        [DataMember]
        public int DeliveredQty
        {
            get { return deliveredQty; }
            set { deliveredQty = value; }
        }

    }
}
