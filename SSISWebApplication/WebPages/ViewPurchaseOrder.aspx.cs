using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.EntityFacade;

namespace SSISWebApplication.WebPages
{
    public partial class ViewPurchaseOrder : System.Web.UI.Page
    {
        ViewPurchaseOrderFacade poFaca = new ViewPurchaseOrderFacade();

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                this.bindSupplier();
                this.bindStatus();
                this.bindAllPurchaseOrder();
            }
        }
        protected void gvPurchaseOrderList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                Response.Redirect("~/WebPages/ViewPurchaseOrderDetail.aspx?poid=" + e.CommandArgument);
            }
            else if (e.CommandName == "Receive")
            {
                poFaca.updatePurchaseOrderStatus("Delivered", e.CommandArgument.ToString());
                poFaca.updateStock(poFaca.getPurchaseOrderDetailById(e.CommandArgument.ToString()));
                this.bindStatus();
                this.bindAllPurchaseOrder();
            }
        }

        protected void butSearch_Click(object sender, EventArgs e)
        {
            this.bindAllPurchaseOrder();
        }

        protected void butCancel_Click(object sender, EventArgs e)
        {
            this.ddlSupplierName.SelectedIndex = 0;
            this.rdbStatus.SelectedIndex = 0;
            this.bindAllPurchaseOrder();
        }

        protected void gvPurchaseOrderList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPurchaseOrderList.PageIndex = e.NewPageIndex;
            this.bindAllPurchaseOrder();
        }
        #endregion

        #region HelperMethods

        public void bindAllPurchaseOrder()
        {
            gvPurchaseOrderList.DataSource = poFaca.searchPurchaseOrder(this.ddlSupplierName.SelectedValue, this.rdbStatus.SelectedValue);
            gvPurchaseOrderList.DataBind();
        }

        public void bindSupplier()
        {
            ddlSupplierName.DataValueField = "supplierid";
            ddlSupplierName.DataTextField = "name";
            ddlSupplierName.DataSource = poFaca.getSupplierData();
            ddlSupplierName.DataBind();
            //add default item to show
            ListItem lstAll = new ListItem("All", "All");
            ddlSupplierName.Items.Insert(0, lstAll);
        }

        public void bindStatus()
        {
            rdbStatus.DataValueField = "Status";
            rdbStatus.DataTextField = "Status";
            rdbStatus.DataSource = poFaca.getStatusDataList();
            rdbStatus.DataBind();
            //add default item to show
            ListItem lstAll = new ListItem("All", "All");
            lstAll.Selected = true;
            rdbStatus.Items.Insert(0, lstAll);
        }
        #endregion

    }
}