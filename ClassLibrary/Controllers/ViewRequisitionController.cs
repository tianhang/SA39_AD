using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.EntityFacade;
using ClassLibrary.Entities;

namespace ClassLibrary.Controllers
{
    public class ViewRequisitionController
    {
        RequisitionFacade requisitionFacade = new RequisitionFacade();
        DepartmentFacade departmentFacade = new DepartmentFacade();
        UserFacade userFacade = new UserFacade();
        public List<string> acceptDepartmentList()
        {
            List<department> deps = departmentFacade.getDepartments_Lingna();
            List<string> names = new List<string>();
            foreach (department d in deps)
            {
                names.Add(d.name);
            }
            return names;
        }

        public List<requisition> selectDepartmentRequisition(string depId, string status)
        {

            return requisitionFacade.getRequisitionsWithStatus_Lingna(status, depId);
        }

        public List<requisition> selectUserRequisition(string userId, string status)
        {
            return requisitionFacade.getUserRequisitionsWithStatus_Lingna(status, userId);
        }

        public user findUser(string userId)
        {
            return userFacade.getUser_Lingna(userId);
        }

        public department findDepartment(string depName)
        {
            return departmentFacade.getDepartment_Lingna(depName);
        }

        public List<RequisitionDetails> selectRequisition(string requisitionId)
        {
            List<requisitionDetail> list = requisitionFacade.getRequisitionDetails_Lingna(requisitionId);
            List<RequisitionDetails> l = new List<RequisitionDetails>();
            foreach (requisitionDetail r in list)
            {
                RequisitionDetails R = new RequisitionDetails();
                R.ItemId = r.itemId;
                R.ItemName = r.item.description;
                R.RequestedQty = (int)r.requestedQty;
                R.DeliveredQty = (int)r.deliveredQty;
                l.Add(R);
            }
            return l;
        }

        public void selectCancelRequistion(string reqId)
        {
            requisitionFacade.updateRequisitionStatus_Lingna("cancelled", reqId);
        }
    }
}
