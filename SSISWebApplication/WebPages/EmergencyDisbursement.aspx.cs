using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.Controllers;
using ClassLibrary.Entities;
using System.Data;

namespace SSISWebApplication.WebPages
{
    public partial class EmergencyDisbursement : System.Web.UI.Page
    {
        SubmitRequisitionController sRController = new SubmitRequisitionController();
        EmergencyDisbursementController eDController = new EmergencyDisbursementController();

        protected void Page_Load(object sender, EventArgs e)
        {

            /*if (Session["UserId"] != null)
            {
                userId = Convert.ToString(Session["UserId"]);
            }*/

            //departmentId = sRController.getDepartmentId(userId);


            //lblmsg.Text = "";
            dvAlert.Visible = false;
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
            ddlDepartmentDataBind();                                                         //add for dep dropdownllist
        }

        protected void ServerValidation(object source, ServerValidateEventArgs args)            // add for validation of textBox
        {

            try
            {
                args.IsValid = false;
                int i = int.Parse(args.Value);
                if (ddlItem.SelectedValue != "-Select One-")
                {
                    string item = ddlItem.SelectedValue;
                    System.Diagnostics.Debug.Print("itemName is " + item);
                    int bal = Convert.ToInt32(eDController.getItem(item).stockBalance);
                    if (i > 0 && i <= bal)
                    {
                        args.IsValid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                args.IsValid = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)                         //lillte change for valide
        {
            if (ddlItem.SelectedIndex != 0 && ddlDepartment.SelectedIndex != 0 && ddlCategory.SelectedIndex != 0)
            {
                if (Page.IsValid)
                {
                    //lblNoti.Text = string.Empty;
                    lblRequisitionItemList.Visible = true;
                    lblRequisitionItemList.Text = "Disbursement Item List";
                    dgvRequisitionItemList.Visible = true;
                    List<DisbursementDetails> DBList = new List<DisbursementDetails>();
                    bool flag = false;
                    if (Session["DBList"] != null)
                    {
                        DBList = (List<DisbursementDetails>)Session["DBList"];
                        foreach (DisbursementDetails db in DBList)
                        {
                            if (db.ItemId == ddlItem.SelectedValue)
                            {
                                //lblmsg.Text = "Cannot add the same item twice.";
                                this.Message("Cannot add the same item twice.");
                                flag = true;
                                return;
                            }
                        }
                    }
                    if (!flag)
                    {
                        DisbursementDetails dd = new DisbursementDetails();
                        dd.ItemId = ddlItem.SelectedValue;
                        dd.ItemName = eDController.getItem(ddlItem.SelectedValue).description;
                        dd.RequestedQty = Convert.ToInt32(txtQuantity.Text);
                        dd.DeliveredQty = dd.RequestedQty;
                        DBList.Add(dd);
                        Session["DBList"] = DBList;
                    }
                    btnSubmit.Visible = true;

                    dgvRequisitionItemList.DataSource = DBList;
                    dgvRequisitionItemList.DataBind();

                }

                else
                {
                    string itemId = ddlItem.SelectedValue;
                    int bal = Convert.ToInt32(eDController.getItem(itemId).stockBalance);
                    string itemName = eDController.getItem(itemId).description;
                    string txtmessage = "The are only " + bal + " for item-" + itemName;
                    this.Message(txtmessage);
                }
            }
            else
            {
                //lblmsg.Text = "Please select one item and department.";
                this.Message("Please select one item and department.");
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

        public void ddlDepartmentDataBind()
        {
            ddlDepartment.DataSource = eDController.acceptDepartmentList();
            ddlDepartment.DataBind();
            //add select one list 
            ListItem lstSelectOne = new ListItem("-Select One-", null);
            ddlDepartment.Items.Insert(0, lstSelectOne);
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
            if (Session["DBList"] != null)
            {
                List<DisbursementDetails> DBList = (List<DisbursementDetails>)Session["DBList"];
                DisbursementDetails r = DBList[e.RowIndex];
                DBList.Remove(r);
                dgvRequisitionItemList.DataSource = DBList;
                dgvRequisitionItemList.DataBind();
            }

            if (dgvRequisitionItemList.Rows.Count == 0)
            {
                lblRequisitionItemList.Visible = false;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (dgvRequisitionItemList.Rows.Count > 0)
            {
                string depName = ddlDepartment.SelectedValue;
                List<DisbursementDetails> DBList = (List<DisbursementDetails>)Session["DBList"];
                eDController.createEmergencyDisbursement(DBList, depName);
                txtQuantity.Text = string.Empty;
                //lblNoti.Text = "Your disbursement is submitted.";
                this.Message("Your disbursement is submitted.");
                dgvRequisitionItemList.DataSource = null;
                dgvRequisitionItemList.DataBind();
            }
            else
            {
                //lblNoti.Text = "Your disbursement should have items.";
                this.Message("Your disbursement should have items.");
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