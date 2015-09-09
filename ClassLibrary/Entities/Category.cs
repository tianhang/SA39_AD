using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class Category
    {
        private string categoryId;
        private string categoryName;

        public Category()
        {

        }

        [DataMember]
        public string CategoryId 
        { 
            get
            {
                return categoryId;
            }
            set
            {
                categoryId = value;
            } 
        }

        [DataMember]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
    }
}
