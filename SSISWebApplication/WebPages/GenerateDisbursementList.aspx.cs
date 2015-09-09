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
    public partial class GenerateDisbursementList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GenerateDisbursementController generateDisbursementController = new GenerateDisbursementController();
            int result = generateDisbursementController.loadController();
            if (result == 1)
            {
                Response.Redirect("ViewDisbursement.aspx/?caller=1");
            }
            else if (result == 0)
            {
                Response.Redirect("ViewDisbursement.aspx/caller=2");
            }
        }

        protected void btnGenerateDBList_Click(object sender, EventArgs e)
        {
            
        }
    }
}