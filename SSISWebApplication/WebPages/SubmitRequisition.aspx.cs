using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.Controllers;
using ClassLibrary.Helper;
using ClassLibrary.Entities;
using System.Text;

namespace SSISWebApplication.WebPages
{
    public partial class SubmitRequisition : System.Web.UI.Page
    {
        SubmitRequisitionController sRController = new SubmitRequisitionController();
        UOM uomEntity;
        User userEntity;
        string requisitionId, userId, quantity, departmentId;
        DateTime date;
        DataTable dt;
        DataRow dr;

        protected void Page_Load(object sender, EventArgs e)
        {
            userEntity = (User)Session["UserEntity"];
            userId = userEntity.UserID;
            departmentId = userEntity.DepartmentId;
            /*if (Session["UserId"] != null)
            {
                userId = Convert.ToString(Session["UserId"]);
            }*/

            departmentId = sRController.getDepartmentId(userId);

            date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            lblRequisitionDate.Text = date.ToString();

            if (!this.IsPostBack)
            {
                setAsDefault();
            }

        }

        public void setAsDefault()
        {
            lblRequisitionItemList.Visible = false;
            dgvRequisitionItemList.Visible = false;
            btnSubmit.Visible = false;

            ddlCategoryDataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.ddlCategory.SelectedIndex != 0 & this.ddlItem.SelectedIndex != 0)
            {
                //lblNoti.Text = string.Empty;
                lblRequisitionItemList.Visible = true;
                lblRequisitionItemList.Text = "Requisition Item List";

                dgvRequisitionItemList.Visible = true;

                if (dgvRequisitionItemList.Rows.Count == 0)
                {
                    dgvRequisitionItemListDataBind(requisitionItemDataTable());
                }
                else
                {
                    dt = requisitionItemDataTable();

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["itemId"].ToString() == ddlItem.SelectedValue)
                        {
                            //lblmsg.Text = "Cannot add the same item twice.";
                            this.Message("Cannot add the same item twice.");
                            return;//out of loop if same item found
                        }
                    }
                    this.dgvRequisitionItemListDataBind(dt);
                }

                btnSubmit.Visible = true;

                ViewState["RequisitionItem"] = dt;
                dgvRequisitionItemList.DataSource = dt;
                dgvRequisitionItemList.DataBind();
                dvAlert.Visible = false;
            }
            else
            {
                //this.lblmsg.Text = "Select Everything to add!";
                this.Message("Select Everything to add!");
            }
        }


        public void dgvRequisitionItemListDataBind(DataTable dt)
        {
            dr = dt.NewRow();
            quantity = txtQuantity.Text;

            dr["requisitionId"] = requisitionId;
            dr["itemId"] = ddlItem.SelectedValue;
            dr["categoryId"] = ddlCategory.SelectedValue;
            dr["categoryName"] = ddlCategory.SelectedItem.Text;
            uomEntity = new UOM();
            uomEntity = sRController.getUOMName(ddlItem.SelectedValue);
            dr["uomId"] = uomEntity.UomId;
            dr["uomName"] = uomEntity.Uom;
            dr["description"] = ddlItem.SelectedItem.Text;
            dr["quantity"] = quantity;
            dt.Rows.Add(dr);
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (dgvRequisitionItemList.Rows.Count > 0)
            {
                dt = requisitionItemDataTable();
                //CodeGeneratorHelper.returnCode("S", sRController.getCodeGeneratorValue_PyaePyae("S"));
                sRController.createRequisition(requisitionId, date, userId, departmentId, null, "Pending", dt);
                //lblNoti.Text = "Your requisition is submitted.";
                this.Message("Your requisition is submitted.");
                setAsDefault();
                ddlItemDataBind(ddlCategory.SelectedValue);
                ViewState["RequisitionItem"] = null;
                txtQuantity.Text = string.Empty;
            }
            else
            {
                //lblNoti.Text = "You requisition need to add items";
                this.Message("You requisition need to add items");
            }
           
        }

        public void ddlCategoryDataBind()
        {
            ddlCategory.DataTextField = "name";
            ddlCategory.DataValueField = "categoryId";
            ddlCategory.DataSource = sRController.getAllCategory();
            ddlCategory.DataBind();
            //add select one list 
            ListItem lstSelectOne = new ListItem("-Select One-", null);
            ddlCategory.Items.Insert(0, lstSelectOne);
        }

        public DataTable requisitionItemDataTable()
        {
            dt = null;
            if (ViewState["RequisitionItem"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("requisitionId");
                dt.Columns.Add("itemId");
                dt.Columns.Add("categoryName");
                dt.Columns.Add("categoryId");
                dt.Columns.Add("uomName");
                dt.Columns.Add("uomId");
                dt.Columns.Add("description");
                dt.Columns.Add("quantity");

            }
            else
            {
                dt = (DataTable)ViewState["RequisitionItem"];
            }
            return dt;
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlCategory.SelectedIndex != 0)
            {
                ddlItemDataBind(ddlCategory.SelectedValue);
            }
        }

        public void ddlItemDataBind(string category)
        {
            ddlItem.DataTextField = "description";
            ddlItem.DataValueField = "itemId";
            ddlItem.DataSource = sRController.getItems(category);
            ddlItem.DataBind();
            //insert select one list
            ListItem lstSelectOne = new ListItem("-Select One-", null);
            ddlItem.Items.Insert(0, lstSelectOne);
        }

        protected void dgvRequisitionItemList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int row = e.RowIndex;
            DataTable dt = (DataTable)ViewState["RequisitionItem"];
            DataRow dr = dt.Rows[row];
            dt.Rows.Remove(dr);
            
            ViewState["RequisitionItem"] = dt;
            dgvRequisitionItemList.DataSource = dt;
            dgvRequisitionItemList.DataBind();

            if (dgvRequisitionItemList.Rows.Count == 0)
            {
                lblRequisitionItemList.Visible = false;
            }
        }

        //Show message
        public void Message(string msg)
        {
            dvAlert.Visible = true;
            lblErrorMessage.Text = msg;
        }

    }
}