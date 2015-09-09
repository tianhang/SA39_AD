using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.Controllers;
using ClassLibrary.EntityFacade;

namespace SSISWebApplication.WebPages
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GenerateDisbursementController generateDisbursementController = new GenerateDisbursementController();
            int result = generateDisbursementController.loadController();
            if(result==1)
            {
                Response.Redirect("ViewDisbursement.aspx/?caller=1");
            }
            else if (result == 0)
            {
                Response.Redirect("ViewDisbursement.aspx/caller=2");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            NotifyUserController notifyUserController = new NotifyUserController();
            notifyUserController.sendEmail("abhinavnair.2311@gmail.com", "Hehehe", "heeeeeeeeee");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            NotifyReordersController notifyReordersController = new NotifyReordersController();
            notifyReordersController.run();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            GetBackAuthorityController getBackAuthorityController = new GetBackAuthorityController();
            getBackAuthorityController.run();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            RequisitionFacade reqFacade = new RequisitionFacade();
            reqFacade.updateDisbursementRequisitions("CPSC");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
        }
    }
}