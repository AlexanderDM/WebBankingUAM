using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Datos;

namespace WebApplication1.Models
{
    public class ConsultaCliente
    {
        public WebBankingEntities6 contexto = new WebBankingEntities6();
    

    public Cliente ListadoClientes()
    {
        try
        {
            var sql = from clie in contexto.Cliente
                      join cuen in contexto.Cuenta on clie.idCliente equals cuen.propietarioCuenta
                       
                       select clie ;
                return sql.FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }
        return null;
    }
    }
}