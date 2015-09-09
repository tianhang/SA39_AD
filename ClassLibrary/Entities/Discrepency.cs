using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    public class Discrepancy
    {
        string discrepancyId;

        public string DiscrepancyId
        {
            get { return discrepancyId; }
            set { discrepancyId = value; }
        }
        string itemId;

        public string ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }
        string userId;

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        int qunatity;

        public int Qunatity
        {
            get { return qunatity; }
            set { qunatity = value; }
        }
        string supplierId;

        public string SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }
        float amount;

        public float Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        string reason;

        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }
        DateTime submitDate;

        public DateTime SubmitDate
        {
            get { return submitDate; }
            set { submitDate = value; }
        }
        DateTime approveDate;

        public DateTime ApproveDate
        {
            get { return approveDate; }
            set { approveDate = value; }
        }
        string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        string rejectReason;

        public string RejectReason
        {
            get { return rejectReason; }
            set { rejectReason = value; }
        }
        string itemDescription;

        public string ItemDescription
        {
            get { return itemDescription; }
            set { itemDescription = value; }
        }
        string categoryName;

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
    }
}
