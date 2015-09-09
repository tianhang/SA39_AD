using ClassLibrary.Entities;
using ClassLibrary.EntityFacade;
using ClassLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Controllers
{
    public class ManageCatalogueController
    {
        CatalogueFacade clf = new CatalogueFacade();
        public List<Item> getItems()
        {
            List<Item> list = new List<Item>();
            foreach (item i in clf.getItems_Yuanyuan())
            {
                Item item = new Item();
                item.CategoryName = clf.getCategoryName_Yuanyuan(i.categoryId);
                item.ItemId = i.itemId;
                item.ReorderLevel = (int)i.reorderLevel;
                item.ReorderQty = (int)i.reorderQty;
                item.StockBalance = (int)i.stockBalance;
                item.UomName = clf.getUom_Yuanyuan(i.uomId);
                item.Description = i.description;
                list.Add(item);
            }
            return list;
        }

        public void createItem(string itemId, string categoryId, string uomId, int reorderLevel, int reorderQty, int stockBalance, string description)
        {
            item i = new item();
            i.itemId = itemId;
            i.categoryId = categoryId;
            i.uomId = uomId;
            i.reorderLevel = reorderLevel;
            i.reorderQty = reorderQty;
            i.stockBalance = stockBalance;
            i.description = description;
            i.status = "Active";
            clf.createItem_Yuanyuan(i);
        }

        //public void deleteItem_Yuanyuan(string itemId) {
        //        clf.deleteItem_Yuanyuan(itemId);
        //}
        public void deleteItem(string itemId)
        {
            clf.deleteItem_Yuanyuan(itemId);
        }

        public void updateItem(string itemId, string description, int reorderLevel, int reorderQty,int stockBalance)
        {
            clf.updateItem_Yuanyuan(itemId, description, reorderLevel, reorderQty,stockBalance);
        }

        public List<string> getCategories()
        {
            return clf.getCategories_Yuanyuan();
        }

        public string getCategoryId(string name)
        {
            return clf.getCategoryId_Yuanyuan(name);
        }

        public List<string> getUoms()
        {
            return clf.getUoms_Yuanyuan();
        }

        public string getUomId(string name)
        {
            return clf.getUomId_Yuanyuan(name);
        }

        public void getItemName(string itemId)
        {

        }

        public void getItemDetails()
        {

        }

        public void updateStock(int qty)
        {

        }
    }
}
