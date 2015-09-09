using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Entities;

namespace ClassLibrary.GaoFan
{
    public class itemGF
    {
        SSISDBEntities ctx = new SSISDBEntities();
        public List<string> getAllItemByCategoryIdGF(string categoryId)
        {
            var item = from o in ctx.items where o.categoryId == categoryId select o.description;
            List<string> list = item.ToList();
            return list;
            //gaofan
        }

        public string getItemNameByItemIdGF(string itemId)
        {
            item i = ctx.items.FirstOrDefault(o => o.itemId == itemId);
            return i.description;
            //gaofan
        }

        public Category getCategoryByDescriptionGF(string description)
        {
            category x = ctx.categories.FirstOrDefault(o => o.name == description);
            Category c = new Category();
            c.CategoryId = x.categoryId;
            c.CategoryName = x.name;
            return c;
            //gaofan
        }

        public Item getItemByDescriptionGF(String discription) { 
            item i = ctx.items.FirstOrDefault(o => o.description == discription);
            Item a = new Item();
            a.ItemId = i.itemId;
            a.Description = i.description;
            a.CategoryId = i.categoryId;
            a.CategoryName = i.category.name;
            a.UomId = i.uomId;
            a.UomName = i.uomeasure.uom;
            a.UomNumber = Convert.ToInt32(i.uomeasure.number);
            a.StockBalance =Convert.ToInt32(i.stockBalance);
            a.ReorderLevel = Convert.ToInt32(i.reorderLevel);
            a.ReorderQty = Convert.ToInt32(i.reorderQty);
            return a;
        }

        public List<Item> getItemByCategoryIdGF(string categoryId) {
            var item = from o in ctx.items
                       where o.categoryId == categoryId
                       select new
                       {
                             ItemId = o.itemId,
            Description = o.description,
            CategoryId = o.categoryId,
            CategoryName = o.category.name,
            UomId = o.uomId,
            UomName = o.uomeasure.uom,
            UomNumber = o.uomeasure.number,
            StockBalance =o.stockBalance,
            ReorderLevel = o.reorderLevel,
            ReorderQty = o.reorderQty
                       };
            List<Item> list = new List<Item>();
            foreach(var i in item){
                Item a = new Item();
                a.ItemId = i.ItemId;
                a.Description = i.Description;
                a.CategoryId = i.CategoryId;
                a.CategoryName = i.CategoryName;
                a.UomId = i.UomId;
                a.UomName = i.UomName;
                a.UomNumber = Convert.ToInt32(i.UomNumber);
                a.StockBalance = Convert.ToInt32(i.StockBalance);
                a.ReorderLevel = Convert.ToInt32(i.ReorderLevel);
                a.ReorderQty = Convert.ToInt32(i.ReorderQty);
                list.Add(a);
            }
            return list;
        }
    }
}
