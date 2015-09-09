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
    public class StockBalanceReportEntityFacade
    {
        SSISDBEntities entity;
        SqlConnection sqlCon;
        SqlCommand sqlComm;
        SqlDataAdapter da;

        public StockBalanceReportEntityFacade()
        {
            entity = new SSISDBEntities();
            sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["SSISDBConnectionString"].ConnectionString);
        }

        public DataTable getStockBalanceByCriteria(string category, string itemName)
        {
            sqlComm = new SqlCommand("SelectStockBalanceData", sqlCon);
            sqlComm.CommandType = CommandType.StoredProcedure;

            sqlComm.Parameters.Add("@Category", SqlDbType.NVarChar).Value = category;
            sqlComm.Parameters.Add("@ItemName", SqlDbType.NVarChar).Value = itemName;

            da = new SqlDataAdapter(sqlComm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public List<item> getItemDataList(string categoryId)
        {
            if (categoryId != "All")
            {
                var itemData = from x in entity.items where x.categoryId == categoryId select x;
                return itemData.ToList();
            }
            else
            {
                var itemData = from x in entity.items select x;
                return itemData.ToList();
            }

        }

        public List<category> getCategoryDataList()
        {
            var categoryData = from x in entity.categories select x;
            return categoryData.ToList();
        }

        #region DatatableConverter
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        #endregion
    }
}
