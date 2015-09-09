using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class Menu
    {
        private string menuId;
        private string menuName;
        private string url;
        private string parentId;

        public Menu()
        {

        }

        [DataMember]
        public string MenuID { get { return menuId; } set { menuId = value; } }

        [DataMember]
        public string MenuName { get { return menuName; } set { menuName = value; } }

        [DataMember]
        public string Url { get { return url; } set { url = value; } }


        [DataMember]
        public string ParentId { get { return parentId; } set { parentId = value; } }
    }
}
