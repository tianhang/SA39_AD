using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helper
{
    [DataContract]
    public class RetrievalHelper
    {
        private string itemId;
        private string description;
        private string binNumber;
        private int stockBalance;
        private int requiredQty;
        private int retrievalQty;

        public RetrievalHelper()
        {
            
        }

        [DataMember]
        public string ItemId { get { return itemId; } set { itemId = value; } }

        [DataMember]
        public string Description { get { return description; } set { description = value; } }

        [DataMember]
        public string BinNumber { get { return binNumber; } set { binNumber = value; } }

        [DataMember]
        public int StockBalance { get { return stockBalance; } set { stockBalance = value; } }

        [DataMember]
        public int RequiredQty { get { return requiredQty; } set { requiredQty = value; } }

        [DataMember]
        public int RetrievalQty { get { return retrievalQty; } set { retrievalQty = value; } }
    }
}
