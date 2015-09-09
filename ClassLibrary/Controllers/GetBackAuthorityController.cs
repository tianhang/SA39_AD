using ClassLibrary.Entities;
using ClassLibrary.EntityFacade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Controllers
{
    public class GetBackAuthorityController
    {
        UserFacade userFacade;
        public GetBackAuthorityController()
        {
            userFacade = new UserFacade();
        }

        public void run()
        {
            List<User> userCollection = userFacade.getUsersWithRole("departmentDeputy");

            if (userCollection.Count != 0)
            {
                role userRole = userFacade.getUserRoles("departmentEmployee");

                foreach (User user in userCollection)
                {
                    DateTime dt = DateTime.Now;
                    dt.AddDays(-1);
                    if (user.EndDate.Date == dt)
                    {
                        userFacade.updateUserRole(user.UserID, userRole.roleId, true);
                    }
                }
            }
        }
    }
}
