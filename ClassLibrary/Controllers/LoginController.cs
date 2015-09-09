using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.EntityFacade;
using ClassLibrary.Entities;

namespace ClassLibrary.Controllers
{    
    public class LoginController
    {
        LoginFacade loginFaca;
        public LoginController()
        {
            loginFaca = new LoginFacade();
        }

        public bool checkAuthentication(string email,string password)
        {
            return loginFaca.checkLogIn(email, password);
        }
    }
}
