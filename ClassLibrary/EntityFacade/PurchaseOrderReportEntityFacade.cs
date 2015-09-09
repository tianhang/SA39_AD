using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Configuration;
using System.Data.SqlClient;

namespace ClassLibrary.EntityFacade
{
    public class PurchaseOrderReportEntityFacade
    {
        SSISDBEntities entity;
        SqlConnection sqlCon;
        SqlCommand sqlComm;
        SqlDataAdapter da;

        public PurchaseOrderReportEntityFacade()
        {
            entity = new SSISDBEntities();
            sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["SSISDBConnectionString"].ConnectionString);
        }
        public DataTable getPurchaseOrderByCriteria(string monthYear, string supplier, string category)
        {
            sqlComm = new SqlCommand("SelectPurchaseOrderData", sqlCon);
            sqlComm.CommandType = CommandType.StoredProcedure;

            //sqlComm.Parameters.Add("@Status", SqlDbType.NVarChar).Value = status;
            sqlComm.Parameters.Add("@MonthYear", SqlDbType.NVarChar).Value = monthYear;
            sqlComm.Parameters.Add("@Category", SqlDbType.NVarChar).Value = category;
            sqlComm.Parameters.Add("@Supplier", SqlDbType.NVarChar).Value = supplier;

            da = new SqlDataAdapter(sqlComm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public List<supplier> getSupplierDataList()
        {
            var supplierData = from x in entity.suppliers select x;
            return supplierData.ToList();
        }

        public List<category> getCategoryDataList()
        {
            var categoryData = from x in entity.categories select x;
            return categoryData.ToList();
        }

    }
}
