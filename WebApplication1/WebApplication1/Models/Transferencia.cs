//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Net;  //Librerias para envio de correo electronico
    using System.Net.Mail;
    using System.Threading; //Hilos de ejecucion
    using System.Linq;

    public partial class Transferencia
    {
        public int idTransferencia { get; set; }
        public int idcliente { get; set; }
        public int idcuentaOrigen { get; set; }
        public string cuentaDestino { get; set; }
        public System.DateTime fechaHora { get; set; }
        public double monto { get; set; }
        public string detalle { get; set; }
               
    
        public virtual Cliente Cliente { get; set; }
        public virtual Cuenta Cuenta { get; set; }

        public void ActualizarCuentas(int idCliente, int origen, int destino, double monto) {
            using (WebBankingEntities17 db = new WebBankingEntities17()) {
                
                var numCuentaOrigen = from clie in db.Cliente
                                       join CXC in db.CuentaPorCliente
                                       on clie.idCliente equals CXC.idCliente
                                       join cuen in db.Cuenta
                                       on CXC.idCuenta equals cuen.idCuenta
                                       where (clie.idCliente == (idCliente) && CXC.idCuenta == (origen))
                                       select new
                                       {
                                           Numcuenta = cuen.numCuenta,
                                           ciente = clie.idCliente,
                                           idcuenta = cuen.idCuenta
                                       };
                Cuenta cOrigen = new Cuenta();
                cOrigen = db.Cuenta.Find(numCuentaOrigen);
                cOrigen.saldo = cOrigen.saldo-Convert.ToInt32(monto);

                var numCuentaDestino = from clie in db.Cliente
                                      join CXC in db.CuentaPorCliente
                                      on clie.idCliente equals CXC.idCliente
                                      join cuen in db.Cuenta
                                      on CXC.idCuenta equals cuen.idCuenta
                                      where (clie.idCliente == (idCliente) && CXC.idCuenta == (destino))
                                      select new
                                      {
                                          Numcuenta = cuen.numCuenta,
                                          ciente = clie.idCliente,
                                          idcuenta = cuen.idCuenta
                                      };
                Cuenta cDestino = new Cuenta();
                cDestino = db.Cuenta.Find(numCuentaDestino);
                cDestino.saldo = cDestino.saldo + Convert.ToInt32(monto);
                db.Entry(cOrigen).State = EntityState.Modified;
                db.Entry(cDestino).State = EntityState.Modified;
                db.SaveChanges();
            }
            //User user = new User();

            /*var token = WebSecurity.CreateUserAndAccount(model.UserName, model.Password, null, true);
            user.LastName = model.LastName;
            user.FirstName = model.FirstName;
            user.Age = model.Age;
            user.Sex = model.Sex;
            user.SecretQuestion = model.SecretQuestion;
            user.SecretQuestionAnswer = model.SecretQuestionAnswer;
            db.Users.Add(user);*/
                        
        }

        public void EnviarCorreoElectronicos(string receptor, string origen, string destino, double monto, string detalle, int tipo)        
        //public void EnviarCorreoElectronicos(string receptor)        
        {
            SmtpClient envio = new SmtpClient(); //Configuracion del ENVIO 
            MailMessage msj = new MailMessage(); //Configuracion del MSJ
                        
            try
            {
                String format = "dd mm yyyy hh:mm tt";                
                DateTime date = DateTime.Now;                

                //Informacion del correo a enviar
                msj = new MailMessage();
                msj.To.Add(new MailAddress(receptor));
                msj.From = new MailAddress("enviocorreos2019@gmail.com");
                msj.Subject = "BVAM contacto digital";
                string body = "";
                if (tipo == 1) {
                    body = "<h2>Notificación de transferencia.<h2> <b>Se ha realizado una transferencia a la cuenta N°" + destino + " el día " + date.ToString(format) + " por un monto de " + monto + " por concepto de " + detalle + ". Muchas gracias. </b>"; }
                if(tipo == 2) {
                    body = "<h2>Notificación de transferencia.<h2> <b>Ha recibido una transferencia a la cuenta N°" + destino + " el día " + date.ToString(format) + " por un monto de " + monto + " por concepto de " + detalle + ". Muchas gracias. </b>"; }
                msj.Body = body;
                msj.IsBodyHtml = true;
                msj.Priority = MailPriority.Normal;

                //Configuracion SMTP
                envio.Host = "smtp.gmail.com";
                envio.Port = 587;
                envio.EnableSsl = true;
                envio.UseDefaultCredentials = false;
                envio.Credentials = new NetworkCredential("enviocorreos2019@gmail.com",
                                                            "@UAM123456789");
                //envio
                envio.Send(msj);
                msj.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }
}
