using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Entities;
using System.Runtime.Serialization;
using ClassLibrary.EntityFacade;
using ClassLibrary.Controllers;

namespace ClassLibrary.GaoFan
{
    public class requisitionGF
    {
        SSISDBEntities ctx = new SSISDBEntities();
        public void insertRequisitionGF(Requisition re)
        {

            requisition newre = new requisition();
            newre.requisitionId = re.RequisitionId;
            newre.date = re.Date;
            newre.userId = re.UserId;
            newre.departmentId = re.DepartmentId;
            newre.rejectreason = re.RejectReason;
            newre.status = re.Status;
            ctx.requisitions.Add(newre);
            ctx.SaveChanges();
            //gaofan

            UserFacade userFacade = new UserFacade();
            List<User> userCollection = userFacade.getUsersWithRole("departmentDeputy", re.DepartmentId);
            if(userCollection.Count==0)
            {
                userCollection = userFacade.getUsersWithRole("departmentHead", re.DepartmentId);

            }


            string subject = "New Requisition Submitted.";

            string bodyStart = "<HTML>"
                          + "<HEAD>"
                          + "</HEAD>"
                          + "<BODY>"
                          + "<BR/>"
                          + "<P>Dear ";

            string body = ",</P><BR/><P>A requisition has been raised from your department.</P>";

            body = body
                + "<BR/>"
                + "<a href=\"http://10.10.1.155/SSISWebApplication/WebPages/ApproveRejectRequisition\">Click this link to view the requisition</a>"//TODO LINK
                + "<BR/>"
                + "<P>From,</P>"
                + "<P>SSIS.</P>"
                + "</BODY>"
                + "</HTML>";

            NotifyUserController notifyUserController = new NotifyUserController();

            foreach (User user in userCollection)
            {
                notifyUserController.sendEmail(user.Email, subject, bodyStart + user.UserName + body);
            }



        }

        public void insertRequisitionDetailsGF(RequisitionDetailsGF red)
        {
            var req = ctx.requisitions.FirstOrDefault(o => o.requisitionId == red.RequisitionId);
            requisitionDetail newred = new requisitionDetail();
            newred.requisitionId = red.RequisitionId;
            newred.itemId = red.ItemId;
            newred.requestedQty = red.RequesetedQty;
            newred.deliveredQty = red.DeliveredQty;
           // newred.status = red.Status;
            req.requisitionDetails.Add(newred);
            ctx.SaveChanges();
            //gaofan
        }
        public List<Requisition> getAllPendingRequisitionByDepartmentIdGF(string departmentId)
        {
            var item = from x in ctx.requisitions
                       where x.departmentId == departmentId && x.status == "Pending"
                       select new
                       {
                           a1 = x.requisitionId,
                           a2 = x.date,
                           a3 = x.userId,
                           a4 = x.status,
                           a5 = x.user.name,
                           a6 = x.departmentId,
                           a7 = x.rejectreason
                       };
            List<Requisition> list = new List<Requisition>();
            foreach (var a in item)
            {
                Requisition re = new Requisition();
                re.RequisitionId = a.a1;
                re.Date = Convert.ToDateTime(a.a2);
                re.UserId = a.a3;
                re.Status = a.a4;
                re.UserName = a.a5;
                re.DepartmentId = a.a6;
                re.RejectReason = a.a7;
                list.Add(re);
            }
            return list;
            //gaofan
        }

        public List<RequisitionDetailsGF> getRequisitionDetailsByRequisitionIdGF(string requisitionId)
        {
            var item = from x in ctx.requisitionDetails
                       where x.requisitionId == requisitionId
                       select new
                       {
                           requsitionId = x.requisitionId,
                           itemId = x.itemId,
                           requestedQty = x.requestedQty,
                           deliveredQty = x.deliveredQty,
                           //status = x.status,
                           itemName = x.item.description
                       };

            List<RequisitionDetailsGF> list = new List<RequisitionDetailsGF>();
            foreach (var a in item)
            {
                RequisitionDetailsGF red = new RequisitionDetailsGF();
                red.RequisitionId = a.requsitionId;
                red.ItemId = a.itemId;
                red.RequesetedQty = Convert.ToInt32(a.requestedQty);
                red.DeliveredQty = Convert.ToInt32(a.deliveredQty);
               // red.Status = a.status;
                red.ItemName = a.itemName;
                list.Add(red);
            }
            return list;
            //gaofan
        }

        public void updateRequisitionStatusGF(Requisition re)
        {
            requisition x = ctx.requisitions.FirstOrDefault(o => o.requisitionId == re.RequisitionId);
            x.status = re.Status;        
            x.rejectreason = re.RejectReason;            
            ctx.SaveChanges();
            // gaofan
        }

        public List<Requisition> getAllRequisitionByUserIdGF(string userId) {
            var item = from x in ctx.requisitions
                       where  x.userId == userId
                       select new
                       {
                           a1 = x.requisitionId,
                           a2 = x.date,
                           a3 = x.userId,
                           a4 = x.status,
                           a5 = x.user.name,
                           a6 = x.departmentId,
                           a7 = x.rejectreason
                       };
            List<Requisition> list = new List<Requisition>();
            foreach (var a in item)
            {
                Requisition re = new Requisition();
                re.RequisitionId = a.a1;
                re.Date = Convert.ToDateTime(a.a2);
                re.UserId = a.a3;
                re.Status = a.a4;
                re.UserName = a.a5;
                re.DepartmentId = a.a6;
                re.RejectReason = a.a7;
                list.Add(re);
            }
            return list;
            //gaofan
        }
    }


    [DataContract]
    public class RequisitionDetailsGF
    {

        private string requisitionId;
        private string itemId;
        private int requestedQty;
        private int deliveredQty;
      //  private string status;
        private string itemName;

        public RequisitionDetailsGF() { }

        [DataMember]
        public string RequisitionId { get { return requisitionId; } set { requisitionId = value; } }
        [DataMember]
        public string ItemId { get { return itemId; } set { itemId = value; } }

        [DataMember]
        public int RequesetedQty { get { return requestedQty; } set { requestedQty = value; } }

        [DataMember]
        public int DeliveredQty { get { return deliveredQty; } set { deliveredQty = value; } }

//        [DataMember]
//        public string Status { get { return status; } set { status = value; } }

        [DataMember]
        public string ItemName { get { return itemName; } set { itemName = value; } }

    }


}
