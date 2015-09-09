using ClassLibrary.Entities;
using ClassLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.EntityFacade
{
    public class DepartmentFacade
    {
        SSISDBEntities ctx;
        ErrorLog errorobj;

        public DepartmentFacade()
        {
            ctx = new SSISDBEntities();
            errorobj = new ErrorLog();
        }

        public List<CollectionPoint> getCollectionPoints()
        {
            List<CollectionPoint> collectionPointCollection;
            try
            {
                collectionPointCollection = new List<CollectionPoint>();

                var cPList = from c in ctx.collectionPoints
                             select c;
     
                foreach(var c in cPList)
                {
                    CollectionPoint collectionPoint = new CollectionPoint();
                    collectionPoint.CollectionPointId = c.collectionPointId;
                    collectionPoint.Address = c.address;
                    collectionPoint.Time = c.time.ToString() ;

                    collectionPointCollection.Add(collectionPoint);
                }
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("DepartmentFacade-getCollectionPoints():::" + exception.ToString());
                collectionPointCollection = new List<CollectionPoint>();
            }
            return collectionPointCollection;
        }

        public List<Department> getDepartments()
        {
            List<Department> departmentCollection = new List<Department>();

            try
            {
                var depC = from u in ctx.users
                           where
                             u.role.name == "departmentRepresentative" &&
                             u.departmentId!="IVS" &&
                             u.status == "Active"
                           select new
                           {
                               depId = u.department.departmentId,
                               collPointId = u.department.collectionPoint.collectionPointId,
                               collPoint = u.department.collectionPoint.address,
                               repName = u.name,
                               depName = u.department.name
                           };

                foreach(var d in depC)
                {
                    Department department = new Department();
                    department.DepartmentId = d.depId;
                    department.CollectionPointId = d.collPointId;
                    department.CollectionPointName = d.collPoint;
                    department.RepresentativeName = d.repName;
                    department.DepartmentName = d.depName;
                    departmentCollection.Add(department);
                }


            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("DepartmentFacade-getDepartments():::" + exception.ToString());
                departmentCollection = new List<Department>();
            }

            return departmentCollection;
        }

        public void updateCollectionPoint(string departmentId, string collectionPointId)
        {
            try
            {
                department dep = (from d in ctx.departments
                                  where d.departmentId == departmentId
                                  select d).FirstOrDefault();

                dep.collectionPointId = collectionPointId;

                ctx.SaveChanges();

            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("DepartmentFacade-updateCollectionPoint():::" + exception.ToString());
            }
        }

        public string getCollectionPoint(string departmentId)
        {
            string collectionPointId;
            try
            {
                collectionPointId = (from d in ctx.departments
                                     where d.departmentId == departmentId
                                     select d.collectionPointId).FirstOrDefault();
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("DepartmentFacade-getCollectionPoint():::" + exception.ToString());
                collectionPointId = "";
            }
            return collectionPointId;
        }


        #region Lingna's Part

        public List<department> getDepartments_Lingna()
        {
            var list = from o in ctx.departments
                       where o.departmentId!="IVS"
                       select o;
            return list.ToList();
        }

        public department getDepartment_Lingna(string depName)
        {
            var dep = from o in ctx.departments
                      where o.name == depName
                      select o;
            return dep.FirstOrDefault();
        }

        #endregion



        #region Pyae Pyae's Part

        public List<department> getDepartment_PyaePyae()
        {
            var departmentData = from x in ctx.departments
                                 where x.departmentId != "IVS"
                                 select x;
            return departmentData.ToList();
        }

        #endregion

    }
}
