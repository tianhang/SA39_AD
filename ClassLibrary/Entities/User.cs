 using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class User
    {
        private string userId;
        private string userName;
        private string phoneNo;
        private string email;
        private string address;
        private string departmentId;
        private string roleId;
        private string password;
        private DateTime startDate;
        private DateTime endDate;
        private string departmentName;
        private string roleName;
        private string status;

        public User()
        {

        }

        [DataMember]
        public string UserID { get { return userId; } set { userId = value; } }

        [DataMember]
        public string UserName { get { return userName; } set { userName = value; } }

        [DataMember]
        public string PhoneNo { get { return phoneNo; } set { phoneNo = value; } }

        [DataMember]
        public string Email { get { return email; } set { email = value; } }

        [DataMember]
        public string Address { get { return address; } set { address = value; } }

        [DataMember]
        public string DepartmentId { get { return departmentId; } set { departmentId = value; } }

        [DataMember]
        public string DepartmentName { get { return departmentName; } set { departmentName = value; } }

        [DataMember]
        public string RoleId { get { return roleId; } set { roleId = value; } }

        [DataMember]
        public string RoleName { get { return roleName; } set { roleName = value; } }

        [DataMember]
        public string Password { get { return password; } set { password = value; } }

        [DataMember]
        public DateTime StartDate { get { return startDate; } set { startDate = value; } }

        [DataMember]
        public DateTime EndDate { get { return endDate; } set { endDate = value; } }

        [DataMember]
        public string Status { get { return status; } set { status = value; } }
    }
}
