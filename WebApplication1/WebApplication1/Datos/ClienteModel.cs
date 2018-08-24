using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Datos
{
    public class ClienteModel
    {
        public Cliente Cliente { get; set; }

        public Cuenta C { get; set; }
    }
}