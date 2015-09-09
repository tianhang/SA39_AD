using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using ClassLibrary.EntityFacade;
using ClassLibrary.Entities;

namespace SSISWebApplication.WebPages.MenuSetting
{
    public partial class Menu : System.Web.UI.Page
    {
        MenuFacade menuFaca = new MenuFacade();
        ClassLibrary.Entities.Menu menuEntity;

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.initializeControls();
                this.bindParentMenu();
                this.bindRoleList();
            }
            this.bindTreeMenu();
        }

        protected void tvMenuSetting_SelectedNodeChanged(object sender, EventArgs e)
        {
            this.initializeControls();
            menuEntity = menuFaca.fillMenuEntity(tvMenuSetting.SelectedValue);
            this.lblMenuID.Text = menuEntity.MenuID;
            this.txtMenuName.Text = menuEntity.MenuName;
            this.txtUrl.Text = menuEntity.Url;
            this.lblParentID.Text = menuEntity.ParentId;
            dvParentMenu.Visible = false;
            butSave.Text = "Save";
            this.bindToRoleByMenuid();
            txtMenuName.Focus();
        }

        protected void butSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMenuName.Text))
            {
                showMessage("Type Menu name and url!");
                txtMenuName.Focus();
            }
            else
            {
                if (butSave.Text.Equals("Save"))
                {
                    menuEntity = new ClassLibrary.Entities.Menu();
                    menuEntity.MenuID = lblMenuID.Text;
                    menuEntity.MenuName = txtMenuName.Text;
                    menuEntity.Url = txtUrl.Text;
                    menuFaca.updateMenu(menuEntity);
                    menuFaca.addMenuByRole(Int32.Parse(lblMenuID.Text), this.getSelectedRoleIdList());
                    initializeControls();
                }
                else if (butSave.Text.Equals("Add Parent"))
                {
                    menuEntity = new ClassLibrary.Entities.Menu();
                    menuEntity.MenuName = txtMenuName.Text;
                    menuEntity.Url = txtUrl.Text;
                    menuEntity.ParentId = "0";
                    int menuId = menuFaca.addMenu(menuEntity);
                    menuFaca.addMenuByRole(menuId, this.getSelectedRoleIdList());
                    initializeControls();
                }
                else if (butSave.Text.Equals("Add Child"))
                {
                    menuEntity = new ClassLibrary.Entities.Menu();
                    menuEntity.MenuName = txtMenuName.Text;
                    menuEntity.Url = txtUrl.Text;
                    menuEntity.ParentId = ddlParentMenu.SelectedValue;
                    int menuId = menuFaca.addMenu(menuEntity);
                    menuFaca.addMenuByRole(menuId, this.getSelectedRoleIdList());
                    initializeControls();
                }
                bindTreeMenu();
                bindParentMenu();
            }

        }

        protected void butAddParent_Click(object sender, EventArgs e)
        {
            initializeControls();
            dvParentMenu.Visible = false;
            butSave.Text = "Add Parent";
            txtMenuName.Focus();
        }

        protected void butAddChild_Click(object sender, EventArgs e)
        {
            initializeControls();
            dvParentMenu.Visible = true;
            butSave.Text = "Add Child";
            txtMenuName.Focus();
        }
        protected void butCancel_Click(object sender, EventArgs e)
        {
            this.initializeControls();
        }

        protected void butDelete_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                if (!string.IsNullOrEmpty(lblMenuID.Text))
                {
                    menuFaca.deleteMenu(Int32.Parse(lblMenuID.Text));
                    initializeControls();
                    bindTreeMenu();
                }
                else
                {
                    showMessage("Select Menu!");
                }
            }
        }
        #endregion

        #region HelperMethods
        //for treeview use
        private void AddNodes(TreeNodeCollection nodes, int level, DataTable dt)
        {
            string filterExp = string.Format("ParentID='{0}'", level);
            foreach (System.Data.DataRow r in dt.Select(filterExp))
            {
                TreeNode item = new TreeNode()
                {
                    Text = r[1].ToString(),
                    Value = r[0].ToString()
                };
                this.AddNodes(item.ChildNodes, int.Parse(r[0].ToString()), dt);
                nodes.Add(item);
            }
        }

        //Show message
        public void showMessage(string msg)
        {
            dvAlert.Visible = true;
            lblErrorMessage.Text = msg;
            txtMenuName.Focus();
        }

        public void initializeControls()
        {
            this.lblMenuID.Text = string.Empty;
            this.lblParentID.Text = string.Empty;
            this.txtMenuName.Text = string.Empty;
            this.txtUrl.Text = string.Empty;
            this.butSave.Text = "Add Parent";
            dvAlert.Visible = false;
            dvParentMenu.Visible = false;
            this.bindRoleList();
        }

        public void bindParentMenu()
        {
            ddlParentMenu.DataValueField = "menuid";
            ddlParentMenu.DataTextField = "menu";
            ddlParentMenu.DataSource = menuFaca.getParentMenuDataList();
            ddlParentMenu.DataBind();
        }

        public void bindTreeMenu()
        {
            tvMenuSetting.Nodes.Clear();
            this.AddNodes(this.tvMenuSetting.Nodes, 0, menuFaca.getMenuDataList());
        }

        public void bindRoleList()
        {
            chkRoleList.DataValueField = "roleid";
            chkRoleList.DataTextField = "name";
            chkRoleList.DataSource = menuFaca.getRoleList();
            chkRoleList.DataBind();
        }

        public List<string> getSelectedRoleIdList()
        {
            List<string> roleIdList = new List<string>();
            for (int i = 0; i < chkRoleList.Items.Count; i++)
            {
                if (chkRoleList.Items[i].Selected)
                {
                    roleIdList.Add(chkRoleList.Items[i].Value);
                }
            }
            return roleIdList;
        }

        public void bindToRoleByMenuid()
        {
            List<string> roleIdList = new List<string>();
            roleIdList = menuFaca.getMenuByRole(Int32.Parse(menuEntity.MenuID));
            for (int j = 0; j < roleIdList.Count; j++)
            {
                for (int i = 0; i < chkRoleList.Items.Count; i++)
                {

                    if (chkRoleList.Items[i].Value == roleIdList[j])
                    {
                        chkRoleList.Items[i].Selected = true;
                    }

                }
            }
        }
        #endregion
    }
}