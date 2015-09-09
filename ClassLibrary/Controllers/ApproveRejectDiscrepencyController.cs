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
    public class ApproveRejectDiscrepancyController
    {
        DiscrepancyFacade df = new DiscrepancyFacade();
        Discrepancy d = new Discrepancy();

        public void updateRejectReason(string id, string reason)
        {
            df.updateRejectReason(id, reason);
        }

        public void updateStatus(List<string> idList, string status)
        {
            DateTime date = System.DateTime.Now;
            df.updateRejAppStatue(idList, status, date);
        }

        public List<discrepancy> updateApproveRejectDiscrepancyStatus(string Exstatus, string Nowstatus)
        {
            List<discrepancy> dList = df.getAllDiscrepancies();

            foreach (discrepancy d in dList)
            {
                if (d.status == Exstatus)
                {
                    d.status = Nowstatus;
                    // d.submitDate = submitDate;
                }
            }
            df.updateDiscrepancyStatue(Exstatus, Nowstatus);
            return dList;
        }

        public List<Discrepancy> getSubmittedGridViewSource()
        {
            List<Discrepancy> list = new List<Discrepancy>();
            foreach (discrepancy d in df.getSubmittedDatas())
            {
                Discrepancy dis = new Discrepancy();
                dis.Amount = (float)d.amount;
                dis.ItemDescription = df.getItemDescription(d.itemId);
                dis.CategoryName = df.getCategoryName(dis.ItemDescription);
                dis.DiscrepancyId = d.discrepancyId;
                dis.SupplierId = d.supplierId;
                dis.ItemId = d.itemId;
                dis.Qunatity = (int)d.quantity;
                dis.Reason = d.reason;
                dis.RejectReason = d.rejectReason;
                dis.Status = d.status;
                dis.SubmitDate = (DateTime)d.submitDate;
                list.Add(dis);
            }
            return list;
        }

        //public List<Discrepancy> getSubmittedGridViewSource(DateTime submitDate)
        //{
        //    List<discrepancy> addedDatas = df.getAddedDatas();
        //    if (addedDatas != null)
        //    {
        //        List<Discrepancy> list = new List<Discrepancy>();
        //        foreach (discrepancy d in df.getSubmittedDatas())
        //        {
        //            Discrepancy dis = new Discrepancy();
        //            dis.Amount = (float)d.amount;
        //            //dis.ApproveDate = (DateTime)d.approveDate;
        //            dis.ItemDescription = df.getItemDescription(d.itemId);
        //            dis.CategoryName = df.getCategoryName_Yuanyuan(dis.ItemDescription);
        //            dis.DiscrepancyId = d.discrepancyId;
        //            dis.SupplierId = d.supplierId;
        //            dis.ItemId = d.itemId;
        //            dis.Qunatity = (int)d.quantity;
        //            dis.Reason = d.reason;
        //            dis.RejectReason = d.rejectReason;
        //            dis.Status = d.status;
        //            //dis.SubmitDate = (DateTime)d.submitDate;
        //            list.Add(dis);
        //        }
        //        return list;
        //    }
        //    else
        //    {
        //        Console.WriteLine("No Added Descripancy!");
        //        return null;
        //    }

        //}

        public List<Discrepancy> getAppAndRejGridViewSource(DateTime AppovedAndRejectedDate)
        {
            List<discrepancy> ajDatas = df.getAllDiscrepancies();
            if (ajDatas != null)
            {
                List<Discrepancy> list = new List<Discrepancy>();
                foreach (discrepancy d in df.getNotAddedDiscrepancies())
                {
                    Discrepancy dis = new Discrepancy();
                    dis.Amount = (float)d.amount;
                    dis.ApproveDate = AppovedAndRejectedDate;
                    dis.ItemDescription = df.getItemDescription(d.itemId);
                    dis.CategoryName = df.getCategoryName(dis.ItemDescription);
                    dis.DiscrepancyId = d.discrepancyId;
                    dis.SupplierId = d.supplierId;
                    dis.ItemId = d.itemId;
                    dis.Qunatity = (int)d.quantity;
                    dis.Reason = d.reason;
                    dis.RejectReason = d.rejectReason;
                    dis.Status = d.status;
                    dis.SubmitDate = (DateTime)d.submitDate;
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

    }


}
