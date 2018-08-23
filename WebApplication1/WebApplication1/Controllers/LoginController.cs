using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autorizar(Cliente userModel)
        {
            using (WebBankingEntities15 db = new WebBankingEntities15())
            {
                var UserDetails = db.Cliente.Where(x => x.usuario == userModel.usuario && x.contrasena == userModel.contrasena).FirstOrDefault();

                if (UserDetails == null)
                {
                    userModel.loginMensajeError = "usuario o contraseña incorrecta";
                    return View("Index", userModel);
                }
                else
                {
                  /*  bool isAdmin = userModel.ValidarAdministrador().esAdmnin;*/
                    
                     
                       

                 
                    Session["idCliente"] = UserDetails.idCliente;
                    Session["usuario"] = UserDetails.usuario;
                    return RedirectToAction("Index", "Home");
                }
            }
                
        }


        public ActionResult LogOut()
        {
            int userId = (int)Session["idCliente"];

            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}