using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class Disbursement
    {
        private string disbursementId;
        private string departmentId;
        private string departmentName;
        private DateTime date;
        private DateTime deliveryDate;
        private string status;
        private string collectionPointName;
        private string representativeName;
        private List<DisbursementDetails> disbursementDetailCollection;

        public Disbursement()
        {

        }

        [DataMember]
        public string DisbursementId 
        {
            get { return disbursementId; }
            set { disbursementId = value; }
        }

        [DataMember]
        public string DepartmentId
        {
            get { return departmentId; }
            set { departmentId = value; }
        }

        [DataMember]
        public string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; }
        }

        [DataMember]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        [DataMember]
        public DateTime DeliveryDate
        {
            get { return deliveryDate; }
            set { deliveryDate = value; }
        }

        [DataMember]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        [DataMember]
        public List<DisbursementDetails> DisbursementDetailsCollection
        {
            get { return disbursementDetailCollection; }
            set { disbursementDetailCollection = value; }
        }

        [DataMember]
        public string CollectionPointName
        {
            get { return collectionPointName; }
            set { collectionPointName = value; }
        }

        [DataMember]
        public string RepresentativeName
        {
            get { return representativeName; }
            set { representativeName = value; }
        }

    }
}
