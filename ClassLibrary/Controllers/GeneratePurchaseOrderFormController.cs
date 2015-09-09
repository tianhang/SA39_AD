using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.EntityFacade;
using ClassLibrary.Entities;
using ClassLibrary.Helper;

namespace ClassLibrary.Controllers
{
    public class GeneratePurchaseOrderFormController
    {
        SupplierFacade supplierFacede = new SupplierFacade();
        CatalogueFacade catalogueFacade = new CatalogueFacade();
        public void loadController()
        {

        }

        public List<string> acceptSupplierList()                                              //modify in 3/11
        {
            List<supplier> list = supplierFacede.getSupplierList_Lingna();
            List<string> names = new List<string>();
            foreach (supplier s in list)
            {
                names.Add(s.name);
            }
            return names;
        }

        //public List<ReorderItem> selectSupplier(string name)                                    //modify in 3/11          
        //{
        //    List<reorderItem> list = supplierFacede.getReorderItemsBySupplier_Lingna(name);
        //    List<ReorderItem> l = new List<ReorderItem>();
        //    foreach (reorderItem i in list)
        //    {
        //        ReorderItem n = new ReorderItem();
        //        n.ItemName = i.item.description;
        //        n.SupplierName = i.supplier.name;
        //        n.QtyToOrder = Convert.ToInt32(i.qty);
        //        n.Amount = Convert.ToInt32(i.amount);
        //        l.Add(n);
        //    }
        //    return l;
        //}


        public List<ReorderItem> selectSupplier(string name)                                    //modify in 3/18        
        {
            List<reorderItem> list = supplierFacede.getReorderItemsBySupplier_Lingna(name);
            List<ReorderItem> l = new List<ReorderItem>();
            foreach (reorderItem i in list)
            {
                ReorderItem n = new ReorderItem();
                n.ReorderItemId = i.reorderId;
                n.ItemName = i.item.description;
                n.SupplierName = i.supplier.name;
                n.QtyToOrder = Convert.ToInt32(i.qty);
                n.Amount = Convert.ToInt32(i.amount);
                l.Add(n);
            }
            return l;
        }



        public void selectGenerate(string supplierName, List<ReorderItem> list, List<ReorderItem> Orglist)                        // modify in 3/11
        {
            purchaseOrder po = new purchaseOrder();
            po.purchaseOrderId = "PO" + supplierFacede.getCodeGeneratorValue_Lingna("PO");
            po.supplierId = supplierFacede.getSupplier_Lingna(supplierName).supplierId;
            DateTime date = DateTime.Now;
            po.orderDate = date;
            po.status = "Pending";
            supplierFacede.createPurchaseOrder_Lingna(po);

            foreach(ReorderItem r in Orglist)
            {
                supplierFacede.updateReorderItemStatus_Lingna(r.ReorderItemId, "Completed");
            }

            foreach (ReorderItem n in list)
            {
                purchaseOrderDetail pod = new purchaseOrderDetail();
                pod.purchaseOrderId = po.purchaseOrderId;
                string itemName = ConvertName(n.ItemName);

                pod.itemId = catalogueFacade.getItem_Lingna(itemName).itemId;
                pod.qty = n.QtyToOrder;
                pod.amount = n.Amount;
                supplierFacede.insertPurchaseOrderDetails_Lingna(po.purchaseOrderId, pod);
            }
            supplierFacede.updateCodeGeneratorValue_Lingna("PO");


        }

        public string ConvertName(string itemName)
        {
            if (itemName.Contains("&quot;"))
            {
                itemName = itemName.Replace("&quot;", @"""");
            }
            return itemName;
        }


        public supplier getSupplier(string name)
        {
            return supplierFacede.getSupplier_Lingna(name);
        }

    }
}
