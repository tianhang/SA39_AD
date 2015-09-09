using ClassLibrary.Entities;
using ClassLibrary.EntityFacade;
using ClassLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Controllers
{
    public class AssignRepresentativeController
    {
        UserFacade userFacade;
        ErrorLog errorobj;
        NotifyUserController notifyUserController;
        public AssignRepresentativeController()
        {
            userFacade = new UserFacade();
            errorobj = new ErrorLog();
        }
        
        public List<User> loadController(string departmentId)
        {
            List<User> userCollection = userFacade.getUsers(departmentId);
            return userCollection;
        }

        public void selectEmployee(string oldEmployeeId, string newEmployeeId,string departmentName)
        {
            try
            {
                role userRole = userFacade.getUserRoles("departmentRepresentative");
                userFacade.updateUserRole(newEmployeeId, userRole.roleId, false);

                userRole = userFacade.getUserRoles("departmentEmployee");
                userFacade.updateUserRole(oldEmployeeId, userRole.roleId, false);
                string subject = "Notification for change of representative";

                string bodyStart = "<HTML>"
                              + "<HEAD>"
                              + "</HEAD>"
                              + "<BODY>"
                              + "<BR/>"
                              + "<P>Dear ";

                string body="";
                    List<User> userCollection = userFacade.getUsersWithRole("storeClerk");

                    body = ",</P><BR/><P>The representative of the " + departmentName + "department has been changed. </P>";

                    body = body
                    + "<BR/>"
                    + "<P>From,</P>"
                    + "<P>SSIS.</P>"
                    + "</BODY>"
                    + "</HTML>";

                    notifyUserController = new NotifyUserController();

                    foreach (User user in userCollection)
                    {
                        notifyUserController.sendEmail(user.Email, subject, bodyStart + user.UserName + body);
                    }


            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("AssignRepresentativeController-selectEmployee():::" + exception.ToString());
            }
        }

    }
}
