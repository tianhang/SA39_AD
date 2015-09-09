using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.Entities;
using ClassLibrary.Controllers;
using ClassLibrary.Helper;
using System.Drawing;
using ClassLibrary.Entities;


namespace SSISWebApplication.WebPages
{
    public partial class GenerateStationaryRetrievalForm : System.Web.UI.Page
    {
        ErrorLog errorobj;
        GenerateStationaryRetrievalFormController generateStationaryRetrievalFormController;
        protected void Page_Load(object sender, EventArgs e)
        {
            generateStationaryRetrievalFormController = new GenerateStationaryRetrievalFormController();

            if (!IsPostBack)
            {
                

                List<RetrievalHelper> retrievalHelperCollection = generateStationaryRetrievalFormController.loadController();

                if (retrievalHelperCollection.Count != 0)
                {
                    RetrievalFormGridView.DataSource = retrievalHelperCollection;
                    RetrievalFormGridView.DataBind();
                    MsgLabel.Visible = false;
                    PrintButton.Visible = true;
                }
                else
                {
                    MsgLabel.Visible = true;
                    PrintButton.Visible = false;
                }
            }
        }


        protected void RetrievalFormGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            RetrievalFormGridView.EditIndex = e.NewEditIndex;
            dataBind();
        }


        protected void RetrievalFormGridView_RowEditing_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = RetrievalFormGridView.Rows[e.RowIndex];
            string itemId = row.Cells[0].Text;
            string stock = ((TextBox)row.Cells[4].Controls[1]).Text;
            int num;
            if (!(int.TryParse(stock, out num)))
                {
                    row.ForeColor = Color.Red;
                }
                else if (Convert.ToInt32(stock)<0)
                {
                    row.ForeColor = Color.Red;
                }
            else
            {
                ClassLibrary.Entities.Item item = new ClassLibrary.Entities.Item();
                item.ItemId = itemId;
                item.StockBalance = Convert.ToInt32(stock);
                generateStationaryRetrievalFormController.updateStock(item);
            RetrievalFormGridView.EditIndex = -1;
            dataBind();
                    row.ForeColor = Color.Black;
                }

            
        }

        protected void RetrievalFormGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            RetrievalFormGridView.EditIndex = -1;
            dataBind();
        }



        private void dataBind()
        {
            List<RetrievalHelper> retrievalHelperCollection = generateStationaryRetrievalFormController.loadController();
            if (retrievalHelperCollection.Count != 0)
            {
                RetrievalFormGridView.DataSource = retrievalHelperCollection;
                RetrievalFormGridView.DataBind();
            }
        }
    }
}