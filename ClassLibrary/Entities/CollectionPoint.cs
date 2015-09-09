using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class CollectionPoint
    {
        private string collectionPointId;
        //private string collectionPointName;
        private string address;
        private string time;

        public CollectionPoint()
        {

        }

        [DataMember]
        public string CollectionPointId 
        {
            get { return collectionPointId; }
            set { collectionPointId = value; }
        }

        //[DataMember]
        //public string CollectionPointName 
        //{
        //    get { return collectionPointName; }
        //    set { collectionPointName = value; }
        //}

        [DataMember]
        public string Address 
        {
            get { return address; }
            set { address = value; }
        }

        [DataMember]
        public string Time 
        {
            get { return time; }
            set { time = value; }
        }
        
    }
}
