using ClassLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.EntityFacade
{
    public class SupplierFacade
    {
        SSISDBEntities ctx = new SSISDBEntities();
        public SupplierFacade()
        {

        }

        public string getReorderItems(string status)
        {
            return status;
        }

        public void getSupplier()
        {

        }

        public string getReorderItemsBySupplier(string status)
        {
            return status;
        }

        public string getItemPrice(string supplierId, string itemId)
        {
            return supplierId;
        }

        public string getSupplierForItem(string itemId)
        {
            return itemId;
        }

        public string getPurchaseOrderByItem(string itemId)
        {
            return itemId;
        }

        public void updateSupplier()
        {
        }

        public string updateReorderItemStatus(string status)
        {
            return status;
        }

        public void updateSupplierPrice()
        {
        }

        public void createSupplier()
        {
        }

        public string createReorderItem(string itemId, string supplierId)
        {
            return itemId;
        }




        //-----------------------------------------------------------------------


        #region Lingna's Work 

        public supplier getSupplier_Lingna(string supplierName)
        {
            supplier s = ctx.suppliers.FirstOrDefault(o => o.name == supplierName);
            return s;
        }

        public List<string> getCategorynames_Lingna()  //get for category dropdownlist
        {
            var cateogrylist = from o in ctx.categories
                               select o.name;
            return cateogrylist.ToList();
        }

        public List<reorderItem> getReorderItemsByStatus_Lingna()
        {
            var items = from o in ctx.reorderItems
                        where o.status == "Pending"
                        select o;
            return items.ToList();
        }

        //public List<string> getSuppliers()   //getSuppliers( )                                                        //delete this method                                                
        //{
        //    var names = from o in ctx.suppliers
        //                where o.status == "Active"
        //                select o.name;
        //    return names.ToList();
        //}

        public List<supplier> getSupplierList_Lingna()   //dai                                                                    // modify in 3//11
        {
            var suppliers = from o in ctx.suppliers
                            where o.status == "Active"
                            select o;
            return suppliers.ToList();
        }

        public List<reorderItem> getReorderItemsBySupplier_Lingna(string supplierName)  //getReorderItemsBySupplier_Lingna(status)
        {
            var items = from o in ctx.reorderItems
                        where o.status == "Approved" && o.supplier.name == supplierName
                        select o;
            return items.ToList();
        }

        public supplierPrice getItemPrice_Lingna(string supplierName, string itemName) //getItemPrice_Lingna(supplierId,itemId)
        {
            supplierPrice s = ctx.supplierPrices.FirstOrDefault(o => o.supplier.name == supplierName && o.item.description == itemName);
            return s;
        }

        public supplierPrice getItemPriceById_Lingna(string supplierId, string itemId) //dai
        {
            supplierPrice s = ctx.supplierPrices.FirstOrDefault(o => o.supplierId == supplierId && o.itemId == itemId);
            return s;
        }

        public List<supplierPrice> getAllItemPrice_Lingna(string supplierId) //getItemPrice_Lingna(supplierId,itemId)                           // modify in 3//11
        {
            var s = from o in ctx.supplierPrices
                    where o.supplierId == supplierId
                    select o;
            return s.ToList();
        }

        public List<string> getSupplierForItem_Lingna(string itemName)  //getSupplierForItem_Lingna(itemId)  change to (itemName)!!!!
        {
            var suppliers = from o in ctx.supplierPrices
                            where o.item.description == itemName
                            select o.supplier.name;
            return suppliers.ToList();
        }

        public List<reorderItem> getPurchaseOrderByItem_Lingna(string itemName)  //getPurchaseOrderByItem_Lingna(itemId)  change to (itemName)!!!!
        {
            var items = from o in ctx.reorderItems
                        where o.item.description == itemName
                        select o;
            return items.ToList();
        }

        public void updateSupplier_Lingna(supplier s)
        {
            ctx.Entry(s).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
        }

        //public void updateReorderItemStatus_Lingna(string itemName, string status)  //update by itemId or by itemName
        //{
        //    var ri = ctx.reorderItems.First(o => o.item.description == itemName);
        //    ri.status = status;
        //    ctx.SaveChanges();
        //}

        public void updateReorderItemStatus_Lingna(string rid, string status)  //update by itemId or by itemName
        {
            var ri = ctx.reorderItems.First(o => o.reorderId==rid);
            ri.status = status;
            ctx.SaveChanges();
        }

        public void updateSupplierPrice_Lingna(supplierPrice sp)
        {
            ctx.Entry(sp).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
        }

        public void createSupplier_Lingna(supplier s)
        {
            ctx.suppliers.Add(s);
            ctx.SaveChanges();
        }

        public void createSupplierPrice_Lingna(supplierPrice s)                                                             //modify in 3/11
        {
            ctx.supplierPrices.Add(s);
            ctx.SaveChanges();
        }

        public void createReorderItem_Lingna(reorderItem ri)  //createReorderItem_Lingna(itemId,supplierId)
        {
            ctx.reorderItems.Add(ri);
            ctx.SaveChanges();
        }

        public void createPurchaseOrder_Lingna(purchaseOrder po)  //createPurchaseOrder_Lingna( )
        {
            ctx.purchaseOrders.Add(po);
            ctx.SaveChanges();
        }

        public void insertPurchaseOrderDetails_Lingna(string poId, purchaseOrderDetail pod)  //insertPurchaseOrderDetails_Lingna( )
        {
            try
            {
                //purchaseOrder po = ctx.purchaseOrders.FirstOrDefault(o => o.purchaseOrderId == poId);
                //po.purchaseOrderDetails.Add(pod);
                ctx.purchaseOrderDetails.Add(pod);
                ctx.SaveChanges();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        public void updateStatus_Lingna(string poId, string status) //updateStatus_Lingna(poId, status)
        {
            var po = ctx.purchaseOrders.FirstOrDefault(o => o.purchaseOrderId == poId);
            po.status = status;
            ctx.SaveChanges();
        }

        public int getCodeGeneratorValue_Lingna(string prefix)
        {
            var g = ctx.codeGenerators.FirstOrDefault(o => o.prefix == prefix);
            return g.lastValue;
        }

        public void updateCodeGeneratorValue_Lingna(string prefix)
        {
            var g = ctx.codeGenerators.FirstOrDefault(o => o.prefix == prefix);
            g.lastValue++;
            ctx.SaveChanges();
        }

        public void deleteSupplier_Lingna(string supplierId)                                                      //modify in 3/12
        {
            var s = ctx.suppliers.First(o => o.supplierId == supplierId);
            s.status = "Deactive";
            ctx.SaveChanges();
        }

        public void deleteSupplierPrice_Lingna(string supplierId, string itemId)                                  //modify in 3/12
        {
            var sp = from o in ctx.supplierPrices
                     where o.supplierId == supplierId && o.itemId == itemId
                     select o;
            supplierPrice s = sp.First();
            ctx.Entry(s).State = System.Data.Entity.EntityState.Deleted;
            ctx.SaveChanges();
        }

        #endregion




    }
}
