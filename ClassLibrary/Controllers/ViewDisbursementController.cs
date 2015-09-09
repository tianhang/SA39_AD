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
    public class ViewDisbursementController
    {
        ErrorLog errorobj;
        RequisitionFacade requisitionFacade;
        DepartmentFacade departmentFacade;
        CompleteDeliveryController completeDeliveryController;

        public ViewDisbursementController()
        {
            errorobj = new ErrorLog();
            requisitionFacade = new RequisitionFacade();
            departmentFacade = new DepartmentFacade();
        }

        public List<Department> loadController()
        {
            return departmentFacade.getDepartments();
        }

        public List<Disbursement> selectDepartment(string departmentId)
        {
            return requisitionFacade.getDisbursementWithStatus("Completed",departmentId);
        }

        public Disbursement selectPendingDisbursement(string departmentId)
        {
            return requisitionFacade.getDisbursementWithDetails("Outstanding", departmentId);
        }

        public List<DisbursementDetails> selectDisbursementDetails(string disbursementId)
        {
            return requisitionFacade.getDisbursementDetails(disbursementId);
        }

        public void selectCompleteDelivery(Disbursement disbursement)
        {
            completeDeliveryController = new CompleteDeliveryController();
            completeDeliveryController.completeDelivery(disbursement);
        }
    }
}
