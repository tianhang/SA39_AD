using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.Controllers;

namespace SSISWebApplication.WebPages.Item
{
    public partial class AddNewItem : System.Web.UI.Page
    {
        ManageCatalogueController mcc = new ManageCatalogueController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string itemId = txtItemId.Text;
            string categoryId = mcc.getCategoryId(DropDownListCategory.Text);
            string uomId = mcc.getUomId(DropDownListUom.Text);
            int reorderLevel = Convert.ToInt32(txtRecordLevel.Text);
            int reorderQty = Convert.ToInt32(txtRecordQuantity.Text);
            int stockBalance = Convert.ToInt32(txtBalance.Text);
            string description = txtDescription.Text;
            try
            {
                mcc.createItem(itemId, categoryId, uomId, reorderLevel, reorderQty, stockBalance, description);
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Successful!')</SCRIPT>");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Invalid item id!')</SCRIPT>");
            }
            dataBind();
        }

        protected void itemTable_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string id = itemTable.Rows[e.RowIndex].Cells[2].Text;
                mcc.deleteItem(id);
                dataBind();
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Item Deleted!')</SCRIPT>");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Access Denied')</SCRIPT>");
            }
        }

        private void dataBind()
        {
            itemTable.DataSource = mcc.getItems();
            itemTable.DataBind();
            DropDownListCategory.DataSource = mcc.getCategories();
            DropDownListCategory.DataBind();
            DropDownListUom.DataSource = mcc.getUoms();
            DropDownListUom.DataBind();
        }

        protected void itemTable_RowEditing(object sender, GridViewEditEventArgs e)
        {
            itemTable.EditIndex = e.NewEditIndex;
            dataBind();
        }

        protected void itemTable_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = itemTable.Rows[e.RowIndex];
            string description = ((TextBox)row.Cells[3].Controls[0]).Text;
            int reorderLevel = Convert.ToInt32(((TextBox)row.Cells[4].Controls[0]).Text);
            int reorderQty = Convert.ToInt32(((TextBox)row.Cells[5].Controls[0]).Text);
            int stockBalance = Convert.ToInt32(((TextBox)row.Cells[6].Controls[0]).Text);
            string id = itemTable.Rows[e.RowIndex].Cells[0].Text;
            mcc.updateItem(id, description, reorderLevel, reorderQty,stockBalance);
            itemTable.EditIndex = -1;
            dataBind();
        }

        protected void itemTable_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            itemTable.EditIndex = -1;
            dataBind();
        }

        protected void itemTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            itemTable.PageIndex = e.NewPageIndex;
            dataBind();
        }
    }
}