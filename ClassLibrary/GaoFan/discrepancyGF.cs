using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Entities;
using System.Net.Mail;
using System.Net;
using ClassLibrary.EntityFacade;
using ClassLibrary.Controllers;


namespace ClassLibrary.GaoFan
{
    public class discrepancyGF
    {
        SSISDBEntities ctx = new SSISDBEntities();
        public List<Discrepancy> getAllPendingDiscrepancyGF() {
            var item = from o in ctx.discrepancies
                       where o.status == "Pending"
                       select new
                       {
                            discrepancyId = o.discrepancyId,
                            itemId = o.itemId,  
                            userId   =o.userId,
                            qunatity = o.quantity,
                            supplierId = o.supplierId,
                            amount = o.amount,
                            reason  = o.reason,
                            submitDate = o.submitDate,
                            approveDate = o.approveDate,
                            status = o.status,
                            rejectReason = o.rejectReason,
                            itemDescription= o.item.description,
                            categoryName = o.item.category.name
                       };

            List<Discrepancy> list = new List<Discrepancy>();
            foreach (var a in item) {
                Discrepancy dis = new Discrepancy();
                dis.DiscrepancyId = a.discrepancyId;
                dis.ItemId = a.itemId;
                dis.UserId = a.userId;
                dis.Qunatity =Convert.ToInt32(a.qunatity);
                dis.SupplierId = a.supplierId;              
                dis.Amount = (float)a.amount;
                dis.Reason = a.reason;
                dis.SubmitDate =Convert.ToDateTime(a.submitDate);
                if (a.approveDate != null)
                {
                    dis.ApproveDate = Convert.ToDateTime(a.approveDate);
                }
                else {
                    dis.ApproveDate = DateTime.Now;
                }
                dis.Status = a.status;
                dis.RejectReason = a.rejectReason;
                dis.ItemDescription = a.itemDescription;
                dis.CategoryName = a.categoryName;
                list.Add(dis);
            }

           // MailMessage message = new MailMessage();
           // message.From = new MailAddress("wnn19890513@gmail.com");

           // message.To.Add(new MailAddress("gaofan19901103@gmail.com"));
           //// message.To.Add(new MailAddress("recipient2@foo.bar.com"));
           //// message.To.Add(new MailAddress("recipient3@foo.bar.com"));

           //// message.CC.Add(new MailAddress("carboncopy@foo.bar.com"));
           // message.Subject = "This is my subject";
           // message.Body = "This is the content";

           // SmtpClient client = new SmtpClient();
           // client.Send(message);



            //MailMessage mail = new MailMessage();
            //mail.From = new System.Net.Mail.MailAddress("wnn19890513@gmail.com");

            //// The important part -- configuring the SMTP client
            //SmtpClient smtp = new SmtpClient();
            //smtp.Port = 587;   // [1] You can try with 465 also, I always used 587 and got success
            //smtp.EnableSsl = true;
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // [2] Added this
            //smtp.UseDefaultCredentials = false; // [3] Changed this
            //smtp.Credentials = new NetworkCredential("wnn19890513@gmail.com", "wnzhyyqmm");  // [4] Added this. Note, first parameter is NOT string.
            //smtp.Host = "smtp.gmail.com";

            ////recipient address
            //mail.To.Add(new MailAddress("gaofan1990113@gmail.com"));

            ////Formatted mail body
            //mail.IsBodyHtml = true;
            //string st = "Test";

            //mail.Body = st;
            //smtp.Send(mail);

           // SmtpClient client = new SmtpClient();
           // client.Port = 587;
           // client.Host = "smtp.gmail.com";
           // client.Timeout = 10000;
           // client.DeliveryMethod = SmtpDeliveryMethod.Network;
           // client.UseDefaultCredentials = false;
           // client.EnableSsl = true;
           // client.Credentials = new System.Net.NetworkCredential("gaofan19901103@gmail.com", "wnzhyyqmm");
           // MailMessage message = new MailMessage();
           //// SmtpClient client = this.GetDefaultSmtpClient();
           // message.From = new MailAddress("gaofan19901103@gmail.com");
           // message.To.Add(new MailAddress("wnn19890513@gmail.com"));
           // message.Subject = "nimei";
           // message.Body = "test";

           // client.Send(message);

            return list;
        }


