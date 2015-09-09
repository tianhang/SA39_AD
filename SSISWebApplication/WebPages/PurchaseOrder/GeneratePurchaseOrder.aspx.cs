using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.Controllers;
using ClassLibrary.Entities;

namespace SSISWebApplication.WebPages.PurchaseOrder
{

    public partial class GeneratePurchaseOrder : System.Web.UI.Page
    {
        GeneratePurchaseOrderFormController controller = new GeneratePurchaseOrderFormController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SupplierDropDownList.DataSource = controller.acceptSupplierList();
                SupplierDropDownList.DataBind();
                Date.Text = System.DateTime.Now.ToShortDateString();
                newpanel.Visible = false;
                newPanel2.Visible = false;
            }
            string supplierName = SupplierDropDownList.SelectedValue;
            supplierNameLbl.Text = supplierName;
            ContactName.Text = controller.getSupplier(supplierName).contactName;
            PhoneNo.Text = Convert.ToString(controller.getSupplier(supplierName).phoneNo);
            FaxNo.Text = Convert.ToString(controller.getSupplier(supplierName).faxNo);
            Address.Text = controller.getSupplier(supplierName).address;
            
        }

        protected void SupplierDropDownList_SelectedIndexChanged(object sender, EventArgs e)                         // modify in 3/11
        {
            //string name = SupplierDropDownList.SelectedValue;
            //if (name == null)
            //{
            //    System.Diagnostics.Debug.WriteLine("Not get the value");
            //}
            //else
            //{
            //    List<ReorderItem> list = controller.selectSupplier(name);
            //    if (list.Count != 0)
            //    {
            //        MessageLabel.Text = "";
            //        SupplierGridView.Visible = true;
            //        double total = 0;

            //        foreach(ReorderItem o in list)
            //            total += Convert.ToDouble(o.Amount);

            //        SupplierGridView.DataSource = list;
            //        SupplierGridView.DataBind();
            //        AmountLabel.Text = Convert.ToString(total);

            //        controller.selectGenerate(name, list);
            //    }
            //    else
            //    {
            //        SupplierGridView.Visible = false;
            //        AmountLabel.Text = "0";
            //        MessageLabel.Text = "There is no purchase order for supplier " + name;
            //        MessageLabel.ForeColor = System.Drawing.Color.Red;
            //    }
            //}    
        }
        protected void btnGenerate_Click(object sender, EventArgs e)                         // modify in 3/11
        {
            string name = SupplierDropDownList.SelectedValue;
            List<ReorderItem> list = controller.selectSupplier(name);
            if (list.Count != 0)
            {
                MessageLabel.Text = "";
                SupplierGridView.Visible = true;
                double total = 0;

                foreach (ReorderItem o in list)
                    total += Convert.ToDouble(o.Amount);

                List<ReorderItem> newList = new List<ReorderItem>();
                bool flag = false;
                foreach (ReorderItem r in list)
                {
                    if (newList.Count == 0)
                    {
                        newList.Add(r);
                    }
                    else
                    {
                        foreach (ReorderItem rr in newList.ToList())
                        {
                            if (rr.ItemName == r.ItemName)
                            {
                                rr.QtyToOrder += r.QtyToOrder;
                                rr.Amount += r.Amount;
                                flag = true;
                            }
                        }
                        if (!flag)
                        {
                            newList.Add(r);
                        }
                    }
                }

                foreach (ReorderItem r in newList)
                {
                    System.Diagnostics.Debug.Print(r.ItemName + "  " + r.QtyToOrder);
                }

                SupplierGridView.DataSource = newList;
                SupplierGridView.DataBind();
                AmountLabel.Text = Convert.ToString(total);

                controller.selectGenerate(name, newList, list);

                newpanel.Visible = true;
                newPanel2.Visible = true;
            }
            else
            {
                SupplierGridView.Visible = false;
                AmountLabel.Text = "0";
                MessageLabel.Text = "There is no purchase order for supplier " + name;
                MessageLabel.ForeColor = System.Drawing.Color.Red;
                newpanel.Visible = false;
                newPanel2.Visible = false;
            }
            //Response.Redirect(Request.RawUrl);
        }    


    }
}