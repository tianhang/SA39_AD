using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Entities;

namespace ClassLibrary.GaoFan
{
    public class departmentGF
    {
        SSISDBEntities ctx = new SSISDBEntities();
        public void updateDepartmentCollectionGF(Department dept) {
            department d = ctx.departments.FirstOrDefault( o => o.departmentId == dept.DepartmentId);
            d.collectionPointId = dept.CollectionPointId;
            ctx.SaveChanges();
        }

        public Department getDepartment(string departmentId)
        {
            Department part = new Department();
            var depart = ctx.departments.FirstOrDefault(x => x.departmentId == departmentId);
            string pointName = depart.collectionPoint.address;

            part.CollectionPointId = depart.collectionPointId;
            part.CollectionPointName = pointName;
            part.DepartmentId = depart.departmentId;
            part.DepartmentName = depart.name;
            part.ContactNo = depart.contactName;
            part.PhoneNo = (long)depart.phoneNo;
            part.FaxNo = (long)depart.faxNo;            

            return part;
        }

    }
}
