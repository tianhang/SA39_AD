using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Timers;
using ClassLibrary.Controllers;
using ClassLibrary.EntityFacade;
using ClassLibrary.Helper;


namespace SSISWebApplication.WebPages.Discrepancy
{
    public partial class DiscrepancyPage : System.Web.UI.Page
    {
        SubmitDiscrepancyController sdc = new SubmitDiscrepancyController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dataBind();
            }

            if (DDListSupplier.Text == "")
            {
                DDListSupplier.DataSource = sdc.getSupplier();
                DDListSupplier.DataBind();
            }
            if (DDListDescription.Text == "")
            {
                DDListDescription.DataSource = sdc.getDescriptions();
                DDListDescription.DataBind();
            }
            dvAlert.Visible = false;

        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string itemName = DDListDescription.Text;
            //string category = txtCategory.Text; 
            string category = labCategory.Text;
            int qty = Convert.ToInt32(txtQuantity.Text);
            string supplier = DDListSupplier.Text;
            float price = sdc.getPrice(itemName, supplier);
            string reason = txtReason.Text;
            //DateTime submitDate = System.DateTime.Now;
            //sdc.addDiscrepancyItem(itemName,catalogue,qty,supplier,price,reason,submitDate);
            sdc.addDiscrepancyItem(itemName, category, qty, supplier, price, reason);
            clearAll();
            dataBind();
        }

        private void clearAll()
        {
            //txtItemName.Text = "";
            labCategory.Text = "";
            txtQuantity.Text = "";
            labPrice.Text = "";
            txtReason.Text = "";
            labTotalPrice.Text = "";
        }

        protected void txtItemName_TextChanged(object sender, EventArgs e)
        {
            //txtCategory.Text = sdc.getCategory(txtItemName.Text);
            labCategory.Text = sdc.getCategory(DDListDescription.Text);
        }

        protected void DDListSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDListDescription.Text != "")
            {
                float price = sdc.getPrice(DDListDescription.Text, DDListSupplier.Text);
                labPrice.Text = price + "";
                if (txtQuantity.Text != "")
                    labTotalPrice.Text = (price * Convert.ToInt32(txtQuantity.Text)) + "";
                else
                {
                    labTotalPrice.Text = "0";
                    txtQuantity.Attributes["placeholder"] = "Quantity can not be null!";
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (gridviewDiscrepancy.Rows.Count > 0)
            {
                List<string> idList = new List<string>();
                foreach (GridViewRow row in gridviewDiscrepancy.Rows)
                {
                    CheckBox chkRow = (row.Cells[0].Controls[1] as CheckBox);
                    if (chkRow.Checked)
                    {
                        string id = row.Cells[1].Text;
                        idList.Add(id);
                    }
                }
                sdc.updateStatus(idList);
                dataBind();
            }
            else
            {
                this.Message("You should have discrepancy list to submit!");
            }

        }

        protected void gridviewDiscrepancy_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //GridViewRow row = (GridViewRow)gridviewDiscrepancy.Rows[e.RowIndex];
            sdc.deleteDiscrepancy(gridviewDiscrepancy.Rows[e.RowIndex].Cells[1].Text);
            dataBind();
        }

        protected void gridviewDiscrepancy_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridviewDiscrepancy.EditIndex = e.NewEditIndex;
            dataBind();
        }

        protected void gridviewDiscrepancy_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridviewDiscrepancy.EditIndex = -1;
            dataBind();
        }

        protected void gridviewDiscrepancy_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridviewDiscrepancy.Rows[e.RowIndex];
            string qty = ((TextBox)row.Cells[4].FindControl("TextBox1")).Text;
            string reason = ((TextBox)row.Cells[6].Controls[1]).Text;
            string id = gridviewDiscrepancy.Rows[e.RowIndex].Cells[1].Text;
            sdc.updateDiscrepancy(Convert.ToInt32(qty), reason, id);
            gridviewDiscrepancy.EditIndex = -1;
            dataBind();
        }
        public void dataBind()
        {
            gridviewDiscrepancy.DataSource = sdc.getGridViewSource();
            gridviewDiscrepancy.DataBind();
        }

        protected void DDListDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            labCategory.Text = sdc.getCategory(DDListDescription.Text);
        }



        //Show message
        public void Message(string msg)
        {
            dvAlert.Visible = true;
            lblErrorMessage.Text = msg;
        }

    }
}