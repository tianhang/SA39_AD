using ClassLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Controllers
{
    public class NotifyUserController
    {
        ErrorLog errorobj;
        MailMessage message;

        public NotifyUserController ()
	    {
            errorobj = new ErrorLog();
            message = new MailMessage();
	    }

        public int sendEmail(string recipient,string subject,string body)
        {
            try
            {
                message = new MailMessage();
                message.Subject = subject;
                message.Sender = new MailAddress("issadprojectteam1@gmail.com");
                message.From = new MailAddress("issadprojectteam1@gmail.com");
                message.Body = body;
                message.IsBodyHtml = true; 

                message.To.Add(new MailAddress(recipient));

                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com"; //SMTP server for GMail.
                client.Port = 25;
                client.EnableSsl = true; //SSL connection is required to be true.
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                System.Net.NetworkCredential credential = new System.Net.NetworkCredential("issadprojectteam1@gmail.com", "Iss1234#");
                client.Credentials = credential;
                client.Send(message); //Sending message process.

                return 1;
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("SendEmail-sendmail():::" + exception.ToString());
                return -1;
            }
        }
    }
}
