using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Helper;
using ClassLibrary.EntityFacade;
using ClassLibrary.Entities;

namespace ClassLibrary.Controllers
{
    public class SubmitDiscrepancyController
    {
        DiscrepancyFacade df = new DiscrepancyFacade();

        public string getCategory(string itemName)
        {
            return df.getCategoryName(itemName);
        }

        public List<string> getSupplier()
        {
            return df.getSupplierID();
        }

        public List<string> getDescriptions()
        {
            return df.getAllDescriptions();
        }

        public float getPrice(string itemName, string supplierID)
        {
            string itemID = df.getItemID(itemName);
            return df.getPrice(supplierID, itemID);
        }

        public List<discrepancy> getAddedData()
        {
            return df.getAddedDatas();
        }

        public List<Discrepancy> getGridViewSource()
        {
            List<discrepancy> addedDatas = df.getAddedDatas();
            if (addedDatas != null)
            {
                List<Discrepancy> list = new List<Discrepancy>();
                foreach (discrepancy d in df.getAddedDatas())
                {
                    Discrepancy dis = new Discrepancy();
                    dis.Amount = (float)d.amount;
                    //dis.ApproveDate = (DateTime)d.approveDate;
                    dis.ItemDescription = df.getItemDescription(d.itemId);
                    dis.CategoryName = df.getCategoryName(dis.ItemDescription);
                    dis.DiscrepancyId = d.discrepancyId;
                    dis.SupplierId = d.supplierId;
                    dis.ItemId = d.itemId;
                    dis.Qunatity = (int)d.quantity;
                    dis.Reason = d.reason;
                    dis.RejectReason = d.rejectReason;
                    dis.Status = d.status;
                    //dis.SubmitDate = (DateTime)d.submitDate;
                    list.Add(dis);
                }
                return list;
            }
            else
            {
                Console.WriteLine("No Added Descripancy!");
                return null;
            }

        }



        public List<discrepancy> getSubmittedData()
        {
            return df.getSubmittedDatas();
        }


        public void addDiscrepancyItem(string itemName, string category, int qty, string supplier
            , float price, string reason)
        {
            discrepancy d = new discrepancy();
            d.amount = qty * price;
            d.discrepancyId = df.generateID();
            d.itemId = df.getItemID(itemName);
            d.quantity = qty;
            d.reason = reason;
            d.status = "Added";
            d.supplierId = supplier;
            d.userId = "u1004";
            df.addDiscrepancyItem(d);
        }

        //public List<discrepancy> updateStatus(DateTime submitDate)
        //{
        //    List<discrepancy> dList = df.getAllDiscrepancies();

        //    foreach (discrepancy d in dList)
        //    {
        //        if (d.status == "Added")
        //        {
        //            d.status = "Submitted";
        //            d.submitDate = submitDate;
        //        }
        //    }            
        //    df.updateDiscrepancyStatue("Added", "Submitted");
        //    return dList;
        //}

        public void updateStatus(List<string> idList)
        {
            DateTime date = System.DateTime.Now;
            df.updateStatue(idList, "Pending", date);
        }

        public void deleteDiscrepancy(string id)
        {
            df.deleteDiscrepancy(id);
        }

        public void updateDiscrepancy(int qty, string reason, string id)
        {
            df.updateDiscrepancy(qty, reason, id);
        }
    }
}
