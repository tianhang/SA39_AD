using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.Controllers;

namespace SSISWebApplication.WebPages.PurchaseOrder
{
    public partial class DetailWindow : System.Web.UI.Page
    {
        ViewItemPOController controller = new ViewItemPOController();
        protected void Page_Load(object sender, EventArgs e)                                 // modify in 3/11
        {
            string itemName = Request.Params["itemName"];
            
            Statement.Text = "";
            if (controller.acceptPurchaseOrderList(itemName).Count != 0)
            {
                TableName.Text = " Detail Table for " + itemName;
                GridView1.DataSource = controller.acceptPurchaseOrderList(itemName);
                GridView1.DataBind();
            }
            else
            {
                Statement.Text = "There is no detail for item " + itemName;
            }
        }
    }
}