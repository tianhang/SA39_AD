using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.EntityFacade;
using ClassLibrary.Entities;


namespace ClassLibrary.Controllers
{
    public class EmergencyDisbursementController
    {
        DepartmentFacade departmentFacade = new DepartmentFacade();
        CatalogueFacade cataFacade = new CatalogueFacade();
        SupplierFacade sFacade = new SupplierFacade();
        DisbursementFacade dFacade = new DisbursementFacade();
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

        public string ConvertName(string itemName)
        {
            if (itemName.Contains("&quot;"))
            {
                itemName = itemName.Replace("&quot;", @"""");
            }
            return itemName;
        }

        public item getItemByName(string itemName)
        {
            string name = ConvertName(itemName);
            System.Diagnostics.Debug.Print("itemName " + name);
            return cataFacade.getItem_Lingna(name);
        }

        public item getItem(string itemId)
        {
            item i = cataFacade.getItemById_Lingna(itemId);
            return i;
        }

        public int getCode(string prefix)
        {
            return sFacade.getCodeGeneratorValue_Lingna(prefix);
        }

        public void createEmergencyDisbursement(List<DisbursementDetails> DBList, string depName)
        {
            disbursement d = new disbursement();
            d.disbursementId = "DB"+sFacade.getCodeGeneratorValue_Lingna("DB");
            d.departmentId = departmentFacade.getDepartment_Lingna(depName).departmentId;
            d.date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            d.deliveryDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            d.status = "Completed";
            dFacade.createDisbursement(d);
            foreach(DisbursementDetails ds in DBList)
            {
                disbursementDetail n = new disbursementDetail();
                n.deliveredQty = ds.DeliveredQty;
                n.disbursementId = d.disbursementId;
                n.itemId = ds.ItemId;
                n.requestQty = ds.RequestedQty;
                cataFacade.updateStockAfterDisbursement_Lingna(ds.DeliveredQty, ds.ItemId);
                dFacade.createDisbursementDetail(n, d);
            }
            sFacade.updateCodeGeneratorValue_Lingna("DB");
        }
    }
}