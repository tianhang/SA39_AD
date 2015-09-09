using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Entities;

namespace ClassLibrary.GaoFan
{
    public class categoryGF
    {
        SSISDBEntities ctx = new SSISDBEntities();
        public List<Category> getAllCategoryGF()
        {
            var item = from o in ctx.categories select o;
            List<Category> list = new List<Category>();
            foreach (var i in item)
            {
                Category c = new Category();
                c.CategoryId = i.categoryId;
                c.CategoryName = i.name;
                list.Add(c);
            }
            return list;
            //gaofan
        }

        public string getCategoryIdByNameGF(string name)
        {
           
            category c = ctx.categories.FirstOrDefault(o => o.name == name);
            return c.categoryId;
            //gaofan
        }
    }
}
