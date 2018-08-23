using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Datos;

namespace WebApplication1.Models
{
    public class ConsultaCliente
    {
        public WebBankingEntities15 contexto = new WebBankingEntities15();
    

    /*public ClienteBanco ListadoClientes()
    {
        try
        {
            var sql = from clie in contexto.ClienteBanco
                      join cuen in contexto.Cuenta on clie.idCliente
                       
                       select clie ;
                return sql.FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }
        return null;
    }*/
    }
}