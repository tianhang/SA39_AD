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
    public class RequisitionTrendEntityFacade
    {
        SSISDBEntities entity;
        SqlConnection sqlCon;
        SqlCommand sqlComm;
        SqlDataAdapter da;

        public RequisitionTrendEntityFacade()
        {
            entity = new SSISDBEntities();
            sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["SSISDBConnectionString"].ConnectionString);
        }

        public DataTable getRequisitionTrendByCriteria(string status,string monthYear,string department,string category)
        {
            sqlComm = new SqlCommand("SelectRequisitionData", sqlCon);
            sqlComm.CommandType = CommandType.StoredProcedure;

            sqlComm.Parameters.Add("@Status", SqlDbType.NVarChar).Value = status;
            sqlComm.Parameters.Add("@MonthYear", SqlDbType.NVarChar).Value = monthYear;
            sqlComm.Parameters.Add("@Department", SqlDbType.NVarChar).Value = department;
            sqlComm.Parameters.Add("@Category", SqlDbType.NVarChar).Value = category;

            da = new SqlDataAdapter(sqlComm);
            DataTable dt=new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable getRequisitionTrendByDepartmentID(string departmentId)
        {
            sqlComm = new SqlCommand("SelectRequisitionByDepartmentID", sqlCon);
            sqlComm.CommandType = CommandType.StoredProcedure;
            sqlComm.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = departmentId;

            da = new SqlDataAdapter(sqlComm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public List<department> getDepartmentDataList()
        {
            var departmentData = from x in entity.departments select x;
            return departmentData.ToList();
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
