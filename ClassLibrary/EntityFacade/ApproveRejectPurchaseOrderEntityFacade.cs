using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ClassLibrary.Entities;
using System.Reflection;

namespace ClassLibrary.EntityFacade
{
    public class ApproveRejectPurchaseOrderEntityFacade
    {
        SSISDBEntities entity;
        ReorderItem reorderEntity;
        List<ReorderItem> reorderEntityList;
        public ApproveRejectPurchaseOrderEntityFacade()
        {
            entity = new SSISDBEntities();
        }

        #region LinqMethods
        public DataTable getReorderItemDataList()
        {
            var reorderItemData = from x in entity.reorderItems select x;
            return this.getReorderDataTable(reorderItemData.ToList());
        }
        public void updateReorderItemStatus(string status,string rejectReason, string reorderId)
        {
            reorderItem reorderData = entity.reorderItems.First(i => i.reorderId == reorderId);
            reorderData.status = status;
            reorderData.rejectReason = rejectReason;
            entity.SaveChanges();
        }
        public DataTable searchReorderItem(string supplierId, string status)
        {
            if (supplierId != "All" && status == "All")
            {
                var reorderData = from x in entity.reorderItems where x.supplierId == supplierId select x;
                return getReorderDataTable(reorderData.ToList());
            }
            else if (supplierId == "All" && status != "All")
            {
                var reorderData = from x in entity.reorderItems where x.status == status select x;
                return getReorderDataTable(reorderData.ToList());
            }
            else if (supplierId != "All" && status != "All")
            {
                var reorderData = from x in entity.reorderItems where x.status == status & x.supplierId == supplierId select x;
                return getReorderDataTable(reorderData.ToList());
            }
            else
            {
                var reorderData = from x in entity.reorderItems select x;
                return getReorderDataTable(reorderData.ToList());
            }
        }
        public DataTable getStatusDataList()
        {
            var statusData = (from x in entity.reorderItems select new { x.status }).Distinct().ToList();
            return ToDataTable(statusData);
        }
        public item getItemById(string itemId)
        {
            var itemData = from x in entity.items where x.itemId == itemId select x;
            return itemData.SingleOrDefault();
        }

        public supplier getSupplierById(string supplierId)
        {
            var supplierData = from x in entity.suppliers where x.supplierId == supplierId select x;
            return supplierData.SingleOrDefault();
        }

        public DataTable getSupplierDataList()
        {
            var supplierData = from x in entity.suppliers select x;
            return ToDataTable(supplierData.ToList());
        }

        public user getUserById(string userId)
        {
            var userData = from x in entity.users where x.userId == userId select x;
            return userData.SingleOrDefault();
        }
        #endregion

        #region FillEntity
        public List<ReorderItem> fillReorderItemEntity(List<reorderItem> reorderItemDataList)
        {
            reorderEntityList = new List<ReorderItem>();
            for (int i = 0; i < reorderItemDataList.Count; i++)
            {
                reorderEntity = new ReorderItem();
                reorderEntity.ReorderItemId = reorderItemDataList[i].reorderId;
                reorderEntity.ItemId = reorderItemDataList[i].itemId;
                reorderEntity.ItemName = this.getItemById(reorderEntity.ItemId).description;
                reorderEntity.SupplierId = reorderItemDataList[i].supplierId;
                reorderEntity.SupplierName = this.getSupplierById(reorderEntity.SupplierId).name;
                reorderEntity.UserId = reorderItemDataList[i].userId;
                reorderEntity.UserName = this.getUserById(reorderEntity.UserId).name;
                reorderEntity.QtyToOrder = reorderItemDataList[i].qty.Value;
                reorderEntity.Amount = reorderItemDataList[i].amount.Value;
                reorderEntity.Price = reorderEntity.Amount / reorderEntity.QtyToOrder;
                reorderEntity.Status = reorderItemDataList[i].status;
                reorderEntity.RejectReason = reorderItemDataList[i].rejectReason;
                reorderEntityList.Add(reorderEntity);
            }
            return reorderEntityList;
        }
        #endregion

        #region DataTableConverter
        public DataTable getReorderDataTable(List<reorderItem> reorderItemDataList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ReorderItemID", String.Empty.GetType());
            dt.Columns.Add("ItemID", String.Empty.GetType());
            dt.Columns.Add("ItemName", String.Empty.GetType());
            dt.Columns.Add("SupplierID", String.Empty.GetType());
            dt.Columns.Add("SupplierName", String.Empty.GetType());
            dt.Columns.Add("UserID", String.Empty.GetType());
            dt.Columns.Add("UserName", String.Empty.GetType());
            dt.Columns.Add("QtyToOrder", Int32.MinValue.GetType());
            dt.Columns.Add("Amount", double.MinValue.GetType());
            dt.Columns.Add("Price", double.MinValue.GetType());
            dt.Columns.Add("Status", String.Empty.GetType());
            dt.Columns.Add("RejectReason", String.Empty.GetType());
            dt.Columns.Add("Enable", String.Empty.GetType());
            for (int i = 0; i < reorderItemDataList.Count(); i++)
            {
                DataRow dr = dt.NewRow();
                dr["ReorderItemID"] = reorderItemDataList[i].reorderId;
                dr["ItemID"] = reorderItemDataList[i].itemId;
                dr["ItemName"] = this.getItemById(dr["ItemID"].ToString()).description;
                dr["SupplierID"] = reorderItemDataList[i].supplierId;
                dr["SupplierName"] = this.getSupplierById(dr["SupplierID"].ToString()).name;
                dr["UserID"] = reorderItemDataList[i].userId;
                dr["UserName"] = this.getUserById(dr["UserID"].ToString()).name;
                dr["QtyToOrder"] = reorderItemDataList[i].qty.Value;
                dr["Amount"] = reorderItemDataList[i].amount.Value;
                dr["Price"] = double.Parse(dr["Amount"].ToString()) / double.Parse(dr["QtyToOrder"].ToString());
                dr["Status"] = reorderItemDataList[i].status;
                dr["RejectReason"] = reorderItemDataList[i].rejectReason;
                if (dr["Status"].ToString() == "Approve" | dr["Status"].ToString() == "Reject")
                    dr["Enable"] = "disabled";
                else
                    dr["Enable"] = "";
                dt.Rows.Add(dr);
            }
            return dt;
        }
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
