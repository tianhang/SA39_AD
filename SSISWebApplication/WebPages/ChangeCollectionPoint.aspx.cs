using ClassLibrary.Controllers;
using ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISWebApplication.WebPages
{
    public partial class ChangeCollectionPoint : System.Web.UI.Page
    {
        ChangeCollectionPointController changeCollectionPointController;
        List<CollectionPoint> collectionPointCollection;
        List<Department> departmentCollection;
        User userEntity;
        string colId;

        protected void Page_Load(object sender, EventArgs e)
        {
            changeCollectionPointController = new ChangeCollectionPointController();

            userEntity = (User)Session["UserEntity"];

            if(!IsPostBack)
            {
                
                collectionPointCollection = changeCollectionPointController.loadCollectionPointList();
                Session["collectionPointCollection"] = collectionPointCollection;



                string userRole = userEntity.RoleName;
                if (userRole == "storeClerk")
                {
                    departmentCollection = changeCollectionPointController.loadDepartmentList();
                    Session["departmentCollection"] = departmentCollection;
                    var de = departmentCollection.Select(d => new { d.DepartmentName }).ToList();
                    List<string> depNameString = new List<string>();
                    foreach (var d in de)
                    {
                        depNameString.Add(d.DepartmentName);
                    }

                    DepartmentDropDownList.DataSource = depNameString.ToList();
                    DepartmentDropDownList.DataBind();
                    DepartmentDropDownList.Visible = true;
                }
                else
                {
                    colId = changeCollectionPointController.selectCollectionPoint(userEntity.DepartmentId);
                    Session["colId"] = colId;
                    DepartmentDropDownList.Visible = false;
                }


                CollectionPointGridView.ForeColor = Color.Black;
                CollectionPointGridView.DataSource = collectionPointCollection;
                CollectionPointGridView.DataBind();


            }
            else
            {
                departmentCollection = (List<Department>)Session["departmentCollection"];
                collectionPointCollection = (List<CollectionPoint>)Session["collectionPointCollection"];
            }
        }

        protected void CollectionPointGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            string userRole = userEntity.RoleName;
            if (userRole == "storeClerk")
            {
                colId = (from d in departmentCollection
                         where d.DepartmentName == DepartmentDropDownList.SelectedValue
                         select d.CollectionPointId).FirstOrDefault();
            }
            else
            {
                colId = Session["colId"].ToString();
            }

            
            if(e.Row.Cells[0].Text.ToString() == colId)
            {
                e.Row.ForeColor = Color.Blue;
                Button b = (Button)e.Row.Cells[3].Controls[0];
                b.Text = "Chosen";
                b.Enabled = false;
            }

        }

        protected void DepartmentDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CollectionPointGridView.DataSource = collectionPointCollection;
            CollectionPointGridView.DataBind();
        }

        protected void CollectionPointGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Select")
            {

                
                CollectionPoint collectionPoint = collectionPointCollection[Convert.ToInt32(e.CommandArgument)];

                string userRole = userEntity.RoleName;
                if (userRole == "storeClerk")
                {
                    Department department = departmentCollection.Find(d => d.DepartmentName == DepartmentDropDownList.SelectedValue);
                    changeCollectionPointController.selectSubmit(department.DepartmentId, department.DepartmentName, collectionPoint, false);
                   
                }
                else
                {
                    changeCollectionPointController.selectSubmit(userEntity.DepartmentId, userEntity.DepartmentName, collectionPoint, true);
                }

                Response.Redirect(Request.RawUrl);
            }
        }


    }
}