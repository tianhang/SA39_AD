using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Entities;

namespace ClassLibrary.GaoFan
{
   public class supplierPriceGF
    {
       SSISDBEntities ctx = new SSISDBEntities();

       public SupplierPrice getSupplierPriceGF(string itemId,string supplierId) {
           supplierPrice s = ctx.supplierPrices.FirstOrDefault(o => o.itemId == itemId && o.supplierId == supplierId);
           SupplierPrice sp = new SupplierPrice();
           sp.ItemId = s.itemId;
           sp.SupplierId = s.supplierId;
           sp.Price =Convert.ToDouble(s.price);
           sp.ItemName = s.item.description;
           return sp;
       }

        public List<Supplier> getAllSupplier()
       {
           var su = from ss in ctx.suppliers
                    select ss;
           List<Supplier> slist = new List<Supplier>();
            foreach(supplier supp in su.ToList())
            {
                Supplier s = new Supplier();
                s.SupplierId = supp.supplierId;
                s.SupplierName = supp.name;
                s.ContactName = supp.contactName;                
                slist.Add(s);
            }
            return slist;
       }
       

       public Supplier getSupplierByName(string name)
         {
            supplier s = ctx.suppliers.FirstOrDefault(x => x.name == name);
           Supplier supp = new Supplier();

           supp.SupplierId = s.supplierId;
           supp.SupplierName = s.name;
           supp.ContactName = s.contactName;
           return supp;
         }
    }
}
