using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class Department
    {
        private string departmentId;
        private string departmentName;
        private string contactName;
        private long phoneNo;
        private long faxNo;
        private string collectionPointId;
        private string collectionPointName;
        private string representativeName;

        public Department()
        {

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
        public string ContactNo
        {
            get { return contactName; }
            set { contactName = value; }
        }

        public long PhoneNo
        {
            get { return phoneNo; }
            set { phoneNo = value; }
        }

        public long FaxNo
        {
            get { return faxNo  ; }
            set { faxNo = value; }
        }

        [DataMember]
        public string CollectionPointId
        {
            get { return collectionPointId; }
            set { collectionPointId = value; }
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
