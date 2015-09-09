using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.Controllers;
using ClassLibrary.EntityFacade;
using ClassLibrary.Entities;

namespace SSISWebApplication.WebPages.Login
{
    public partial class Login : System.Web.UI.Page
    {
        LoginController loginController=new LoginController();
        LoginFacade loginFaca=new LoginFacade();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitializeControls();
            }
        }

        protected void butLogin_Click(object sender, EventArgs e)
        {

            if (loginController.checkAuthentication(txtEmail.Text, txtPassword.Text))
            {
                Session["UserEntity"] = loginFaca.fillUserEntity(txtEmail.Text, txtPassword.Text);               
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Message("Login Fail!...Email or password or department is wrong!!");
            }
        }

        protected void butCanel_Click(object sender, EventArgs e)
        {
            this.InitializeControls();
        }

        //Show message
        public void Message(string msg)
        {            
            dvAlert.Visible = true;
            lblErrorMessage.Text = msg;
            txtEmail.Focus();
        }

        public void InitializeControls()
        {
            this.txtEmail.Text = string.Empty;
            this.txtPassword.Text = string.Empty;
        }
    }
}