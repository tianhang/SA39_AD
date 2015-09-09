using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.Controllers;
using ClassLibrary.Entities;

namespace SSISWebApplication.WebPages
{
    public partial class ViewRequisition : System.Web.UI.Page
    {
        User userEntity;
        ViewRequisitionController controller = new ViewRequisitionController();
        string userRole = "departmentEmployee";
        string userId = "u1001";
        protected void Page_Load(object sender, EventArgs e)
        {

            userEntity = (User)Session["UserEntity"];
            userId = userEntity.UserID;
            userRole = userEntity.RoleName;
            //departmentId = userEntity.DepartmentId;

            //string userRole = Convert.ToString(Session["userRole"]);
            if(!IsPostBack)
            {
                List<ListItem> list = new List<ListItem>();
                
                if (userRole == "storeClerk")
                {
                    DepartmentDropDownList.DataSource = controller.acceptDepartmentList();
                    DepartmentDropDownList.DataBind();

                    list.Add(new ListItem("Approved"));
                    list.Add(new ListItem("In Progress"));
                    list.Add(new ListItem("Outstanding"));  
                }
                else
                {
                    DepartmentDropDownList.Visible = false;
                    DepLab.Visible = false;

                    list.Add(new ListItem("Approved"));
                    list.Add(new ListItem("Rejected"));
                    list.Add(new ListItem("Cancelled"));
                    list.Add(new ListItem("In Progress"));
                    list.Add(new ListItem("Outstanding"));
                    list.Add(new ListItem("Completed"));
                }

                //StatusDropDownList.DataSource = list;
                //StatusDropDownList.DataBind();
                StatusRadioButtonList.DataSource = list;
                StatusRadioButtonList.DataBind();
                
            }
        }
        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            
            //string status = StatusDropDownList.SelectedValue;
            string status = StatusRadioButtonList.SelectedValue;
            if (userRole == "storeClerk")
            {
                string dep = DepartmentDropDownList.SelectedValue;
                string depId = controller.findDepartment(dep).departmentId;
                RequisitionGridView.DataSource = controller.selectDepartmentRequisition(depId, status);
            }
            else
            {
                if(userRole == "departmentEmployee")
                {
                    RequisitionGridView.DataSource = controller.selectUserRequisition(userId, status);
                }
                else
                {
                    string depId = controller.findUser(userId).departmentId;
                    RequisitionGridView.DataSource = controller.selectDepartmentRequisition(depId, status);
                }
            }

            RequisitionGridView.DataBind();
            DetailGridView.Visible = false;
        }

        protected void RequisitionGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RequisitionGridView.SelectedRow != null)
            {
                GridViewRow row = (GridViewRow)RequisitionGridView.SelectedRow;
                string reqId = row.Cells[0].Text;
                DetailGridView.DataSource = controller.selectRequisition(reqId);
                DetailGridView.DataBind();
                DetailGridView.Visible = true;
            }
        }

        protected void RequisitionGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //string status = StatusDropDownList.SelectedValue;
            string status = StatusRadioButtonList.SelectedValue;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button b = (Button)e.Row.FindControl("CancelBtn");
                if(userRole == "departmentEmployee" && (status == "Outstanding" || status == "Approved"))
                {
                    for (int i = 0; i < RequisitionGridView.Rows.Count; i++)
                    {
                        RequisitionGridView.Rows[i].Cells[4].Visible = true;
                    }
                }

                if (status == "Rejected")
                {
                    for (int i = 0; i < RequisitionGridView.Rows.Count; i++)
                    {
                        RequisitionGridView.Rows[i].Cells[2].Visible = true;
                    }
                }
            }
        }

        protected void RequisitionGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cancelRequisition")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = RequisitionGridView.Rows[index];
                string reqId = row.Cells[0].Text;
                controller.selectCancelRequistion(reqId);
                //string status = StatusDropDownList.SelectedValue;
                string status = StatusRadioButtonList.SelectedValue;
                RequisitionGridView.DataSource = controller.selectUserRequisition(userId, status);
                RequisitionGridView.DataBind();
            }
        }
    }
}