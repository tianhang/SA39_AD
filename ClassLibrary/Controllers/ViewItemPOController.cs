using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.EntityFacade;

namespace ClassLibrary.Controllers
{
    public class ViewItemPOController
    {
        SupplierFacade supplierfacade = new SupplierFacade();
        public List<reorderItem> acceptPurchaseOrderList(string itemName)
        {
            if (itemName.Contains("&quot;"))
            {
                itemName = itemName.Replace("&quot;", @"""");
            }
            return supplierfacade.getPurchaseOrderByItem_Lingna(itemName);
        }
    }
}
