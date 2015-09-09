using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace SSISWebApplication.WebPages.Report.ReportViewer
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string header = Request.QueryString["ReportHeader"].ToString();
            switch(header){
                case "Req": this.lblReportHeader.Text = "Requisition Trend Report";
                    break;
                case "Stock": this.lblReportHeader.Text = "Stock Balance Report";
                    break;
                case "PurchaseOrder": this.lblReportHeader.Text = "Purchase Order Report";
                    break;
            }
            ReportDocument reportDoc = new ReportDocument();
            reportDoc = (ReportDocument)Session["ReportDocument"];
            crvReportViewer.ReportSource = reportDoc;
        }
    }
}