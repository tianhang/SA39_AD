using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Entities;

namespace ClassLibrary.Controllers
{
    public class ApproveRejectPurchaseOrderController
    {

        private NotifyUserController notifyUserController;

        //Constructor
        public ApproveRejectPurchaseOrderController()
        {

        }

        //get reorder items data by status
        public List<ReorderItem> loadController()
        {
            List<ReorderItem> reorderItemsList = new List<ReorderItem>();
            //
            //Write here to get data from entity
            //
            return reorderItemsList;
        }

        public void selectApprove()
        {
            //update status as approve in entity


            notifyUserController = new NotifyUserController();
            notifyUserController.sendEmail("", "", "");
        }

        public void selectReject()
        {
            //update status as reject in entity

            notifyUserController = new NotifyUserController();
            notifyUserController.sendEmail("", "", "");
        }

    }
}
