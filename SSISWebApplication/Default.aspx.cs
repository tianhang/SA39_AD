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
using ClassLibrary.Entities;

namespace SSISWebApplication
{
    public partial class _Default : Page
    {
        RequisitionTrendEntityFacade reqFaca = new RequisitionTrendEntityFacade();
        //User userEntity;
        protected void Page_Load(object sender, EventArgs e)
        {
            //ReportDocument reportDoc = new ReportDocument();
            //reportDoc.Load(Server.MapPath("~/WebPages/Report/CrystalReportFiles/RequisitionDefaultView.rpt"));
            //DataTable dt = new DataTable();
            //userEntity = (User)Session["UserEntity"];
            //dt = reqFaca.getRequisitionTrendByDepartmentID(userEntity.DepartmentId);
            //reportDoc.SetDataSource(dt);
            //crvRequisition.ReportSource = reportDoc;
        }
    }
}