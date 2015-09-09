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
    public class ChangeCollectionPointController
    {
        DepartmentFacade departmentFacade;
        ErrorLog errorobj;
        UserFacade userFacade;
        NotifyUserController notifyUserController;

        public ChangeCollectionPointController()
        {
            departmentFacade = new DepartmentFacade();
            errorobj = new ErrorLog();
        }

        public List<CollectionPoint> loadCollectionPointList()
        {
            return departmentFacade.getCollectionPoints();
        }

        public List<Department> loadDepartmentList()
        {
            return departmentFacade.getDepartments();
        }

        public string selectCollectionPoint(string departmentId)
        {
            return departmentFacade.getCollectionPoint(departmentId);
        }

        public void selectSubmit(string departmentId, string departmentName, CollectionPoint collectionPoint, bool representative)
        {
            try
            {
                departmentFacade.updateCollectionPoint(departmentId, collectionPoint.CollectionPointId);

                userFacade = new UserFacade();

                

                string subject = "Notification for change of collection point";

                string bodyStart = "<HTML>"
                              + "<HEAD>"
                              + "</HEAD>"
                              + "<BODY>"
                              + "<BR/>"
                              + "<P>Dear ";

                string body ;

                

                if (representative == true)
                {
                    List<User> userCollection = userFacade.getUsersWithRole("storeClerk");

                    body = ",</P><BR/><P>The collection point of the " + departmentName + "department has been changed to " + collectionPoint.Address + " </P>";

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
                else
                {
                    body = ",</P><BR/><P>The collection point of your department has been changed to " + collectionPoint.Address + " </P>";

                    body = body
                    + "<BR/>"
                    + "<P>From,</P>"
                    + "<P>SSIS.</P>"
                    + "</BODY>"
                    + "</HTML>";

                    List<User> userCollection = userFacade.getUsersWithRole("departmentRepresentative",departmentId);

                    notifyUserController = new NotifyUserController();

                    foreach (User user in userCollection)
                    {
                        notifyUserController.sendEmail(user.Email, subject, bodyStart + user.UserName + body);
                    }
                }
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("ChangeCollectionPointController-selectSubmit():::" + exception.ToString());
            }
            

        }

    }
}
