using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.Controllers;
using ClassLibrary.EntityFacade;
using ClassLibrary.Helper;

namespace SSISWebApplication.WebPages.Discrepancy
{
    public partial class ApproveDiscrepancyRequest : System.Web.UI.Page
    {
        ApproveRejectDiscrepancyController ardc = new ApproveRejectDiscrepancyController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            List<string> idList = new List<string>();
            foreach (GridViewRow row in gridviewApproveDiscrepancy.Rows)
            {
                CheckBox chkRow = (row.Cells[0].Controls[1] as CheckBox);
                if (chkRow.Checked)
                {
                    string id = row.Cells[1].Text;
                    idList.Add(id);
                    string reason = ((TextBox)row.Cells[7].Controls[1]).Text;
                    ardc.updateRejectReason(id, reason);
                }
            }
            ardc.updateStatus(idList,"Approved");
            dataBind();
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            List<string> idList = new List<string>();
            foreach (GridViewRow row in gridviewApproveDiscrepancy.Rows)
            {
                CheckBox chkRow = (row.Cells[0].Controls[1] as CheckBox);
                if (chkRow.Checked)
                {
                    string id = row.Cells[1].Text;
                    idList.Add(id);
                    string reason = ((TextBox)row.Cells[7].Controls[1]).Text;
                    ardc.updateRejectReason(id, reason);
                }
            }

            ardc.updateStatus(idList, "Rejected");
            dataBind();
        }

        protected void gridviewApproveDiscrepancy_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridviewApproveDiscrepancy.Rows[e.RowIndex];
            string reason = ((TextBox)row.Cells[7].Controls[1]).Text;
            string id = gridviewApproveDiscrepancy.Rows[e.RowIndex].Cells[1].Text;
            ardc.updateRejectReason(id,reason);
            gridviewApproveDiscrepancy.EditIndex = -1;
            dataBind();
        }

        protected void gridviewApproveDiscrepancy_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridviewApproveDiscrepancy.EditIndex = e.NewEditIndex;
            dataBind();
        }

        protected void gridviewApproveDiscrepancy_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridviewApproveDiscrepancy.EditIndex = -1;
            dataBind();
        }

        private void dataBind()
        {
            gridviewApproveDiscrepancy.DataSource = ardc.getSubmittedGridViewSource();
            gridviewApproveDiscrepancy.DataBind();
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridviewApproveDiscrepancy.Rows)
            {
                CheckBox chkRow = (row.Cells[0].Controls[1] as CheckBox);
                chkRow.Checked = true;
            }
        }

        protected void btnDeselect_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridviewApproveDiscrepancy.Rows)
            {
                CheckBox chkRow = (row.Cells[0].Controls[1] as CheckBox);
                chkRow.Checked = false;
            }
        }
    }
}