using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.EntityFacade;
using System.Data;

namespace ClassLibrary.Controllers
{
    public class ReportGeneratorController
    {
        RequisitionTrendEntityFacade reqTrendFaca;
        StockBalanceReportEntityFacade stockBalanceFaca;
        PurchaseOrderReportEntityFacade purchaseOrderFaca;
        public ReportGeneratorController()
        {
            reqTrendFaca = new RequisitionTrendEntityFacade();
            stockBalanceFaca = new StockBalanceReportEntityFacade();
            purchaseOrderFaca = new PurchaseOrderReportEntityFacade();
        }


        public DataTable searchRequisitionTrend(string status, List<string> monthYearList,string department,string category)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < monthYearList.Count; i++)
            {
                dt.Merge(reqTrendFaca.getRequisitionTrendByCriteria(status, monthYearList[i].ToString(),department,category));
            }
            return dt;
        }

        public DataTable searchPurchasOrder(List<string> monthYearList, string supplier, string category)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < monthYearList.Count; i++)
            {
                dt.Merge(purchaseOrderFaca.getPurchaseOrderByCriteria(monthYearList[i].ToString(), supplier, category));
            }
            return dt;
        }

        public DataTable searchStockBalance(string category, string itemName)
        {
            return stockBalanceFaca.getStockBalanceByCriteria(category, itemName);
        }
    }
}
