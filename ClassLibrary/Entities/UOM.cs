using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class UOM
    {
        private string uomId;
        private string uom;
        private int number;

        public UOM() { }

        [DataMember]
        public string UomId { get { return uomId; } set { uomId = value; } }

        [DataMember]
        public string Uom { get { return uom; } set { uom = value; } }

        [DataMember]
        public int Number { get { return number; } set { number = value; } }
    }
}
