using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult AgregarCliente()
        {
            Cliente clie = new Cliente();

            clie.nombreCliente = "viviana Fernandez";
            clie.identificacion = "16545987";
            clie.numCuenta = "20002010012351";
            clie.estado = "Activo";
            clie.email = "Viviana.fernandez@gmail.com";
            clie.direccion = "San José,Guadalupe";
            clie.telefono = "86596378";
            return View(clie) ;
        }
    }
}