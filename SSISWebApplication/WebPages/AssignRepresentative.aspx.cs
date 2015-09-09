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
    public partial class AssignRepresentative : System.Web.UI.Page
    {
        AssignRepresentativeController assignRepresentativeController;
        string repUserId;
        List<User> userCollection;
        User userEntity;

        protected void Page_Load(object sender, EventArgs e)
        {
            userEntity = (User)Session["UserEntity"];

            assignRepresentativeController = new AssignRepresentativeController();

           //userCollection = assignRepresentativeController.loadController("CPSC");//TODO

            dataBind();
        }

        protected void SearchTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void EmployeeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells[2].Text.ToString() == "departmentRepresentative")
            {
                e.Row.ForeColor = Color.Blue;
                Button b = (Button)e.Row.Cells[3].Controls[0];
                b.Text = "Assigned";
                b.Enabled = false;
            }

            e.Row.Cells[2].Visible = false;
        }

        protected void EmployeeGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = EmployeeGridView.Rows[Convert.ToInt32(e.CommandArgument)];

                foreach(GridViewRow r in EmployeeGridView.Rows)
                {
                    if(r.Cells[2].Text.ToString() == "departmentRepresentative")
                    {
                        assignRepresentativeController.selectEmployee(r.Cells[0].Text.ToString(),row.Cells[0].Text,userEntity.DepartmentName);
                        break;
                    }
                }

                
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void EmployeeGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EmployeeGridView.PageIndex = e.NewPageIndex;
            dataBind();
        }


        private void dataBind()
        {
            EmployeeGridView.DataSource = assignRepresentativeController.loadController(userEntity.DepartmentId);

            EmployeeGridView.DataBind();
        }
    }
}