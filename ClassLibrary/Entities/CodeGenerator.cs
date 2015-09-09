using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ClassLibrary.Entities
{
    [DataContract]
    public class CodeGenerator
    {
        string prefix;
        int lastValue;
        [DataMember]
        public string Prefix
        {
            get { return prefix; }
            set { prefix = value; }
        }
        [DataMember]
        public int LastValue
        {
            get { return lastValue; }
            set { lastValue = value; }
        }
    }
}
