using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.EntityFacade;
using ClassLibrary.Entities;

namespace ClassLibrary.Controllers
{

    public class ManageSuppliersController
    {
        SupplierFacade supplierFacade = new SupplierFacade();
        CatalogueFacade catalogueFacade = new CatalogueFacade();                                                        //modify in 3/11  just add this sentence
        public List<supplier> acceptSupplierList()
        {
            return supplierFacade.getSupplierList_Lingna();
        }

        public supplier acceptSupplier(string supplierName)
        {
            return supplierFacade.getSupplier_Lingna(supplierName);
        }

        public void selectUpdate(string supplierName, string contact, string phone, string fax, string address)
        {
            supplier s = supplierFacade.getSupplier_Lingna(supplierName);
            s.address = address;
            s.contactName = contact;
            s.phoneNo = Convert.ToDecimal(phone);
            s.faxNo = Convert.ToDecimal(fax);
            supplierFacade.updateSupplier_Lingna(s);
        }

        public void selectUpdatePrice(string supplierid, string itemId, string price)
        {
            supplierPrice sp = supplierFacade.getItemPriceById_Lingna(supplierid, itemId);
            sp.price = Convert.ToDouble(price);
            supplierFacade.updateSupplierPrice_Lingna(sp);
        }

        public List<SupplierPrice> acceptSupplierPriceList(string supplierId)                                                                // modify in 3/11
        {
            List<supplierPrice> s = supplierFacade.getAllItemPrice_Lingna(supplierId);
            List<SupplierPrice> l = new List<SupplierPrice>();
            foreach (supplierPrice i in s)
            {
                SupplierPrice n = new SupplierPrice();
                n.ItemName = i.item.description;
                n.ItemId = i.itemId;
                n.Price = Convert.ToDouble(i.price);
                l.Add(n);
            }
            return l;
        }

        public void selectAddNewSupplier(string supplierId, string supplierName, string contact, string phone, string fax, string Address)      //modify in 3/11
        {
            supplier s = new supplier();
            s.address = Address;
            s.contactName = contact;
            s.name = supplierName;
            s.supplierId = supplierId;
            s.phoneNo = Convert.ToDecimal(phone);
            s.faxNo = Convert.ToDecimal(fax);
            s.status = "Active";
            supplierFacade.createSupplier_Lingna(s);

            List<item> items = catalogueFacade.getItems_Lingna();
            foreach (item i in items)
            {
                supplierPrice sp = new supplierPrice();
                sp.supplierId = supplierId;
                sp.itemId = i.itemId;
                sp.price = 0;
                supplierFacade.createSupplierPrice_Lingna(sp);
            }
        }

        public void selectDeleteSupplier(string SupplierId)                                                 //for delete modify in 3/12
        {
            supplierFacade.deleteSupplier_Lingna(SupplierId);
            List<supplierPrice> list = supplierFacade.getAllItemPrice_Lingna(SupplierId);
            foreach (supplierPrice sp in list)
            {
                supplierFacade.deleteSupplierPrice_Lingna(sp.supplierId, sp.itemId);
            }
        }
        public void selectDeleteSupplierPrice(string SupplierId, string itemId)                             //for delete modify in 3/12
        {
            supplierFacade.deleteSupplierPrice_Lingna(SupplierId, itemId);
        }
    }
}