        public void insertDiscrepancyGF(Discrepancy dis) {
            discrepancy d = new discrepancy();
            d.discrepancyId = dis.DiscrepancyId;
            d.itemId = dis.ItemId;
            d.userId = dis.UserId;
            d.quantity = dis.Qunatity;
            d.supplierId = dis.SupplierId;
            d.amount = dis.Amount;
            d.reason = dis.Reason;
            d.submitDate = dis.SubmitDate;
            d.approveDate = dis.ApproveDate;
            d.status = dis.Status;
            d.rejectReason = dis.RejectReason;
            ctx.discrepancies.Add(d);
            ctx.SaveChanges();

            UserFacade userFacade = new UserFacade();
            List<User> userCollection;
            if(dis.Amount>250)
            {
               userCollection= userFacade.getUsersWithRole("storeManager");
            }
            else
            {
               userCollection= userFacade.getUsersWithRole("storeSupervisor");
            }


            string subject = "New Discrepency Submitted.";

            string bodyStart = "<HTML>"
                          + "<HEAD>"
                          + "</HEAD>"
                          + "<BODY>"
                          + "<BR/>"
                          + "<P>Dear ";

            string body = ",</P><BR/><P>A discrepency has been raised from your department.</P>";

            body = body
                + "<BR/>"
                + "<a href=\"http://10.10.1.155/SSISWebApplication/WebPages/Discrepancy/ApproveDiscrepancyRequest\">Click this link to view the discrepency</a>"//TODO LINK
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

        public void updateDiscrepancyStatusGF(Discrepancy dis) {
            discrepancy d = ctx.discrepancies.FirstOrDefault( o => o.discrepancyId == dis.DiscrepancyId);
            d.status = dis.Status;
            d.rejectReason = dis.RejectReason;
            d.approveDate = dis.ApproveDate;
            ctx.SaveChanges();

            if (dis.Status == "Rejected")
            {
                UserFacade userFacade = new UserFacade();
                user us = userFacade.getUser_Lingna(dis.UserId);


                string subject = "Discrepency Rejected";

                string bodyStart = "<HTML>"
                              + "<HEAD>"
                              + "</HEAD>"
                              + "<BODY>"
                              + "<BR/>"
                              + "<P>Dear ";

                string body = ",</P><BR/><P>The discrepency you submitted with ID : " + dis.DiscrepancyId + ", has been rejected.</P>"
                    + "<BR/><P>Reject Reason : " + dis.RejectReason + "</P>";

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

        public List<Discrepancy> getAllPendingDiscrepancyOver250GF() {
            var item = from o in ctx.discrepancies
                       where o.status == "Pending" && o.amount > (float)250
                       select new
                       {
                           discrepancyId = o.discrepancyId,
                           itemId = o.itemId,
                           userId = o.userId,
                           qunatity = o.quantity,
                           supplierId = o.supplierId,
                           amount = o.amount,
                           reason = o.reason,
                           submitDate = o.submitDate,
                           approveDate = o.approveDate,
                           status = o.status,
                           rejectReason = o.rejectReason,
                           itemDescription = o.item.description,
                           categoryName = o.item.category.name
                       };

            List<Discrepancy> list = new List<Discrepancy>();
            foreach (var a in item)
            {
                Discrepancy dis = new Discrepancy();
                dis.DiscrepancyId = a.discrepancyId;
                dis.ItemId = a.itemId;
                dis.UserId = a.userId;
                dis.Qunatity = Convert.ToInt32(a.qunatity);
                dis.SupplierId = a.supplierId;
                dis.Amount = (float)a.amount;
                dis.Reason = a.reason;
                dis.SubmitDate = Convert.ToDateTime(a.submitDate);
                if (a.approveDate != null)
                {
                    dis.ApproveDate = Convert.ToDateTime(a.approveDate);
                }
                else
                {
                    dis.ApproveDate = DateTime.Now;
                }
                dis.Status = a.status;
                dis.RejectReason = a.rejectReason;
                dis.ItemDescription = a.itemDescription;
                dis.CategoryName = a.categoryName;
                list.Add(dis);
            }
            return list;
        }

        public List<Discrepancy> getAllPendingDiscrepancyUnder250GF()
        {
            var item = from o in ctx.discrepancies
                       where o.status == "Pending" && o.amount < (float)250.0
                       select new
                       {
                           discrepancyId = o.discrepancyId,
                           itemId = o.itemId,
                           userId = o.userId,
                           qunatity = o.quantity,
                           supplierId = o.supplierId,
                           amount = o.amount,
                           reason = o.reason,
                           submitDate = o.submitDate,
                           approveDate = o.approveDate,
                           status = o.status,
                           rejectReason = o.rejectReason,
                           itemDescription = o.item.description,
                           categoryName = o.item.category.name
                       };

            List<Discrepancy> list = new List<Discrepancy>();
            foreach (var a in item)
            {
                Discrepancy dis = new Discrepancy();
                dis.DiscrepancyId = a.discrepancyId;
                dis.ItemId = a.itemId;
                dis.UserId = a.userId;
                dis.Qunatity = Convert.ToInt32(a.qunatity);
                dis.SupplierId = a.supplierId;
                dis.Amount = (float)a.amount;
                dis.Reason = a.reason;
                dis.SubmitDate = Convert.ToDateTime(a.submitDate);
                if (a.approveDate != null)
                {
                    dis.ApproveDate = Convert.ToDateTime(a.approveDate);
                }
                else
                {
                    dis.ApproveDate = DateTime.Now;
                }
                dis.Status = a.status;
                dis.RejectReason = a.rejectReason;
                dis.ItemDescription = a.itemDescription;
                dis.CategoryName = a.categoryName;
                list.Add(dis);
            }
            return list;
        }
    }
}
