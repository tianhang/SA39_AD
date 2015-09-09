using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class Requisition
    {
        private string requisitionId;
        private DateTime date;
        private string userId;
        private string status;
        private string userName;
        private string departmentId;
        private string rejectReason;
        private List<RequisitionDetails> requisitionDetailsCollection;

        public Requisition() { }

        [DataMember]
        public string RequisitionId { get { return requisitionId; } set { requisitionId = value; } }

        [DataMember]
        public DateTime Date { get { return date; } set { date = value; } }

        [DataMember]
        public string UserId { get { return userId; } set { userId = value; } }

        [DataMember]
        public string Status { get { return status; } set { status = value; } }

        [DataMember]
        public string UserName { get { return userName; } set { userName = value; } }

        [DataMember]
        public string DepartmentId { get { return departmentId; } set { departmentId = value; } }

        [DataMember]
        public string RejectReason { get { return rejectReason; } set { rejectReason = value; } }

        [DataMember]
        public List<RequisitionDetails> RequisitionDetailsCollection { get { return requisitionDetailsCollection; } set { requisitionDetailsCollection = value; } }
    }
}
