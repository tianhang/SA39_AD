using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Entities;
using ClassLibrary.EntityFacade;
using ClassLibrary.Controllers;

namespace ClassLibrary.GaoFan
{
    public class reorderGF
    {
        SSISDBEntities ctx = new SSISDBEntities();
        public List<ReorderItem> getReorderItemsByStatusGF()  
        {
            var items = from o in ctx.reorderItems
                        where o.status == "Pending"
                        select new
                        {
                            reorderItemId = o.reorderId,
                            itemId = o.itemId,
                            itemName = o.item.description,
                            supplierId = o.supplierId,
                            supplierName = o.supplier.name,
                            userId = o.userId,
                            userName = o.user.name,
                            QtyToOrder = o.qty,
                            amount = o.amount,
                            status = o.status,
                            rejectReason = o.rejectReason
                        };
            List<ReorderItem> reorderItemCollection = new List<ReorderItem>();
            foreach (var i in items)
            {
                ReorderItem r = new ReorderItem();
                r.ReorderItemId = i.reorderItemId;
                r.ItemId = i.itemId;
                r.Description = i.itemName;
                r.SupplierId = i.supplierId;
                r.SupplierName = i.supplierName;
                r.UserId = i.userId;
                r.UserName = i.userName;
                r.QtyToOrder = Convert.ToInt32(i.QtyToOrder);
                r.Amount = Convert.ToDouble(i.amount);
                r.Status = i.status;
                r.RejectReason = i.rejectReason;
                reorderItemCollection.Add(r);
            }

            return reorderItemCollection;
            //gaofan
        }

        public void updateReorderItemGF(ReorderItem ri) {
            reorderItem r = ctx.reorderItems.FirstOrDefault(o => o.reorderId == ri.ReorderItemId);
            r.status = ri.Status;
            r.rejectReason = ri.RejectReason;
            ctx.SaveChanges();

            if (ri.Status == "Rejected")
            {
                UserFacade userFacade = new UserFacade();
                user us = userFacade.getUser_Lingna(ri.UserId);


                string subject = "Purchase reorder Rejected";

                string bodyStart = "<HTML>"
                              + "<HEAD>"
                              + "</HEAD>"
                              + "<BODY>"
                              + "<BR/>"
                              + "<P>Dear ";

                string body = ",</P><BR/><P>The purchase reorder you submitted with ID : " + ri.ReorderItemId + ", has been rejected.</P>"
                    + "<BR/><P>Reject Reason : " + ri.RejectReason + "</P>";

                body = body
                    + "<BR/>"
                    + "<P>From,</P>"
                    + "<P>SSIS.</P>"
                    + "</BODY>"
                    + "</HTML>";

                NotifyUserController notifyUserController = new NotifyUserController();
                notifyUserController.sendEmail(us.email, subject, bodyStart + us.name + body);
            }


        }
    }
}
