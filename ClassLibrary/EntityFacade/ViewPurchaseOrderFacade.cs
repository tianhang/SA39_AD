using ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibrary.EntityFacade
{
    public class ViewPurchaseOrderFacade
    {
        SSISDBEntities entity;
        PurchaseOrder poEntity;
        PurchaseOrderDetails poDetailEntity;
        List<PurchaseOrderDetails> poDetailList;

        public ViewPurchaseOrderFacade()
        {
            entity = new SSISDBEntities();
        }

        #region LinqMethods
        public DataTable getPurchaseOrderDataList()
        {
            var poData = from x in entity.purchaseOrders select x;
            return getPurchaseOrderDataTable(poData.ToList());
        }

        public DataTable getStatusDataList()
        {
            var poData = (from x in entity.purchaseOrders select new { x.status }).Distinct().ToList();
            return ToDataTable(poData);
        }

        public DataTable searchPurchaseOrder(string supplierId, string status)
        {
            if (supplierId != "All" && status == "All")
            {
                var poData = from x in entity.purchaseOrders where x.supplierId == supplierId select x;
                return getPurchaseOrderDataTable(poData.ToList());
            }
            else if (supplierId == "All" && status != "All")
            {
                var poData = from x in entity.purchaseOrders where x.status == status select x;
                return getPurchaseOrderDataTable(poData.ToList());
            }
            else if (supplierId != "All" && status != "All")
            {
                var poData = from x in entity.purchaseOrders where x.status == status & x.supplierId == supplierId select x;
                return getPurchaseOrderDataTable(poData.ToList());
            }
            else
            {
                var poData = from x in entity.purchaseOrders select x;
                return getPurchaseOrderDataTable(poData.ToList());
            }
        }

        public PurchaseOrder getPurchaseOrderDataById(string poId)
        {
            var poData = from x in entity.purchaseOrders where x.purchaseOrderId == poId select x;
            return fillPurchaseOrderEntity(poData.SingleOrDefault());
        }

        public void updatePurchaseOrderStatus(string status, string poId)
        {
            purchaseOrder poData = entity.purchaseOrders.First(i => i.purchaseOrderId == poId);
            poData.status = status;
            poData.deliveryDate = System.DateTime.Now;
            entity.SaveChanges();
        }

        public void updateStock(DataTable dtPODetail)
        {
            foreach (DataRow dr in dtPODetail.Select())
            {
                string itemID = dr["ItemId"].ToString();
                item itemData = entity.items.First(i => i.itemId == itemID);
                itemData.stockBalance = itemData.stockBalance + Int32.Parse(dr["Qty"].ToString());
                entity.SaveChanges();
            }
        }

        public DataTable getPurchaseOrderDetailById(string poId)
        {
            var poData = from x in entity.purchaseOrderDetails where x.purchaseOrderId == poId select x;
            return ToDataTable(fillPurchaseOrderDetailsEntity(poData.ToList()));
        }

        public int getPurchaseOrderDetailCount(string poId)
        {
            var supplierData = from x in entity.purchaseOrderDetails where x.purchaseOrderId == poId select x.itemId;
            return supplierData.Count();
        }

        public item getItemDataById(string itemId)
        {
            var itemData = from x in entity.items where x.itemId == itemId select x;
            return itemData.SingleOrDefault();
        }

        public DataTable getSupplierData()
        {
            var supplierData = from x in entity.suppliers select x;
            return ToDataTable(supplierData.ToList());
        }

        public supplier getSupplierDataById(string supplierId)
        {
            var supplierData = from x in entity.suppliers where x.supplierId == supplierId select x;
            return supplierData.SingleOrDefault();
        }
        #endregion

        #region fillEntity
        public PurchaseOrder fillPurchaseOrderEntity(purchaseOrder poData)
        {
            poEntity = new PurchaseOrder();
            poEntity.PurchaseOrderId = poData.purchaseOrderId;
            poEntity.SupplierId = poData.supplierId;
            poEntity.SupplierName = getSupplierDataById(poData.supplierId).name;
            poEntity.OrderDate = Convert.ToDateTime(poData.orderDate);
            poEntity.DeliveryDate = Convert.ToDateTime(poData.deliveryDate);
            poEntity.Status = poData.status;
            return poEntity;
        }

        public List<PurchaseOrderDetails> fillPurchaseOrderDetailsEntity(List<purchaseOrderDetail> poDetailDataList)
        {
            poDetailList = new List<PurchaseOrderDetails>();

            for (int i = 0; i < poDetailDataList.Count; i++)
            {
                poDetailEntity = new PurchaseOrderDetails();
                poDetailEntity.ItemId = poDetailDataList[i].itemId;
                poDetailEntity.ItemName = getItemDataById(poDetailDataList[i].itemId.ToString()).description;
                poDetailEntity.Qty = Convert.ToInt32(poDetailDataList[i].qty);
                poDetailEntity.Amount = Convert.ToDouble(poDetailDataList[i].amount);
                poDetailEntity.Price = poDetailEntity.Amount / poDetailEntity.Qty;
                poDetailList.Add(poDetailEntity);
            }
            return poDetailList;
        }
        #endregion

        #region dataTableConverter
        public DataTable getPurchaseOrderDataTable(List<purchaseOrder> poList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PurchaseOrderID", String.Empty.GetType());
            dt.Columns.Add("SupplierID", String.Empty.GetType());
            dt.Columns.Add("SupplierName", String.Empty.GetType());
            dt.Columns.Add("OrderDate", String.Empty.GetType());
            dt.Columns.Add("DeliveryDate", String.Empty.GetType());
            dt.Columns.Add("Status", String.Empty.GetType());
            dt.Columns.Add("Status_Bool", String.Empty.GetType());
            dt.Columns.Add("PoDetailCount", String.Empty.GetType());
            for (int i = 0; i < poList.Count(); i++)
            {
                DataRow dr = dt.NewRow();
                dr["PurchaseOrderID"] = poList[i].purchaseOrderId;
                dr["SupplierID"] = poList[i].supplierId;
                dr["SupplierName"] = getSupplierDataById(poList[i].supplierId).name;
                dr["OrderDate"] = poList[i].orderDate.Value.ToShortDateString();
                if (string.IsNullOrEmpty(poList[i].deliveryDate.ToString()))
                    dr["DeliveryDate"] = "-";
                else
                    dr["DeliveryDate"] = poList[i].deliveryDate.Value.ToShortDateString();
                dr["Status"] = poList[i].status;
                if (dr["Status"].ToString() == "Delivered")
                    dr["Status_Bool"] = "btn btn-primary navbar-btn disabled";
                else
                    dr["Status_Bool"] = "btn btn-primary navbar-btn";
                dr["PoDetailCount"] = getPurchaseOrderDetailCount(poList[i].purchaseOrderId).ToString();
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
