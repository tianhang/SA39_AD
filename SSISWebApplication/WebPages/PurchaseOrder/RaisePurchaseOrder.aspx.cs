using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary.Controllers;
using System.Data;
using ClassLibrary.Entities;
using ClassLibrary.EntityFacade;
using System.Drawing;

namespace SSISWebApplication.WebPages.PurchaseOrder
{
    public partial class RaisePurchaseOrder : System.Web.UI.Page
    {
        RaisePurchaseOrderController controller = new RaisePurchaseOrderController();
        User userEntity;
        protected void Page_Load(object sender, EventArgs e)                                                              // modify in 3/11
        {
            if (!IsPostBack)
            {
                
                CategoryDropDownList.DataSource = controller.getCategoryList();
                CategoryDropDownList.DataBind();

                SelectGridView.DataSource = controller.acceptItemList();
                SelectGridView.DataBind();
                
                SubmitBtn.Visible = false;
                //ViewState["CurrentTable"] = controller.SetInitialTable();
                //UpdateGridView.DataSource = controller.SetInitialTable();
                //UpdateGridView.DataBind();
            }
            //Statement.Text = "";
            dvAlert.Visible = false;
        }

        protected void SelectGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SelectGridView.PageIndex = e.NewPageIndex;
            SelectGridView.DataSource = controller.acceptItemList();
            SelectGridView.DataBind();
        }

