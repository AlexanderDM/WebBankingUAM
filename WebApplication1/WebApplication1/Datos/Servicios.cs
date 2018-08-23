using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Datos
{
    public class Servicios
    {
        Servicios servicio {get;set; }
        

        public int ValidarServicio(string Type)
        {
          
            Servicio servicio = new Servicio();
            var cuenta=0;

            if (Type.Equals("AGUA"))
            {
                cuenta = servicio.idCuenta;
            }

            if (Type.Equals(" LUZ"))
            {
                cuenta = servicio.idCuenta;
            }

            if (Type.Equals("TELEFONO"))
            {
                cuenta = servicio.idCuenta;
            }
            return cuenta;
        }

        
    }
}