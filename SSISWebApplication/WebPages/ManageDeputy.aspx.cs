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
    public partial class ManageDeputy : System.Web.UI.Page
    {
        ManageDeputyController manageDeputyController;
        User userEntity;

        protected void Page_Load(object sender, EventArgs e)
        {
            manageDeputyController = new ManageDeputyController();

            userEntity = (User)Session["UserEntity"];

            if(!IsPostBack)
            {
                dataBind();
                //List<User> userCollection = manageDeputyController.loadController("CPSC");//TODO
                //if(userCollection.Count!=0)
                //{
                //    User user = userCollection[0];
                //    UIDLabel.Text = user.UserID;
                //    NameLabel.Text = user.UserName;
                //    Session["startDate"] = user.StartDate;
                //    StartDateLabel.Text = "Start Date : "+user.StartDate.Date.ToString("d");
                //    EndDateLabel.Text = "End Date : "+user.EndDate.Date.ToString("d");
                //    Session["EndDate"] = user.EndDate;
                //    Panel1.Visible = true;
                //    EmployeeGridView.Visible = false;
                //    Panel2.Visible = false;
                //}
                //else
                //{
                //    EmployeeGridView.DataSource = manageDeputyController.selectEmployee("CPSC");//TODO
                //    EmployeeGridView.DataBind();
                //    Panel1.Visible = false;
                //    Panel2.Visible = false;
                //}

                MessageLabel.Visible = false;
            }
        }

        protected void EmployeeGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Panel2.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = EmployeeGridView.Rows[index];
                StartCalendar.Visible = true;
                StLabel.Visible = true;
                Session["selected"] = row.Cells[0].Text.ToString();
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if(Panel1.Visible==true)
            {
                manageDeputyController.selectUpdate(UIDLabel.Text.ToString(), (DateTime)Session["startDate"], EndCalendar.SelectedDate);
            }
            else
            {
                string uid = Session["selected"].ToString();
                manageDeputyController.selectAdd(uid, StartCalendar.SelectedDate, EndCalendar.SelectedDate);
            }
            Response.Redirect(Request.RawUrl);
        }

        protected void UpdpateButton_Click(object sender, EventArgs e)
        {
            StLabel.Visible = false;
            StartCalendar.Visible = false;
            Panel2.Visible = true;
        }

        protected void RemoveButton_Click(object sender, EventArgs e)
        {
            manageDeputyController.selectRemove(UIDLabel.Text.ToString());
            Response.Redirect(Request.RawUrl);
        }

        protected void Calendar_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date.CompareTo(DateTime.Today) < 0)
            {
                e.Cell.BackColor = Color.Gray;
                e.Day.IsSelectable = false;
            }
        }

        protected void EndCalendar_SelectionChanged(object sender, EventArgs e)
        {
            if (Panel1.Visible == false)
            {
                if(StartCalendar.SelectedDate >EndCalendar.SelectedDate)
                {
                    MessageLabel.Visible = true;
                    SubmitButton.Enabled = false;
                }
                else
                {
                    MessageLabel.Visible = false;
                    SubmitButton.Enabled = true;
                }
            }
            else
            {
                DateTime endDate = (DateTime)Session["EndDate"];
                if (endDate>EndCalendar.SelectedDate)
                {
                    MessageLabel.Visible = true;
                    SubmitButton.Enabled = false;
                }
                else
                {
                    MessageLabel.Visible = false;
                    SubmitButton.Enabled = true;
                }
            }
        }


        protected void EmployeeGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EmployeeGridView.PageIndex = e.NewPageIndex;
            dataBind();
        }


        private void dataBind()
        {
            List<User> userCollection = manageDeputyController.loadController(userEntity.DepartmentId);
            if (userCollection.Count != 0)
            {
                User user = userCollection[0];
                UIDLabel.Text = user.UserID;
                NameLabel.Text = user.UserName;
                Session["startDate"] = user.StartDate;
                StartDateLabel.Text = "Start Date : " + user.StartDate.Date.ToString("d");
                EndDateLabel.Text = "End Date : " + user.EndDate.Date.ToString("d");
                Session["EndDate"] = user.EndDate;
                Panel1.Visible = true;
                EmployeeGridView.Visible = false;
                Panel2.Visible = false;
            }
            else
            {
                EmployeeGridView.DataSource = manageDeputyController.selectEmployee(userEntity.DepartmentId);
                EmployeeGridView.DataBind();
                Panel1.Visible = false;
                Panel2.Visible = false;
            }
        }

    }
}