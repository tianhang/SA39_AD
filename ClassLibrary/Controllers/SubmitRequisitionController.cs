using ClassLibrary.Entities;
using ClassLibrary.EntityFacade;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Controllers
{
    public class SubmitRequisitionController
    {
        RequisitionFacade requisitionFacade = new RequisitionFacade();
        CatalogueFacade catalogueFacade = new CatalogueFacade();

        //public void selectRequisitionSubmit(Requisition requisition)
        //{
        //    //requisitionFacade.createRequisition_PyaePyae(requisition);
        //}

        /*public string getLastRequisitionId()
        {
            return requisitionFacade.getLastRequisitionId();
        }*/

        public string getDepartmentId(string userId)
        {
            return requisitionFacade.getDepartmentId_PyaePyae(userId);
        }

        public int getCodeGeneratorValue(string prefix)
        {
            return requisitionFacade.getCodeGeneratorValue_PyaePyae(prefix);
        }
        public List<category> getAllCategory()
        {
            return catalogueFacade.getAllCategory_PyaePyae();
        }

        public List<item> getItems(string categoryId)
        {
            return catalogueFacade.getItems_PyaePyae(categoryId);
        }

        public string getCategoryName(string itemId)
        {
            return catalogueFacade.getCategoryName_PyaePyae(itemId);
        }

        public UOM getUOMName(string itemId)
        {
            return catalogueFacade.getUOMName_PyaePyae(itemId);
        }

        public string getItemName(string itemId)
        {
            return catalogueFacade.getItemName_PyaePyae(itemId);
        }

        public void createRequisition(string requisitionId, DateTime date, string userId, string departmentId, string rejectReason, string status, DataTable requisitionDetails)
        {
            HelperFacade helperFacade = new HelperFacade();
            codeGenerator codeG = helperFacade.getCode("requisitionId");
            int id = codeG.lastValue + 1;
            codeG.lastValue = id;
            string reqId = codeG.prefix + id;
            requisitionId = reqId;

            foreach(DataRow row in requisitionDetails.Rows)
            {
                row["requisitionId"] = reqId;
                //row["deliveredQty"] = 0;
            }

            requisitionFacade.createRequisition_PyaePyae(requisitionId, date, userId, departmentId, rejectReason, status, requisitionDetails);

            helperFacade.updateCode(codeG);
        }
    }
}
