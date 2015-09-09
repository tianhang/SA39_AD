using ClassLibrary.Entities;
using ClassLibrary.EntityFacade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Controllers
{
    public class ManageDeputyController
    {
        UserFacade userFacade;
        public ManageDeputyController()
        {
            userFacade = new UserFacade();
        }

        public List<User> loadController(string departmentId)
        {
            return userFacade.getUsersWithRole("departmentDeputy", departmentId);
        }

        public List<User> selectEmployee(string departmentId)
        {
            return userFacade.getUsersWithRole("departmentEmployee", departmentId);
        }


        public void selectRemove(string userId)
        {
            role role = userFacade.getUserRoles("departmentEmployee");
            userFacade.updateUserRole(userId, role.roleId, true);
        }

        public void selectAdd(string userId, DateTime startDate, DateTime endDate)
        {
            commonMethod(userId, startDate, endDate, "departmentDeputy");
        }

        public void selectUpdate(string userId,DateTime startDate, DateTime endDate)
        {
            commonMethod(userId, startDate, endDate, "departmentDeputy ");
        }

        private void commonMethod(string userId, DateTime startDate, DateTime endDate,string roleName)
        {
            role role = userFacade.getUserRoles(roleName);
            userFacade.createDeputy(userId, startDate, endDate, role.roleId);
        }

    }
}
