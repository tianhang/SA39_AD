using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Entities;
using ClassLibrary.EntityFacade;

namespace ClassLibrary.Controllers
{
    public class ApproveRejectRequisitionController
    {
        RequisitionFacade requisitionFacade = new RequisitionFacade();
        //Constructor
        public ApproveRejectRequisitionController()
        {

        }

        public string getDepartmentId(string userId)
        {
            return requisitionFacade.getDepartmentId_PyaePyae(userId);
        }

        public List<requisition> getRequisition_forApproveReject(string departmentId)
        {
            return requisitionFacade.getRequisition_forApproveReject_PyaePyae(departmentId);
        }

        public void updateRequisitionStatus(string requisitionId, string status, string reason)
        {
            requisitionFacade.updateRequisitionStatus_PyaePyae(requisitionId, status, reason);
        }

        public List<Requisition> loadController(string status, string departmentID)
        {
            List<Requisition> requisitionList = new List<Requisition>();
            //
            //write here to get data from ctx
            //
            return requisitionList;
        }

        public List<RequisitionDetails> selectRequisitionDetail(string requisitionId)
        {
            //RequisitionDetails requisitionDetail = new RequisitionDetails();
            //
            //fill ctx here
            //
            List<requisitionDetail> list = requisitionFacade.getRequisitionDetails_PyaePyae(requisitionId);
            List<RequisitionDetails> newlist = new List<RequisitionDetails>();
            foreach (requisitionDetail r in list)
            {
                RequisitionDetails R = new RequisitionDetails();
                //R.ItemId = r.itemId;
                R.ItemName = r.item.description;
                R.RequestedQty = Convert.ToInt32(r.requestedQty);
                newlist.Add(R);
            }
            return newlist;
        }

        public void selectApproveRequisition(string requisitionID)
        {
            //update status as approve in ctx
        }

        public void selectRejectRequisition(string requisitionID)
        {
            //update status as reject in ctx
        }
    }
}
