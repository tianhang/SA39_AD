using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Entities;
using ClassLibrary.Helper;

namespace ClassLibrary.EntityFacade
{
    public class CatalogueFacade
    {
        SSISDBEntities ctx;
        ErrorLog errorobj;
        UOM uomEntity;

        public CatalogueFacade()
        {
            ctx = new SSISDBEntities();
            errorobj = new ErrorLog();
        }

        public List<Item> getItems(string status)
        {
            List<Item> itemCollection = new List<Item>();
            try
            {
                var itCollection = from itemDB in ctx.items
                                   where itemDB.status == status
                                   select new
                                   {
                                       itemId = itemDB.itemId,
                                       description = itemDB.description,
                                       bin = itemDB.binNumber,
                                       stockBalance = itemDB.stockBalance
                                   };

                foreach (var i in itCollection)
                {
                    Item item = new Item();
                    item.ItemId = i.itemId;
                    item.Description = i.description;
                    item.BinNumber = i.bin;
                    item.StockBalance = Convert.ToInt32(i.stockBalance);

                    itemCollection.Add(item);
                }
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("CatalogueFacade-getItems():::" + exception.ToString());
                itemCollection = new List<Item>();
            }
            

            return itemCollection;

        }

        public void createItem()
        {

        }

        public void updateItem()
        {

        }

        public void getItemName(string itemId)
        {

        }

        public void getItemDetails()
        {

        }

        public void updateStock(string itemId, int stockQty)
        {
            try
            {
                //ctx.updateStockPro(itemId, stockQty);

                using(SSISDBEntities ct = new SSISDBEntities())
                {
                    item it = (from i in ct.items
                               where i.itemId == itemId
                               select i).FirstOrDefault();

                    int result = Convert.ToInt32(it.stockBalance) - stockQty;
                    if(result <0)
                    {
                        it.stockBalance = 0;
                    }
                    else
                    {
                        it.stockBalance = result;
                    }

                    ct.SaveChanges();
                }
                
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("CatalogueFacade-updateStock():::" + exception.ToString());
            }
        }

        public void updateStockret(string itemId, int stockQty)
        {
            try
            {
                ctx.updateStockPro(itemId, stockQty);

            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("CatalogueFacade-updateStockret():::" + exception.ToString());
            }
        }



        public List<Item> getItemsForReorder(string status)
        {
            List<Item> itemCollection = new List<Item>();
            try
            {
                var itCollection = from itemDB in ctx.items
                                   where itemDB.status == status && itemDB.stockBalance<=itemDB.reorderLevel
                                   select new
                                   {
                                       itemId = itemDB.itemId,
                                       desc = itemDB.description
                                   };

                foreach (var i in itCollection)
                {
                    Item item = new Item();
                    item.ItemId = i.itemId;
                    item.Description = i.desc;
                    itemCollection.Add(item);
                }
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("CatalogueFacade-getItemsForReorder():::" + exception.ToString());
                itemCollection = new List<Item>();
            }


            return itemCollection;
        }



        #region Lingna's Work

        public List<item> getItems_Lingna()                               //modify in 3/11
        {
            var items = from o in ctx.items
                        where o.status == "Active"
                        select o;
            return items.ToList(); ;
        }

        public item getItemById_Lingna(string id)                           // modify in 3/17
        {
            var i = ctx.items.First(o => o.itemId == id);
            return i;
        }

        public List<item> getItemByCategory_Lingna(string category)       //modify in 3/11
        {
            var items = ctx.items.Where(o => o.category.name == category);
            return items.ToList();
        }

        public item getItem_Lingna(string itemName)
        {
            item i = ctx.items.FirstOrDefault(o => o.description == itemName);
            return i;
        }

        public void updateStock_Lingna(int qty, string itemName)
        {
            var i = ctx.items.FirstOrDefault(o => o.description == itemName);
            i.stockBalance = qty;
            ctx.SaveChanges();
        }

        public void updateStockAfterDisbursement_Lingna(int qty, string itemId)             // Add new method in 3/18
        {
            var i = ctx.items.FirstOrDefault(o => o.itemId == itemId);
            i.stockBalance = i.stockBalance - qty;
            ctx.SaveChanges();
        }

        #endregion


        #region Yuanyuan's Part

        public List<item> getItems_Yuanyuan()
        {
            var query = from d in ctx.items
                        where d.status.Equals("Active")
                        select d;
            return query.ToList();
        }

        public List<string> getCategories_Yuanyuan()
        {
            var query = from c in ctx.categories
                        select c.name;
            return query.ToList();
        }

        public string getCategoryId_Yuanyuan(string name)
        {
            var query = from n in ctx.categories
                        where n.name == name
                        select n.categoryId;
            return query.First();
        }

        public string getCategoryName_Yuanyuan(string id)
        {
            var query = from n in ctx.categories
                        where n.categoryId == id
                        select n.name;
            return query.First();
        }

        public List<string> getUoms_Yuanyuan()
        {
            var query = from c in ctx.uomeasures
                        select c.uom;
            return query.ToList();
        }

        public string getUomId_Yuanyuan(string name)
        {
            var query = from n in ctx.uomeasures
                        where n.uom == name
                        select n.uomId;
            return query.First();
        }

        public string getUom_Yuanyuan(string id)
        {
            var query = from n in ctx.uomeasures
                        where n.uomId == id
                        select n.uom;
            return query.First();
        }

        public void createItem_Yuanyuan(item i)
        {
            ctx.items.Add(i);
            ctx.SaveChanges();
        }

        public void updateItem_Yuanyuan(string itemId, string description, int reorderLevel, int reorderQty,int stockBalance)
        {
            item item = ctx.items.First(i => i.itemId == itemId);
            item.description = description;
            item.reorderLevel = reorderLevel;
            item.reorderQty = reorderQty;
            item.stockBalance = stockBalance;
            ctx.SaveChanges();
        }

        public void deleteItem_Yuanyuan(string itemId)
        {
            item myitem = ctx.items.First(i => i.itemId == itemId);
            myitem.status = "Inactive";
            ctx.SaveChanges();
        }

        #endregion


        #region Pyae Pyae's Part

        public List<category> getAllCategory_PyaePyae()
        {
            var categoryData = from x in ctx.categories
                               select x;
            return categoryData.ToList();
        }

        public List<item> getItems_PyaePyae(string categoryId)
        {
            var itemData = from x in ctx.items
                           where x.categoryId == categoryId
                           select x;
            return itemData.ToList();
        }

        public string getCategoryName_PyaePyae(string itemId)
        {
            var categoryName = from i in ctx.items
                               join c in ctx.categories
                               on i.categoryId equals c.categoryId
                               where i.itemId == itemId
                               select c.name;
            return categoryName.ToString();
        }

        public UOM getUOMName_PyaePyae(string itemId)
        {
            var uomData = from i in ctx.items
                          join u in ctx.uomeasures
                          on i.uomId equals u.uomId
                          where i.itemId == itemId
                          select u;
            return fillUOMEntity_PyaePyae(uomData.SingleOrDefault());
        }

        public UOM fillUOMEntity_PyaePyae(uomeasure uomData)
        {
            uomEntity = new UOM();
            uomEntity.UomId = uomData.uomId;
            uomEntity.Uom = uomData.uom;
            return uomEntity;
        }

        public string getItemName_PyaePyae(string itemId)
        {
            var itemName = from x in ctx.items
                           where x.itemId == itemId
                           select x.description;
            return itemName.ToString();
        }

        #endregion

    }
}
