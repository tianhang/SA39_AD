using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Text;
using ClassLibrary.Entities;

namespace SSISWebApplication
{
    public partial class SiteMaster : MasterPage
    {
        //Menu Varialbes
        static string constr = "Provider=SQLOLEDB;Data Source=(local);Integrated Security=SSPI;Initial Catalog=SSISDB";
        OleDbConnection cn = new OleDbConnection(constr);
        User userEntity;
        string userRole = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserEntity"] != null)
            {
                userEntity = (User)Session["UserEntity"];
                userRole = userEntity.RoleId;
                this.aUserName.InnerHtml = " : "+userEntity.UserName;
                this.aDepartmentName.InnerHtml = userEntity.DepartmentName;
                PopulateRootLevel();
                Context.GetOwinContext().Authentication.SignIn();
            }
            else
            {
                Response.Redirect("~/WebPages/Login/Login.aspx");
            }
        }

        protected void logoff_click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebPages/Login/Login.aspx");
        }

        protected void lbTitle_click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
        protected void lbAboutUs_click(object sender, EventArgs e)
        {
            Response.Redirect("~/About.aspx");
        }

        #region MenuPopulate
        private void PopulateRootLevel()
        {
            string selectMenu = @"select m.menuid,m.menu,m.url,(select count(*) FROM Menu WHERE parentid=m.menuid) childnodecount FROM Menu m where parentID ='0'";
            string selectMenuByRole = @"select m.menuid,m.menu,m.url,(select count(*) FROM Menu WHERE parentid=m.menuid) childnodecount FROM menubyrole mr inner join menu m on m.menuid=mr.menuid where m.parentID = '0' and mr.roleid ='" + userRole + "'";
            OleDbCommand objCommand = new OleDbCommand(selectMenuByRole, cn);
            OleDbDataAdapter da = new OleDbDataAdapter(objCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            PopulateNodes(dt, MainMenu.Items);
        }

        private void PopulateSubLevel(int parentid, MenuItem parentMenu)
        {
            string selectMenu = @"select m.menuid,m.menu,m.url,(select count(*) FROM Menu WHERE parentid=m.menuid) childnodecount FROM Menu m where parentID=" + parentid + "";
            string selectMenuByRole = @"select m.menuid,m.menu,m.url,(select count(*) FROM Menu WHERE parentid=m.menuid) childnodecount FROM menubyrole mr inner join menu m on m.menuid=mr.menuid where parentID=" + parentid + " and mr.roleid ='" + userRole + "'";
            OleDbCommand objCommand = new OleDbCommand(selectMenuByRole, cn);
            OleDbDataAdapter da = new OleDbDataAdapter(objCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            PopulateNodes(dt, parentMenu.ChildItems);
        }

        private void PopulateNodes(DataTable dt, MenuItemCollection items)
        {
            foreach (DataRow dr in dt.Rows)
            {
                MenuItem mi = new MenuItem();

                mi.Text = dr["menu"].ToString();
                mi.Value = dr["menuid"].ToString();
                mi.NavigateUrl = dr["url"].ToString();
                items.Add(mi);
                //If node has child nodes, then enable on-demand populating
                bool flag = ((int)(dr["childnodecount"]) > 0);
                if (flag)
                {
                    menuCreate(mi);
                }
            }
        }
        private void menuCreate(MenuItem m)
        {
            MenuEventArgs e = new MenuEventArgs(m);
            PopulateSubLevel(Int32.Parse(e.Item.Value), e.Item);
        }
        #endregion
    }

}