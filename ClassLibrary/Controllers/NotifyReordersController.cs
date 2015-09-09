using ClassLibrary.Entities;
using ClassLibrary.EntityFacade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Controllers
{
    public class NotifyReordersController
    {
        CatalogueFacade catalogueFacade;
        UserFacade userFacade;
        NotifyUserController notifyUserController;
        public NotifyReordersController()
        {
            catalogueFacade = new CatalogueFacade();
        }

        public void run()
        {
            List<Item> itemCollection = catalogueFacade.getItemsForReorder("Active");

            if (itemCollection.Count != 0)
            {

                List<string> itemList = new List<string>();

                string subject = "Notification for items below reorder level";

                string bodyStart = "<HTML>"
                              + "<HEAD>"
                              + "</HEAD>"
                              + "<BODY>"
                              +"<BR/>"
                              +"<P>Dear ";

                string body = ",</P><BR/><P>The following items have fallen below reorder level :- </P>"
                              + "<UL>";

                foreach (Item item in itemCollection)
                {
                    body = body + "<LI>" + item.ItemId + "    " + item.Description + "</LI>";
                }

                body = body + "</UL>"
                    + "<BR/>"
                    + "<a href=\"http://10.10.1.155/SSISWebApplication/WebPages/PurchaseOrder/RaisePurchaseOrder\">Click this link to reorder</a>"//TODO LINK
                    + "<BR/>"
                    + "<P>From,</P>"
                    + "<P>SSIS.</P>"
                    + "</BODY>"
                    + "</HTML>";

                List<User> userCollection = new List<User>();
                userFacade = new UserFacade();

                userCollection = userFacade.getUsersWithRole("storeClerk");

                notifyUserController = new NotifyUserController();
                
                foreach(User user in userCollection)
                {
                    notifyUserController.sendEmail(user.Email,subject,bodyStart+user.UserName+body);
                }

            }

        }
    }
}
