using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {   
            if(string.IsNullOrEmpty(controller.HttpContext.Session.GetString("Username")))
            {
                controller.Request.HttpContext.Response.Redirect("/Usuario/Login");
            }
        }

        public static bool CheckAdmin(Controller controller)
        {   
            if(controller.HttpContext.Session.GetString("Username") == "admin")
            {
                return true;
            } else {
                return false;
            }
        }
    }
}