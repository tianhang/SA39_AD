using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class UserRole
    {
        private string roleId;
        private string roleName;

        public UserRole()
        {

        }

        [DataMember]
        public string RoleId { get { return roleId; } set { roleId = value; } }

        [DataMember]
        public string RoleName { get { return roleName; } set { roleName = value; } }

    }
}
