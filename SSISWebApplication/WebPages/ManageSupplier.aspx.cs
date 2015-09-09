using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.Controllers;

namespace SSISWebApplication.WebPages
{
    public partial class ManageSupplier : System.Web.UI.Page
    {
        ManageSuppliersController controller = new ManageSuppliersController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SupplierGridView.DataSource = controller.acceptSupplierList();
                SupplierGridView.DataBind();
            }
            Label2.Text = "";
        }

        protected void SupplierGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SupplierGridView.EditIndex = e.NewEditIndex; 
            SupplierGridView.DataSource = controller.acceptSupplierList();
            SupplierGridView.DataBind(); 
        }

        protected void SupplierGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)                          //modify in 3/12
        {
            GridViewRow row = (GridViewRow)SupplierGridView.Rows[e.RowIndex];
            string Id = ((Label)row.Cells[1].FindControl("LabelSupplierId")).Text;
            controller.selectDeleteSupplier(Id);
            SupplierGridView.DataSource = controller.acceptSupplierList();
            SupplierGridView.DataBind();
            PriceGridView.DataSource = null;
            PriceGridView.DataBind();
        }

        protected void SupplierGridView_CancelEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SupplierGridView.EditIndex = -1;
            SupplierGridView.DataSource = controller.acceptSupplierList();
            SupplierGridView.DataBind(); 
        }
        protected void SupplierGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)SupplierGridView.Rows[e.RowIndex];
            string name = ((Label)row.Cells[2].FindControl("LabelSupplierName")).Text; 
            string contact = ((TextBox)row.Cells[3].FindControl("TextBoxEditContactName")).Text;
            string phone = ((TextBox)row.Cells[4].FindControl("TextBoxEditPhoneNo")).Text; 
            string fax = ((TextBox)row.Cells[5].FindControl("TextBoxEditFaxNo")).Text; 
            string address = ((TextBox)row.Cells[6].FindControl("TextBoxEditAddress")).Text; 

            controller.selectUpdate(name, contact, phone, fax, address);

            SupplierGridView.EditIndex = -1; 
            SupplierGridView.DataSource = controller.acceptSupplierList();
            SupplierGridView.DataBind();

            Label2.Text = "Update Seccessful!";
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            string Address = ((TextBox)SupplierGridView.FooterRow.FindControl("TextBoxAddress")).Text;
            string phone = ((TextBox)SupplierGridView.FooterRow.FindControl("TextBoxPhoneNo")).Text;
            string fax = ((TextBox)SupplierGridView.FooterRow.FindControl("TextBoxFaxNo")).Text;
            string contact = ((TextBox)SupplierGridView.FooterRow.FindControl("TextBoxContactName")).Text;
            string supplierName = ((TextBox)SupplierGridView.FooterRow.FindControl("TextBoxSupplierName")).Text;
            string supplieId = ((TextBox)SupplierGridView.FooterRow.FindControl("TextBoxSupplierId")).Text;

            controller.selectAddNewSupplier(supplieId, supplierName, contact, phone, fax, Address);
            SupplierGridView.DataSource = controller.acceptSupplierList();
            SupplierGridView.DataBind();
        }

        protected void PriceGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            PriceGridView.EditIndex = e.NewEditIndex; 
            string supplierId = Convert.ToString(Session["supplierId"]);
            PriceGridView.DataSource = controller.acceptSupplierPriceList(supplierId);
            PriceGridView.DataBind(); 
        }

        protected void PriceGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            PriceGridView.EditIndex = -1; 
            string supplierId = Convert.ToString(Session["supplierId"]);
            PriceGridView.DataSource = controller.acceptSupplierPriceList(supplierId);
            PriceGridView.DataBind(); 
        }

        protected void PriceGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)PriceGridView.Rows[e.RowIndex];
            string supplierId = Convert.ToString(Session["supplierId"]);
            string itemId = ((Label)row.Cells[0].FindControl("LabelItemID")).Text;
            string description = ((Label)row.Cells[1].FindControl("LabelDescription")).Text; 
            string price = ((TextBox)row.Cells[2].FindControl("TextBoxEditPrice")).Text; 

            controller.selectUpdatePrice(supplierId, itemId, price);

            PriceGridView.EditIndex = -1;
            PriceGridView.DataSource = controller.acceptSupplierPriceList(supplierId);
            PriceGridView.DataBind();

            Label2.Text = "Update Seccessful!";
        }

        protected void SupplierGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SupplierGridView.SelectedRow != null)
            {
                GridViewRow row = (GridViewRow)SupplierGridView.SelectedRow;
                string supplierId = ((Label)row.Cells[1].FindControl("LabelSupplierId")).Text;
                Session["supplierId"] = supplierId;
                PriceGridView.DataSource = controller.acceptSupplierPriceList(supplierId);
                PriceGridView.DataBind();
            }
        }

        protected void PriceGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PriceGridView.PageIndex = e.NewPageIndex;
            string supplierId = Convert.ToString(Session["supplierId"]);
            PriceGridView.DataSource = controller.acceptSupplierPriceList(supplierId);
            PriceGridView.DataBind();
        }

        protected void PriceGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)                      //modify in 3/12
        {
            GridViewRow row = (GridViewRow)PriceGridView.Rows[e.RowIndex];
            string supplierId = Convert.ToString(Session["supplierId"]);
            string itemId = ((Label)row.Cells[0].FindControl("LabelItemID")).Text;
            controller.selectDeleteSupplierPrice(supplierId, itemId);
            PriceGridView.DataSource = controller.acceptSupplierPriceList(supplierId);
            PriceGridView.DataBind();
        }

    }
}