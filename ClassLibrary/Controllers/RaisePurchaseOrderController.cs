using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.EntityFacade;
using System.Data;
using ClassLibrary.Entities;

namespace ClassLibrary.Controllers
{
    public class RaisePurchaseOrderController
    {
        SupplierFacade supplierFacede = new SupplierFacade();
        CatalogueFacade catalogueFacade = new CatalogueFacade();

        public List<string> getCategoryList()                                                                  //for dropdownlist
        {
            return supplierFacede.getCategorynames_Lingna();
        }

        //public DataTable SetInitialTable()                                                                    //delete this method in 3/11
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("ItemDescription", typeof(string));
        //    dt.Columns.Add("Qty", typeof(string));
        //    dt.Columns.Add("Supplier", typeof(string));
        //    dt.Columns.Add("Price", typeof(string));
        //    dt.Columns.Add("Amount", typeof(string));
        //    dt.AcceptChanges();
        //    return dt;
        //}

        public List<Item> acceptItemList()                                                                           //modify in 3/11
        {
            List<item> items = catalogueFacade.getItems_Lingna();
            List<Item> l = new List<Item>();
            foreach (item i in items)
            {
                Item n = new Item();
                n.Description = i.description;
                n.UomName = i.uomeasure.uom;
                n.StockBalance = Convert.ToInt32(i.stockBalance);
                n.ReorderLevel = Convert.ToInt32(i.reorderLevel);
                n.ReorderQty = Convert.ToInt32(i.reorderQty);
                l.Add(n);
            }
            return l;
        }

        public List<Item> selectSearch(string category)  //click dropdownlist to get                                      //modify in 3/11
        {
            List<item> items = catalogueFacade.getItemByCategory_Lingna(category);
            List<Item> l = new List<Item>();
            foreach (item i in items)
            {
                Item n = new Item();
                n.Description = i.description;
                n.UomName = i.uomeasure.uom;
                n.StockBalance = Convert.ToInt32(i.stockBalance);
                n.ReorderLevel = Convert.ToInt32(i.reorderLevel);
                n.ReorderQty = Convert.ToInt32(i.reorderQty);
                l.Add(n);
            }
            return l;
        }

        public string ConvertName(string itemName)
        {
            if (itemName.Contains("&quot;"))
            {
                itemName = itemName.Replace("&quot;", @"""");
            }
            return itemName;
        }

        public List<string> selectSuppliers(string itemName)
        {
            itemName = ConvertName(itemName);
            return supplierFacede.getSupplierForItem_Lingna(itemName);
        }

        public double selectAddtoOrder(string itemName, string supplierName)
        {
            itemName = ConvertName(itemName);
            supplierPrice sp = supplierFacede.getItemPrice_Lingna(supplierName, itemName);
            return Convert.ToDouble(sp.price);
        }

        public bool checkLessThanBalance(string itemName)
        {
            bool flag = false;
            itemName = ConvertName(itemName);
            item i = catalogueFacade.getItem_Lingna(itemName);
            if (i.stockBalance < i.reorderLevel)
                flag = true;
            return flag;
        }

        public void createReorderItem(ReorderItem ri)                                                             //modify in 3/11    
        {
            reorderItem r = new reorderItem();
            string itemName = ConvertName(ri.ItemName);
            r.itemId = catalogueFacade.getItem_Lingna(itemName).itemId;
            string supplierName = ri.SupplierName;
            r.supplierId = supplierFacede.getSupplier_Lingna(supplierName).supplierId;
            r.qty = ri.QtyToOrder;
            r.amount = ri.Amount;
            r.status = "Pending";
            r.reorderId = "RI" + supplierFacede.getCodeGeneratorValue_Lingna("RI");
            r.userId = ri.UserId;                                                                     //get the userId from the Session
            supplierFacede.createReorderItem_Lingna(r);
            supplierFacede.updateCodeGeneratorValue_Lingna("RI");
        }
    }
}
