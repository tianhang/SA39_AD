using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.EntityFacade;
using System.Text;

namespace SSISWebApplication.WebPages
{
    public partial class ApproveRejectPurchaseOrder : System.Web.UI.Page
    {
        ApproveRejectPurchaseOrderEntityFacade appRejectFaca = new ApproveRejectPurchaseOrderEntityFacade();

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                this.ddlSupplier_Bind();
                //this.bindStatus();
                this.bindGridView();
            }
            dvAlert.Visible = false;
        }

        protected void butSearch_Click(object sender, EventArgs e)
        {
            this.bindGridView();
        }

        protected void butCancel_Click(object sender, EventArgs e)
        {
            //this.bindStatus();
            this.ddlSupplierName.SelectedIndex = 0;
            //this.rdbStatus.SelectedIndex = 0;
            this.bindGridView();
        }

        protected void gvReorderList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approved")
            {
                GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int rowIndex = gvRow.RowIndex;
                TextBox txtRejectReason = (TextBox)gvReorderItemList.Rows[rowIndex].Cells[7].FindControl("txtRejectReason");
                appRejectFaca.updateReorderItemStatus(e.CommandName, txtRejectReason.Text, e.CommandArgument.ToString());
                //this.bindStatus();
                this.bindGridView();
            }
            else if (e.CommandName == "Rejected")
            {
                GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int rowIndex = gvRow.RowIndex;
                TextBox txtRejectReason = (TextBox)gvReorderItemList.Rows[rowIndex].Cells[7].FindControl("txtRejectReason");
                appRejectFaca.updateReorderItemStatus(e.CommandName, txtRejectReason.Text, e.CommandArgument.ToString());
                //this.bindStatus();
                this.bindGridView();
            }
        }

        protected void gvReorderList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReorderItemList.PageIndex = e.NewPageIndex;
            bindGridView();
        }
        #endregion

        #region HelperMethods
        public void bindGridView()
        {
            gvReorderItemList.DataSource = appRejectFaca.searchReorderItem(this.ddlSupplierName.SelectedValue, "Pending");
            gvReorderItemList.DataBind();
            if (gvReorderItemList.Rows.Count == 0)
            {
                this.Message("There is no data!");
            }
        }

        public void ddlSupplier_Bind()
        {
            ddlSupplierName.DataValueField = "supplierid";
            ddlSupplierName.DataTextField = "name";
            ddlSupplierName.DataSource = appRejectFaca.getSupplierDataList();
            ddlSupplierName.DataBind();
            //insert all to list
            ListItem lstAll = new ListItem("All", "All");
            ddlSupplierName.Items.Insert(0, lstAll);
        }

        //public void bindStatus()
        //{
        //    rdbStatus.DataValueField = "Status";
        //    rdbStatus.DataTextField = "Status";
        //    rdbStatus.DataSource = appRejectFaca.getStatusDataList();
        //    rdbStatus.DataBind();
        //    //add default item to show
        //    ListItem lstAll = new ListItem("All", "All");
        //    lstAll.Selected = true;
        //    rdbStatus.Items.Insert(0, lstAll);
        //}




        //Show message
        public void Message(string msg)
        {
            dvAlert.Visible = true;
            lblErrorMessage.Text = msg;
        }
        #endregion
    }
}