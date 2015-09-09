using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class PurchaseOrder
    {
        private string purchaseOrderId;
        private string supplierId;
        private string supplierName;
        private DateTime orderDate;
        private DateTime deliveryDate;
        private string status;
        private List<PurchaseOrderDetails> purchaseOrderDetailsCollection;

        public PurchaseOrder() { }

        [DataMember]
        public string PurchaseOrderId { get { return purchaseOrderId; } set { purchaseOrderId = value; } }

        [DataMember]
        public string SupplierId { get { return supplierId; } set { supplierId = value; } }

        [DataMember]
        public string SupplierName { get { return supplierName; } set { supplierName = value; } }

        [DataMember]
        public DateTime OrderDate { get { return orderDate; } set { orderDate = value; } }

        [DataMember]
        public DateTime DeliveryDate { get { return deliveryDate; } set { deliveryDate = value; } }

        [DataMember]
        public string Status { get { return status; } set { status = value; } }

        [DataMember]
        public List<PurchaseOrderDetails> PurchaseOrderDetailsCollection { get { return purchaseOrderDetailsCollection; } set { purchaseOrderDetailsCollection = value; } }
    }
}
