using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.Controllers;
using ClassLibrary.Entities;

namespace SSISWebApplication.WebPages
{
    public partial class ApproveRejectRequisition : System.Web.UI.Page
    {
        ApproveRejectRequisitionController arRequisitionController = new ApproveRejectRequisitionController();
        User userEntity;
        string requisitionId = "", userId = "u1005", departmentId = "";
        int reqIndex = 0;
        DataTable dt;
        DataRow dr;

        protected void Page_Load(object sender, EventArgs e)
        {
            userEntity = (User)Session["UserEntity"];
            userId = userEntity.UserID;
            departmentId = userEntity.DepartmentId;


            if(Session["UserId"]!=null)
            {
                userId = Convert.ToString(Session["UserId"]);
            }
            departmentId = arRequisitionController.getDepartmentId(userId);
            if(!IsPostBack)
            {
                dgvRequisitionListDataBind();
            } 
        }

        public void dgvRequisitionListDataBind()
        {
            dgvRequisitionList.DataSource = arRequisitionController.getRequisition_forApproveReject(departmentId);
            dgvRequisitionList.DataBind();
        }

        protected void dgvRequisitionList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowDetails")
            {
                reqIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow r = (GridViewRow)dgvRequisitionList.Rows[reqIndex];
                requisitionId = r.Cells[0].Text;
            }

            //System.Diagnostics.Debug.Print(requisitionId);
            //string name = arRequisitionController.selectRequisitionDetail(requisitionId)[0].ItemName;
            //System.Diagnostics.Debug.Print(name);

            List<RequisitionDetails> list2 = arRequisitionController.selectRequisitionDetail(requisitionId);

            foreach (RequisitionDetails details in list2)
            {
                dt=detailsDataTable();
                dr = dt.NewRow();

                //dr["itemId"] = details.ItemId;
                dr["description"] = details.ItemName;
                dr["requestedQty"] = details.RequestedQty;

                dt.Rows.Add(dr);
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        public DataTable detailsDataTable()
        {
            dt = null;
            
            dt = new DataTable();
            //dt.Columns.Add("itemId");
            dt.Columns.Add("description");
            dt.Columns.Add("requestedQty");

            return dt;
        }

        protected void btnApproveReject_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            //NamingContainer to get the row of the particular button click
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            if(row!=null)
            {
                requisitionId = row.Cells[0].Text;
            }

            TextBox rejectReason = (TextBox)row.FindControl("txtRejectReason");

            if(btn.Text=="Approve")
            {
                arRequisitionController.updateRequisitionStatus(requisitionId, "Approved",rejectReason.Text);
            }

            else
            {
                arRequisitionController.updateRequisitionStatus(requisitionId, "Rejected", rejectReason.Text);
            }

            Response.Redirect(Request.RawUrl);
        }
    }
}