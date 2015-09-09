using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.EntityFacade;
using ClassLibrary.Controllers;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

namespace SSISWebApplication.WebPages.Report.ReportGenerator
{
    public partial class StockBalanceReportGenerator : System.Web.UI.Page
    {
        ReportGeneratorController reportGenerator = new ReportGeneratorController();
        StockBalanceReportEntityFacade stockBalanceFaca = new StockBalanceReportEntityFacade();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlCategory_Bind();
                this.ddlItemName_Bind();
            }
        }

        protected void ddlCategoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlItemName_Bind();
        }
        protected void butGenerate_Click(object sender, EventArgs e)
        {
            ReportDocument reportDoc = new ReportDocument();
            if (this.ddlCategoryName.SelectedIndex == 0)
            {                
                reportDoc.Load(Server.MapPath("~/WebPages/Report/CrystalReportFiles/StockBalanceReport.rpt"));                
            }
            else
            {
                reportDoc.Load(Server.MapPath("~/WebPages/Report/CrystalReportFiles/StockBalanceReportByCategory.rpt"));            
            }
            DataTable dt = new DataTable();
            dt = reportGenerator.searchStockBalance(ddlCategoryName.SelectedItem.Text, ddlItemName.SelectedItem.Text);
            if (dt.Rows.Count > 0)
            {
                reportDoc.SetDataSource(dt);
                Session["ReportDocument"] = reportDoc;
                Response.Redirect("~/WebPages/Report/ReportViewer/ReportViewer.aspx?ReportHeader=Stock");
            }
            else
            {
                this.Message("There is no data for criteria you selected!!");
            }
        }

        protected void butCancel_Click(object sender, EventArgs e)
        {
            this.InitializeControls();
        }

        #region HelperMethods
        public void ddlItemName_Bind()
        {
            ddlItemName.DataValueField = "itemid";
            ddlItemName.DataTextField = "description";
            ddlItemName.DataSource = stockBalanceFaca.getItemDataList(ddlCategoryName.SelectedValue);
            ddlItemName.DataBind();
            //add default item to show
            ListItem lstAll = new ListItem("All", "All");
            ddlItemName.Items.Insert(0, lstAll);
        }
        public void ddlCategory_Bind()
        {
            ddlCategoryName.DataValueField = "categoryId";
            ddlCategoryName.DataTextField = "name";
            ddlCategoryName.DataSource = stockBalanceFaca.getCategoryDataList();
            ddlCategoryName.DataBind();
            //add default item to show
            ListItem lstAll = new ListItem("All", "All");
            ddlCategoryName.Items.Insert(0, lstAll);
        }
        public void InitializeControls()
        {
            this.ddlCategoryName.SelectedIndex = 0;
            this.ddlItemName_Bind();
            this.ddlItemName.SelectedIndex = 0;
            dvAlert.Visible = false;
        }
        //Show message
        public void Message(string msg)
        {
            dvAlert.Visible = true;
            lblErrorMessage.Text = msg;
        }
        #endregion
    }
}