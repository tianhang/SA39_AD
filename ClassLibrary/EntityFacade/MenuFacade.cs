using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ClassLibrary.Entities;


namespace ClassLibrary.EntityFacade
{
    public class MenuFacade
    {
        SSISDBEntities entity;
        Menu menuEntity;

        public MenuFacade()
        {
            entity = new SSISDBEntities();
        }

        public DataTable getMenuDataList()
        {
            var menuData = from x in entity.menus select x;
            return getMenuDataTable(menuData.ToList());
        }

        public DataTable getParentMenuDataList()
        {
            var menuData = from x in entity.menus where x.parentId == "0" select x;

            return getMenuDataTable(menuData.ToList());
        }

        public menu getMenuDataById(int menuId)
        {
            var menuData = from x in entity.menus where x.menuId == menuId select x;
            return menuData.SingleOrDefault();
        }

        public Menu fillMenuEntity(string menuId)
        {
            menu menuData = getMenuDataById(Int32.Parse(menuId));

            menuEntity = new Menu();

            menuEntity.MenuID = menuData.menuId.ToString();
            menuEntity.MenuName = menuData.menu1;
            menuEntity.Url = menuData.url;
            menuEntity.ParentId = menuData.parentId;

            return menuEntity;
        }

        public DataTable getMenuDataTable(List<menu> menuList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("menuid", String.Empty.GetType());
            dt.Columns.Add("menu", String.Empty.GetType());
            dt.Columns.Add("ParentID", String.Empty.GetType());
            dt.Columns.Add("url", String.Empty.GetType());
            for (int i = 0; i < menuList.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["menuid"] = menuList[i].menuId.ToString();
                dr["menu"] = menuList[i].menu1;
                dr["parentid"] = menuList[i].parentId;
                dr["url"] = menuList[i].url;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public int addMenu(Menu menuEntity)
        {
            menu menuData = new menu();

            var menuNewID = from x in entity.menus orderby x.menuId descending select x;
            menuData.menuId = (menuNewID.FirstOrDefault().menuId) + 1;
            menuData.menu1 = menuEntity.MenuName;
            menuData.url = menuEntity.Url;
            menuData.parentId = menuEntity.ParentId;

            entity.menus.Add(menuData);
            entity.SaveChanges();

            return menuData.menuId;
        }

        public void addMenuByRole(int menuid, List<string> roleIdList)
        {
            //delete all menubyrole as menuid
            this.deleteMenuByRole(menuid);
            //update new all
            for (int i = 0; i < roleIdList.Count; i++)
            {
                menuByRole mbRoleData = new menuByRole();
                var mbRoleID = from x in entity.menuByRoles orderby x.menuByRoleId descending select x;
                mbRoleData.menuByRoleId = (mbRoleID.FirstOrDefault().menuByRoleId) + 1;
                mbRoleData.menuId = menuid;
                mbRoleData.roleId = roleIdList[i];

                entity.menuByRoles.Add(mbRoleData);
                entity.SaveChanges();
            }
        }

        public void deleteMenuByRole(int menuID)
        {
            //delete menu by role first
            var mbRoleData = from x in entity.menuByRoles where x.menuId == menuID select x;
            List<menuByRole> mbRoleList = new List<menuByRole>();
            mbRoleList = mbRoleData.ToList();
            for (int i = 0; i < mbRoleList.Count; i++)
            {
                menuByRole mbRole = new menuByRole();
                mbRole = mbRoleList[i];
                entity.menuByRoles.Remove(mbRole);
                entity.SaveChanges();
            }
        }

        public List<string> getMenuByRole(int menuID)
        {
            var mbRoleData = from x in entity.menuByRoles where x.menuId == menuID select x.roleId;
            return mbRoleData.ToList();
        }

        public void updateMenu(Menu menuEntity)
        {
            int menuid = int.Parse(menuEntity.MenuID);
            menu menuData = (from x in entity.menus where x.menuId == menuid select x).SingleOrDefault();
            menuData.menu1 = menuEntity.MenuName;
            menuData.url = menuEntity.Url;
            entity.SaveChanges();
        }

        public void deleteMenu(int menuID)
        {
            //delete menu by role first
            this.deleteMenuByRole(menuID);

            //delete menu form menu table
            menu menuData = entity.menus.First(i => i.menuId == menuID);
            entity.menus.Remove(menuData);
            entity.SaveChanges();
        }

        public List<role> getRoleList()
        {
            var roleData = from x in entity.roles select x;
            return roleData.ToList();
        }
    }
}
