using ClassLibrary.Controllers;
using ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISWebApplication.WebPages
{
    public partial class ViewDisbursement : System.Web.UI.Page
    {
        ViewDisbursementController viewDisbursementController;
        List<Disbursement> disbursementCollection;
        List<Department> departmentCollection;
        Disbursement disbursement;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            viewDisbursementController = new ViewDisbursementController();
            dvAlert.Visible = false;
            if(!IsPostBack)
            {
                try
                {
                    int option = Convert.ToInt32(Request.QueryString["caller"]);
                    if(option == 2)
                    {
                        //ErrorMessageLabel.Text = "There are outstanding disbursements. Please complete them before generating new disbursement.";
                        //ErrorMessageLabel.Visible = true;   
                        this.Message("There are outstanding disbursements. Please complete them before generating new disbursement.");
                    }
                    //else
                    //{
                    //    ErrorMessageLabel.Visible = false;
                    //}
                }
                catch (Exception)
                {
                }
                

                departmentCollection = viewDisbursementController.loadController();
                var de = departmentCollection.Select(d => new { d.DepartmentName }).ToList();
                List<string> depNameString = new List<string>();
                foreach(var d in de)
                {
                    depNameString.Add(d.DepartmentName);
                }
                
                Session["departmentCollection"] = departmentCollection;
                DepartmentDropDownList.DataSource = depNameString.ToList();
                DepartmentDropDownList.DataBind();
                DisbursementPanel.Visible = false;
                DisbursementDetailsPanel.Visible = false;
                CompleteDeliveryPanel.Visible = false;
                //ErrorMessageLabel.Visible = false;
            }
            else
            {
                departmentCollection = (List<Department>)Session["departmentCollection"];
            }
            
        }

        protected void Generatebtn_Click(object sender, EventArgs e)
        {
            Department department = departmentCollection.Find(d => d.DepartmentName == DepartmentDropDownList.SelectedValue);
            if(PendingRadioButton.Checked==true)
            {
                DisbursementPanel.Visible = false;
                disbursement = viewDisbursementController.selectPendingDisbursement(department.DepartmentId);
                if (disbursement != null)
                {
                    Session["disbursement"] = disbursement;
                    CompleteDetailsGridView.DataSource = disbursement.DisbursementDetailsCollection;
                    CompleteDetailsGridView.DataBind();
                    CompleteDetailsGridView.ForeColor = Color.Black;
                    CollectionPointLabel1.Text = disbursement.CollectionPointName;
                    RepresentativeLabel1.Text = disbursement.RepresentativeName;
                    DepartmentLabel1.Text = disbursement.DepartmentName;
                    DateLabel1.Text = disbursement.Date.ToString();
                    CompleteDeliveryPanel.Visible = true;
                    DisbursementDetailsPanel.Visible = false;
                    DisbursementPanel.Visible = false;
                    //ErrorMessageLabel.Visible = false;
                }
                else
                {
                    CompleteDeliveryPanel.Visible = false;
                    //ErrorMessageLabel.Text = "No records found.";
                    //ErrorMessageLabel.Visible = true;
                    this.Message("No records found.");
                }
                
            }
            else
            {
                disbursementCollection = viewDisbursementController.selectDepartment(department.DepartmentId);
                DisbursementPanel.Visible = true;
                DisbursementDetailsPanel.Visible = false;
                CompleteDeliveryPanel.Visible = false;
                if (disbursementCollection.Count != 0)
                {   DisbursementGridView.DataSource = disbursementCollection;
                    DisbursementGridView.DataBind();
                    CompleteDeliveryPanel.Visible = false;
                    DisbursementDetailsPanel.Visible = false;
                    DisbursementPanel.Visible = true;
                    //ErrorMessageLabel.Visible = false;
                }
                else
                {
                    DisbursementPanel.Visible = false;
                    //ErrorMessageLabel.Text = "No records found.";
                    //ErrorMessageLabel.Visible = true;
                    this.Message("No records found.");
                }
            }
        }

        protected void DisbursementGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="SelectDetails")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = DisbursementGridView.Rows[index];

                string disbursementId = row.Cells[1].Text;

                DisbursementDetailsGridView.DataSource = viewDisbursementController.selectDisbursementDetails(disbursementId);
                DisbursementDetailsGridView.DataBind();
                CollectionPointLabel.Text = row.Cells[7].Text;
                RepresentativeLabel.Text = row.Cells[8].Text;
                DepartmentLabel.Text = row.Cells[3].Text;
                DateLabel.Text = row.Cells[4].Text;
                DelDateLabel.Text = row.Cells[5].Text;
                CompleteDeliveryPanel.Visible = false;
                DisbursementDetailsPanel.Visible = true;
                //ErrorMessageLabel.Visible = false;
            }
        }

        protected void DisbursementGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[6].Visible = false;
        }

        protected void CompleteDetailsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }

        protected void SelectAllButton_Click(object sender, EventArgs e)
        {
            foreach(GridViewRow row in CompleteDetailsGridView.Rows)
            {
                TextBox t = (TextBox)row.FindControl("DelQtyTextBox");
                t.Text = row.Cells[2].Text;
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            int flag = 0;
            foreach (GridViewRow row in CompleteDetailsGridView.Rows)
            {
                TextBox t = (TextBox)row.FindControl("DelQtyTextBox");
                int num;
                if (!(int.TryParse(t.Text.ToString(), out num)))
                {
                    row.ForeColor = Color.Red;
                    flag = 1;
                }
                else if (Convert.ToInt32(t.Text) > Convert.ToInt32(row.Cells[2]) || Convert.ToInt32(t.Text)<0)
                {
                    flag = 1;
                }
            }
            if (flag == 1)
            {
                //ErrorMessageLabel.Text = "Please note you are to enter the quantity in numbers between 0 and the requestedQty";
                //ErrorMessageLabel.Visible = true;
                this.Message("Please note you are to enter the quantity in numbers between 0 and the requestedQty");
            }
            else
            {
                Disbursement disbursemnt = (Disbursement)Session["disbursement"];

                List<DisbursementDetails> disbursementDetailsCollection = disbursemnt.DisbursementDetailsCollection;

                foreach (GridViewRow row in CompleteDetailsGridView.Rows)
                {
                    DisbursementDetails disbursementDetails = disbursementDetailsCollection.Find(d => d.ItemId == row.Cells[0].Text.ToString());
                    TextBox t = (TextBox)row.FindControl("DelQtyTextBox");
                    disbursementDetails.DeliveredQty = Convert.ToInt32(t.Text);
                }

                viewDisbursementController.selectCompleteDelivery(disbursemnt);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in CompleteDetailsGridView.Rows)
            {
                TextBox t = (TextBox)row.FindControl("DelQtyTextBox");
                t.Text = "";
            }
            //ErrorMessageLabel.Visible = false;
        }

        //Show message
        public void Message(string msg)
        {
            dvAlert.Visible = true;
            lblErrorMessage.Text = msg;
        }
    }
}