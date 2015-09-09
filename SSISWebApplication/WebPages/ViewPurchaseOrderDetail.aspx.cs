using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.EntityFacade;
using ClassLibrary.Entities;
using System.Data;

namespace SSISWebApplication.WebPages
{
    public partial class ViewPurchaseOrderDetail : System.Web.UI.Page
    {
        ViewPurchaseOrderFacade poFaca = new ViewPurchaseOrderFacade();
        ClassLibrary.Entities.PurchaseOrder poEntity;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.bindStatus();
                this.fillPurchaseOrder();
            }
            this.bindDataGridView();

        }
        protected void gvPurchaseOrderList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPurchaseOrderList.PageIndex = e.NewPageIndex;
            gvPurchaseOrderList.DataBind();
        }

        protected void btnDeliveryReceive_Click(object sender, EventArgs e)
        {
            if (rdbStatus.SelectedValue != "Delivered")
            {
                poFaca.updatePurchaseOrderStatus("Delivered", this.txtPurchaseOrderId.Text);
                poFaca.updateStock((DataTable)Session["dtPODetail"]);
                this.bindStatus();
                this.fillPurchaseOrder();
            }
            else
            {
                Message("This purchase order is already received!!");
            }
        }


        #region HelperMethods
        //Show message
        public void Message(string msg)
        {
            dvAlert.Visible = true;
            lblErrorMessage.Text = msg;
        }

        public void bindDataGridView()
        {
            Session["dtPODetail"] = poFaca.getPurchaseOrderDetailById(Request.QueryString["poid"].ToString());
            gvPurchaseOrderList.DataSource = (DataTable)Session["dtPODetail"];
            gvPurchaseOrderList.DataBind();
        }

        public void fillPurchaseOrder()
        {
            poEntity = new ClassLibrary.Entities.PurchaseOrder();
            poEntity = poFaca.getPurchaseOrderDataById(Request.QueryString["poid"].ToString());
            this.txtPurchaseOrderId.Text = poEntity.PurchaseOrderId;
            this.txtSupplierName.Text = poEntity.SupplierName;
            this.txtOrderDate.Text = poEntity.OrderDate.ToShortDateString();
            this.rdbStatus.SelectedValue = poEntity.Status;
        }

        public void bindStatus()
        {
            rdbStatus.DataValueField = "Status";
            rdbStatus.DataTextField = "Status";
            rdbStatus.DataSource = poFaca.getStatusDataList();
            rdbStatus.DataBind();
        }
        #endregion
    }
}