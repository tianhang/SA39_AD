using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class Supplier
    {
        private string supplierId;
        private string supplierName;
        private string contactName;
        private long phoneNo;
        private long faxNo;
        private string address;
        private List<SupplierPrice> supplierPriceCollection;
        private string status;

        public Supplier() { }

        [DataMember]
        public string SupplierId { get { return supplierId; } set { supplierId = value; } }

        [DataMember]
        public string SupplierName { get { return supplierName; } set { supplierName = value; } }

        [DataMember]
        public string ContactName { get { return contactName; } set { contactName = value; } }

        [DataMember]
        public long PhoneNo { get { return phoneNo; } set { phoneNo = value; } }

        [DataMember]
        public long FaxNo { get { return faxNo; } set { faxNo = value; } }

        [DataMember]
        public string Address { get { return address; } set { address = value; } }

        [DataMember]
        public string Status { get { return status; } set { status = value; } }

        [DataMember]
        public List<SupplierPrice> SupplierPriceCollection { get { return supplierPriceCollection; } set { supplierPriceCollection = value; } }
    }
}