        protected void SupplierGridView_RowDataBound(object sender, GridViewRowEventArgs e) 
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl = (DropDownList)e.Row.FindControl("SupplierDropDownList");
                if (null != ddl)
                {
                    string itemName = e.Row.Cells[0].Text;

                    ddl.DataSource = controller.selectSuppliers(itemName);
                    ddl.DataBind();

                    bool flag = controller.checkLessThanBalance(itemName);
                    if (flag)
                    {
                        e.Row.ForeColor = System.Drawing.Color.Red; 
                    }
                }
            }
        }

        protected void CategoryDropDownList_SelectedIndexChanged(object sender, EventArgs e)   
        {
            string category = CategoryDropDownList.SelectedValue;
            if (category == null)
            {
                System.Diagnostics.Debug.WriteLine("Not get the value");
            }
            if (controller.selectSearch(category) != null)
            {
                SelectGridView.DataSource = controller.selectSearch(category);
                SelectGridView.DataBind();
            }
            else
                System.Diagnostics.Debug.WriteLine("View1 not found**********");
        }

        protected void SelectGridView_RowCommand(object sender, GridViewCommandEventArgs e)                       // modify in 3/11
        {
            if (e.CommandName == "AddToTable")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = SelectGridView.Rows[index];

                TextBox t = ((TextBox)row.Cells[6].FindControl("TextBox1"));
                int num;
                if (!(int.TryParse(t.Text.ToString(), out num)))
                {
                    row.ForeColor = Color.Red;
                    //Statement.Text = "You need to enter an integer";
                    this.Message("You need to enter an integer");
                }
                else if (Convert.ToInt32(t.Text) < 1)
                {
                    row.ForeColor = Color.Red;
                    //Statement.Text = "Quantity cannot be zero ";
                    this.Message("Quantity cannot be zero ");
                }
                else
                {
                    //Statement.Text = "";
                    row.ForeColor = Color.Black;
                    string itemName = controller.ConvertName(row.Cells[0].Text);
                    int qty = Convert.ToInt16(((TextBox)row.Cells[6].FindControl("TextBox1")).Text);
                    string supplierName = ((DropDownList)row.Cells[7].FindControl("SupplierDropDownList")).SelectedValue;
                    double price = controller.selectAddtoOrder(itemName, supplierName);
                    double amount = qty * price;


                    userEntity = (User)Session["UserEntity"];
                    ReorderItem r = new ReorderItem();
                    r.ItemName = itemName;
                    r.UserId = userEntity.UserID;
                    r.QtyToOrder = qty;
                    r.SupplierName = supplierName;
                    r.Price = Convert.ToDouble(price);
                    r.Amount = Convert.ToDouble(amount);
                    List<ReorderItem> orderList = new List<ReorderItem>();
                    int Index = 0;
                    if (Session["orderList"] != null)
                    {
                        orderList = (List<ReorderItem>)Session["orderList"];
                        bool flag = false;

                        for (int i = 0; i < orderList.Count; i++)
                        {
                            if (orderList[i].ItemName.Equals(itemName) && orderList[i].SupplierName.Equals(supplierName))
                            {
                                flag = true;
                                Index = i;
                            }
                        }
                        if (flag == false)
                        {
                            orderList.Add(r);
                        }
                        else
                        {
                            ReorderItem ri = orderList[Index];
                            ri.QtyToOrder = ri.QtyToOrder + qty;
                            ri.Amount = (ri.QtyToOrder + qty) * price;
                        }
                    }
                    else
                    {
                        orderList.Add(r);
                    }
                    Session["orderList"] = orderList;

                    UpdateGridView.DataSource = orderList;
                    UpdateGridView.DataBind();
                    SubmitBtn.Visible = true;
                }
                
            }
        }

        //private void AddNewRowToGrid(GridViewRow row)                                                                //modify in 3/11  just delete this method
        //{
        //    if (ViewState["CurrentTable"] != null)
        //    {
        //        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        //        string itemName = controller.ConvertName(row.Cells[1].Text);
        //        int qty = Convert.ToInt16(((TextBox)row.Cells[6].FindControl("TextBox1")).Text);
        //        string supplierName = ((DropDownList)row.Cells[7].FindControl("SupplierDropDownList")).SelectedValue;
        //        double price = controller.selectAddtoOrder(itemName, supplierName);
        //        bool flag = false;
        //        int index = 0;
                
        //        for(int i =0; i < dtCurrentTable.Rows.Count; i++)
        //        {
        //            if (dtCurrentTable.Rows[i]["ItemDescription"].Equals(itemName) && dtCurrentTable.Rows[i]["Supplier"].Equals(supplierName))
        //            {
        //                flag = true;
        //                index = i;
        //            }
        //        }
        //        if (flag == false)
        //        {
        //            DataRow dr = dtCurrentTable.NewRow();
        //            dr["ItemDescription"] = itemName;
        //            dr["Qty"] = qty;
        //            dr["Supplier"] = supplierName;
        //            dr["Price"] = price; 
        //            dr["Amount"] = Convert.ToString(qty * price);
        //            dtCurrentTable.Rows.Add(dr);   
        //        }
        //        else
        //        {
        //            int q = Convert.ToInt32(dtCurrentTable.Rows[index]["Qty"]);
        //            dtCurrentTable.Rows[index]["Qty"] = Convert.ToString(q + qty);
        //            dtCurrentTable.Rows[index]["Amount"] = Convert.ToString((q + qty) * price);
        //        }

        //        ViewState["CurrentTable"] = dtCurrentTable;
        //        dtCurrentTable.AcceptChanges();
        //        UpdateGridView.DataSource = dtCurrentTable;
        //        UpdateGridView.DataBind();

        //    }
        //}

        protected void DetailBtn_Click(object sender, EventArgs e)      
        {
            if (SelectGridView.SelectedRow != null)
            {
                GridViewRow row = (GridViewRow)SelectGridView.SelectedRow;
                string itemName = controller.ConvertName(row.Cells[1].Text);

                //string script = "alert(\"Hello!\");";
                //ScriptManager.RegisterStartupScript(this, GetType(),
                //                      "ServerControlScript", script, true);

                string URL = "DetailWindow.aspx?itemName=" + itemName;
                ClientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script>openNewWin('" + URL + "')</script>");
            }
        }

        protected void SupplierDropDownList_SelectedIndexChanged(object sender, EventArgs e)       
        {
            DropDownList SupplierDropDownList = (DropDownList)sender;
            GridViewRow row = (GridViewRow)SupplierDropDownList.NamingContainer;

            string itemName = row.Cells[0].Text;
            string supplierName = SupplierDropDownList.SelectedValue;
            double p = controller.selectAddtoOrder(itemName, supplierName);
            Label lblPrice = (Label)row.FindControl("lblPrice");
            lblPrice.Text = p.ToString();
            if (((TextBox)row.Cells[6].FindControl("TextBox1")).Text != "")
            {
                int balance = Convert.ToInt16(((TextBox)row.Cells[6].FindControl("TextBox1")).Text);
                Label lblAmount = (Label)row.FindControl("lblAmount");
                lblAmount.Text = balance.ToString();
            }
            //row.Cells[8].Text = Convert.ToString(p);
            //if (((TextBox)row.Cells[6].FindControl("TextBox1")).Text != "")
            //{
            //    int balance = Convert.ToInt16(((TextBox)row.Cells[6].FindControl("TextBox1")).Text);
            //    row.Cells[9].Text = Convert.ToString(p * balance);
            //}
        }

        protected void UpdateGridview_RowDeleting(object sender, GridViewDeleteEventArgs e)         // modify in 3/11
        {
            //if (ViewState["CurrentTable"] != null)
            //{
            //    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            //    DataRow dr = dtCurrentTable.Rows[e.RowIndex];
            //    dr.Delete();

            //    dtCurrentTable.AcceptChanges();  
            //    UpdateGridView.DataSource = dtCurrentTable;
            //    UpdateGridView.DataBind();
            //}
            if (Session["orderList"] != null)
            {
                List<ReorderItem> orderList = (List<ReorderItem>)Session["orderList"];
                ReorderItem r = orderList[e.RowIndex];
                orderList.Remove(r);
                UpdateGridView.DataSource = orderList;
                UpdateGridView.DataBind();
            }
            
        }

        protected void SubmitBtn_Click(object sender, EventArgs e)                           // modify in 3/11
        {
            if (Session["orderList"] != null)
            {
                List<ReorderItem> orderList = (List<ReorderItem>)Session["orderList"];
                for (int i = 0; i < orderList.Count; i++)
                {
                    ReorderItem r = orderList[i];
                    controller.createReorderItem(r);
                }

                Session["orderList"] = null;
                UpdateGridView.DataSource = null;
                UpdateGridView.DataBind();
            }

            string subject = "Purchase Order Raised";

            string bodyStart = "<HTML>"
                          + "<HEAD>"
                          + "</HEAD>"
                          + "<BODY>"
                          + "<BR/>"
                          + "<P>Dear ";

            string body = "";

            UserFacade userFacade = new UserFacade();
            List<User> userCollection = userFacade.getUsersWithRole("storeManager");

            body = ",</P><BR/><P>A purchase order has been raised.</P>"
                + "<BR/>"
                + "<a href=\"http://10.10.1.155/SSISWebApplication/WebPages/ApproveRejectPurchaseOrder\">Please click this link to view it</a>"; //TODO LINK

            body = body
            + "<BR/>"
            + "<P>From,</P>"
            + "<P>SSIS.</P>"
            + "</BODY>"
            + "</HTML>";

            NotifyUserController notifyUserController = new NotifyUserController();

            foreach (User user in userCollection)
            {
                notifyUserController.sendEmail(user.Email, subject, bodyStart + user.UserName + body);
            }
        }


        //Show message
        public void Message(string msg)
        {
            dvAlert.Visible = true;
            lblErrorMessage.Text = msg;
        }
    }
}