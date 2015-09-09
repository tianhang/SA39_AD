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
    public partial class PurchaseOrderReportGenerator : System.Web.UI.Page
    {
        ReportGeneratorController reportGenerator = new ReportGeneratorController();
        PurchaseOrderReportEntityFacade purchaseOrderFaca = new PurchaseOrderReportEntityFacade();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlCategory_Bind();
                this.ddlSupplierName_Bind();
                this.bindYear(7);
            }
        }
        protected void btnAddDates_Click(object sender, EventArgs e)
        {
            string DateName = ddlMonth.SelectedValue + " " + ddlYear.SelectedValue;
            if (blCompareDateList.Items.FindByText(DateName) == null)
            {
                ListItem DateList = new ListItem();
                DateList.Text = DateName;
                DateList.Value = DateName;
                blCompareDateList.Items.Add(DateList);
            }
            else
            {
                this.Message("This Month and Year is already added!!");
            }

        }

        protected void butGenerate_Click(object sender, EventArgs e)
        {
            if (blCompareDateList.Items.Count > 0)
            {
                ReportDocument reportDoc = new ReportDocument();
                if (ddlSupplierName.SelectedIndex == 0)
                {
                    reportDoc.Load(Server.MapPath("~/WebPages/Report/CrystalReportFiles/PurchaseOrderReport.rpt"));
                }
                else
                {
                    reportDoc.Load(Server.MapPath("~/WebPages/Report/CrystalReportFiles/PurchaseOrderReportBySupplier.rpt"));
                }

                List<String> monthYearList = new List<String>();
                for (int i = 0; i < blCompareDateList.Items.Count; i++)
                {
                    monthYearList.Add(blCompareDateList.Items[i].Value);
                }
                DataTable dt = new DataTable();
                dt = reportGenerator.searchPurchasOrder(monthYearList, this.ddlSupplierName.SelectedItem.Text, this.ddlCategoryName.SelectedItem.Text);
                if (dt.Rows.Count > 0)
                {
                    reportDoc.SetDataSource(dt);
                    Session["ReportDocument"] = reportDoc;
                    Response.Redirect("~/WebPages/Report/ReportViewer/ReportViewer.aspx?ReportHeader=PurchaseOrder");
                }
                else
                {
                    this.Message("There is no data for criteria you selected!!");
                }
            }
            else
            {
                this.Message("Select Month and Year to Compare!!");
            }
        }

        protected void butCancel_Click(object sender, EventArgs e)
        {
            this.InitializeControls();
        }

        #region HelperMethods
        public void bindYear(int numberOfYears)
        {
            for (int i = 0; i < numberOfYears; i++)
            {
                ListItem lstYear = new ListItem();
                lstYear.Text = (System.DateTime.Now.Year - i).ToString();
                lstYear.Value = (System.DateTime.Now.Year - i).ToString();
                ddlYear.Items.Add(lstYear);
            }
        }
        public void ddlSupplierName_Bind()
        {
            ddlSupplierName.DataValueField = "supplierid";
            ddlSupplierName.DataTextField = "name";
            ddlSupplierName.DataSource = purchaseOrderFaca.getSupplierDataList();
            ddlSupplierName.DataBind();
            //add default item to show
            ListItem lstAll = new ListItem("All", "All");
            ddlSupplierName.Items.Insert(0, lstAll);
        }
        public void ddlCategory_Bind()
        {
            ddlCategoryName.DataValueField = "categoryId";
            ddlCategoryName.DataTextField = "name";
            ddlCategoryName.DataSource = purchaseOrderFaca.getCategoryDataList();
            ddlCategoryName.DataBind();
            //add default item to show
            ListItem lstAll = new ListItem("All", "All");
            ddlCategoryName.Items.Insert(0, lstAll);
        }
        public void InitializeControls()
        {
            this.ddlSupplierName.SelectedIndex = 0;
            this.ddlCategoryName.SelectedIndex = 0;
            this.blCompareDateList.Items.Clear();
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