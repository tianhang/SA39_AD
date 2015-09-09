using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.Controllers;
using CrystalDecisions.CrystalReports.Engine;
using ClassLibrary.EntityFacade;
using System.Data;

namespace SSISWebApplication.WebPages.Report.ReportGenerator
{
    public partial class RequisitionTrendReportGenerator : System.Web.UI.Page
    {
        ReportGeneratorController reportGenerator = new ReportGeneratorController();
        RequisitionTrendEntityFacade reqFaca = new RequisitionTrendEntityFacade();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlDepartment_Bind();
                this.ddlCategory_Bind();
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
                if (ddlDepartment.SelectedIndex == 0)
                {
                    reportDoc.Load(Server.MapPath("~/WebPages/Report/CrystalReportFiles/RequisitionTrendReport.rpt"));
                }
                else
                {
                    reportDoc.Load(Server.MapPath("~/WebPages/Report/CrystalReportFiles/RequisitionTrendReportByDepartment.rpt"));
                }
                
                List<String> monthYearList = new List<String>();
                for (int i = 0; i < blCompareDateList.Items.Count; i++)
                {
                    monthYearList.Add(blCompareDateList.Items[i].Value);
                }
                DataTable dt = new DataTable();
                dt = reportGenerator.searchRequisitionTrend("Approved", monthYearList, this.ddlDepartment.SelectedItem.Text, this.ddlCategoryName.SelectedItem.Text);
                if (dt.Rows.Count > 0)
                {
                    reportDoc.SetDataSource(dt);
                    Session["ReportDocument"] = reportDoc;
                    Response.Redirect("~/WebPages/Report/ReportViewer/ReportViewer.aspx?ReportHeader=Req");
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
        public void ddlDepartment_Bind()
        {
            ddlDepartment.DataValueField = "departmentid";
            ddlDepartment.DataTextField = "name";
            ddlDepartment.DataSource = reqFaca.getDepartmentDataList();
            ddlDepartment.DataBind();
            //add default item to show
            ListItem lstAll = new ListItem("All", "All");
            ddlDepartment.Items.Insert(0, lstAll);
        }
        public void ddlCategory_Bind()
        {
            ddlCategoryName.DataValueField = "categoryId";
            ddlCategoryName.DataTextField = "name";
            ddlCategoryName.DataSource = reqFaca.getCategoryDataList();
            ddlCategoryName.DataBind();
            //add default item to show
            ListItem lstAll = new ListItem("All", "All");
            ddlCategoryName.Items.Insert(0, lstAll);
        }
        public void InitializeControls()
        {
            this.ddlDepartment.SelectedIndex = 0;
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