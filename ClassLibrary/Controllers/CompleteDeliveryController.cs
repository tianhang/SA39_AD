using ClassLibrary.Entities;
using ClassLibrary.EntityFacade;
using ClassLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Controllers
{
    public class CompleteDeliveryController
    {
        RequisitionFacade requisitionFacade;
        CatalogueFacade catalogueFacade;
        ErrorLog errorobj;
        UserFacade userFacade;
        public CompleteDeliveryController()
        {
            errorobj = new ErrorLog();
            requisitionFacade = new RequisitionFacade();
            catalogueFacade = new CatalogueFacade();
        }

        public void completeDelivery(Disbursement disbursement)
        {
            List<DisbursementDetails> disbursementDetailsCollection = disbursement.DisbursementDetailsCollection;

            requisitionFacade.updateDisbursment(disbursementDetailsCollection, disbursement.DisbursementId);


            foreach (DisbursementDetails disbursementDetails in disbursementDetailsCollection)
            {
                catalogueFacade.updateStock(disbursementDetails.ItemId, disbursementDetails.DeliveredQty);

                List<DisbursementHelper> disbursementHelperCollection = requisitionFacade.getRequisitionsForCompleteDisbursement(disbursementDetails.ItemId,disbursement.DepartmentId);
                if (disbursementHelperCollection.Count > 0)
                {
                    foreach (DisbursementHelper d in disbursementHelperCollection)
                    {
                        if (disbursementDetails.DeliveredQty > 0)
                        {
                            int req = d.RequestedQty - d.DeliveredQty;
                            if (disbursementDetails.DeliveredQty<=req)
                            {
                                d.DeliveredQty = d.DeliveredQty+disbursementDetails.DeliveredQty;
                                disbursementDetails.DeliveredQty -= disbursementDetails.DeliveredQty;
                            }
                            else
                            {
                                d.DeliveredQty = d.DeliveredQty+req;
                                disbursementDetails.DeliveredQty -= req;
                            }
                            

                            requisitionFacade.updateRequisitionDelivery(d, disbursementDetails.ItemId);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            requisitionFacade.updateDisbursementRequisitions(disbursement.DepartmentId);


            string subject = "Disbursement Update";

            string bodyStart = "<HTML>"
                          + "<HEAD>"
                          + "</HEAD>"
                          + "<BODY>"
                          + "<BR/>"
                          + "<P>Dear ";

            string body = "";

            userFacade = new UserFacade();

            List<User> userCollection = userFacade.getUsersWithRole("departmentHead",disbursement.DepartmentId);

            body = ",</P><BR/><P>Disbursement update for Disbursement Id : "+disbursement.DisbursementId+" delivered on date : "+disbursement.DeliveryDate+" </P>"
                + "<BR/>"
                + "<P>The details are :- </P>"
                + "<UL>";

            NotifyUserController notifyUserController;

            List<DisbursementDetails> disbursementDetailCol = disbursement.DisbursementDetailsCollection;
            foreach (DisbursementDetails disdetail in disbursementDetailCol)
            {
                body = body + "<LI>" + disdetail.ItemName + "    " + disdetail.RequestedQty + "     "+ disdetail.DeliveredQty+"</LI>";
            }


            body = body + "</UL>"
            + "<BR/>"
            + "<P>From,</P>"
            + "<P>SSIS.</P>"
            + "</BODY>"
            + "</HTML>";

            notifyUserController = new NotifyUserController();

            foreach (User user in userCollection)
            {
                notifyUserController.sendEmail(user.Email, subject, bodyStart + user.UserName + body);
            }


        }
    }
}
