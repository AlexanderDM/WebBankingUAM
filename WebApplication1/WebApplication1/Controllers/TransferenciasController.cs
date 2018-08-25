using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TransferenciasController : Controller
    {
        private WebBankingEntities17 db = new WebBankingEntities17();
        public CuentaPorCliente CXC = new CuentaPorCliente();

        public ActionResult Index()
        {
            var transferencia = db.Transferencia.Include(t => t.Cliente).Include(t => t.Cuenta);
            return View(transferencia.ToList());



        }

        // GET: Transferencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transferencia transferencia = db.Transferencia.Find(id);
            if (transferencia == null)
            {
                return HttpNotFound();
            }
            return View(transferencia);
        }

        // GET: Transferencias/Create
        public ActionResult Create()
        {

            ViewBag.idcliente = new SelectList(db.Cliente, "idCliente", "nombreCliente");
            int i = (Int32)Session["idCliente"];


            var sql = from clie in db.Cliente
                      join CXC in db.CuentaPorCliente
                      on clie.idCliente equals CXC.idCliente
                      join cuen in db.Cuenta
                      on CXC.idCuenta equals cuen.idCuenta
                      where (clie.idCliente == (i))
                      select new
                      {
                          Numcuenta = cuen.numCuenta,
                          ciente = clie.idCliente,
                          idcuenta = cuen.idCuenta
                      };

            ViewBag.idcuentaOrigen = new SelectList(sql, "idCuenta", "numCuenta");
            return View();
        }

        // POST: Transferencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        public void GetListadoClientesCuentas(Cliente clie)
        {
            try
            {
                IEnumerable<Cliente> values = Enum.GetValues(typeof(Cliente))
                    .Cast<Cliente>();

                IEnumerable<SelectListItem> items =
                    from value in values
                    select new SelectListItem
                    {
                        Text = value.ToString(),

                        Value = value.ToString(),

                        Selected = value == clie,
                    };
                ViewBag.Cuentas = items;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Nombre, [Bind(Include = "idTransferencia,idcliente,idcuentaOrigen,cuentaDestino,fechaHora,monto,detalle")] Transferencia transferencia)
        {
            Transferencia obj = new Transferencia();

            String format = "dd mm yyyy hh:mm tt";
            //var dat = new DateTime.Now;
            DateTime date = DateTime.Now;
            // Display the result string. 
            Console.WriteLine(date.ToString(format));
            int i = (Int32)Session["idCliente"];

            obj.idcliente = i;
            obj.idcuentaOrigen = transferencia.idcuentaOrigen;
            obj.cuentaDestino= transferencia.cuentaDestino;
            transferencia.fechaHora = date;
            obj.fechaHora = transferencia.fechaHora;
            obj.monto = transferencia.monto;
            obj.detalle = transferencia.detalle;

            int valor;
            int.TryParse(transferencia.cuentaDestino, out valor);
            //obj.ActualizarCuentas(i, transferencia.idcuentaOrigen, valor, transferencia.monto);

            if (ModelState.IsValid)
            {

                db.Transferencia.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            


            var sql = from clie in db.Cliente
                      join CXC in db.CuentaPorCliente
                      on clie.idCliente equals CXC.idCliente
                      join cuen in db.Cuenta
                      on CXC.idCuenta equals cuen.idCuenta
                      where (clie.idCliente == (i))
                      select new
                      {
                          Numcuenta = cuen.numCuenta,
                          ciente = clie.idCliente,
                          idcuenta = cuen.idCuenta
                      };
            string dir = Session["email"].ToString();
            obj.EnviarCorreoElectronicos(dir, transferencia.idcuentaOrigen.ToString(), transferencia.cuentaDestino, transferencia.monto, transferencia.detalle, 1);
            obj.EnviarCorreoElectronicos(dir, transferencia.idcuentaOrigen.ToString(), transferencia.cuentaDestino, transferencia.monto, transferencia.detalle, 2);                                

            ViewBag.idcuentaOrigen = new SelectList(sql, "idCuenta", "numCuenta");


            ViewBag.idcliente = new SelectList(db.Cliente, "idCliente", "nombreCliente", transferencia.idcliente);
            // ViewBag.idcuentaOrigen = new SelectList(db.Cuenta, "idCuenta", "numCuenta", transferencia.idcuentaOrigen);
            //ViewBag.idcuentaOrigen = ListadoClientesCuentas(transferencia.idcliente);
            // ViewBag.idcuentaOrigen = items;
           
            return View(transferencia);
        }

        

        // GET: Transferencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transferencia transferencia = db.Transferencia.Find(id);
            if (transferencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcliente = new SelectList(db.Cliente, "idCliente", "nombreCliente", transferencia.idcliente);
            ViewBag.idcuentaOrigen = new SelectList(db.Cuenta, "idCuenta", "numCuenta", transferencia.idcuentaOrigen);
            //  ViewBag.idcuentaOrigen = new SelectList(db.CuentaPorCliente.Where(c => c.idCliente == transferencia.idcliente), "idCliente", "numCuenta", transferencia.idcuentaOrigen);
            return View(transferencia);
        }

        // POST: Transferencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTransferencia,idcliente,idcuentaOrigen,cuentaDestino,fechaHora,monto,detalle")] Transferencia transferencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transferencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idcliente = new SelectList(db.Cliente, "idCliente", "nombreCliente", transferencia.idcliente);
            ViewBag.idcuentaOrigen = new SelectList(db.Cuenta, "idCuenta", "numCuenta", transferencia.idcuentaOrigen);
            //   ViewBag.idcuentaOrigen = new SelectList(db.CuentaPorCliente.Where(c => c.idCliente == transferencia.idcliente), "idCliente", "numCuenta", transferencia.idcuentaOrigen);
            return View(transferencia);
        }

        // GET: Transferencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transferencia transferencia = db.Transferencia.Find(id);
            if (transferencia == null)
            {
                return HttpNotFound();
            }
            return View(transferencia);
        }

        // POST: Transferencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transferencia transferencia = db.Transferencia.Find(id);
            db.Transferencia.Remove(transferencia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult listaServicios()
        {
            return View();
        }

        public ActionResult Nince(string identificador)
        {
            Servicio obj = new Servicio();


            return View();
        }
        
     /*  public ActionResult Validar(int identificador)
           
        {

        } */

        public ActionResult ObtenerTipoServicio(String tipo)
        {

            return  RedirectToAction("Nince", "Transferencias") ;
        }
        [HttpPost]
        public ActionResult PagoServicio(string TipoServicio)
        {

            ViewBag.idcliente = new SelectList(db.Cliente, "idCliente", "nombreCliente");
            int i = (Int32)Session["idCliente"];
            var numCuentaCliente = from clie in db.Cliente
                      join CXC in db.CuentaPorCliente
                      on clie.idCliente equals CXC.idCliente
                      join cuen in db.Cuenta
                      on CXC.idCuenta equals cuen.idCuenta
                      where (clie.idCliente == (i))
                      select new
                      {
                          Numcuenta = cuen.numCuenta,
                          ciente = clie.idCliente,
                          idcuenta = cuen.idCuenta
                      };

            var numCuentaServicio = from ser in db.Servicio
                                    join cue in db.Cuenta
                                    on ser.idCuenta equals cue.idCuenta

                                    where (ser.tipoServicio == ("Agua"))
                                    select new
                                    {
                                        Numcuenta = cue.numCuenta,
                                        idcuenta = cue.idCuenta

                                    };

           
            ViewBag.tipoServicio = new SelectList(db.Servicio, "idServicio", "tipoServicio");
            ViewBag.cuentaDestino = new SelectList(numCuentaCliente, "idCuenta", "numCuenta");

            ViewBag.idcuentaOrigen = new SelectList(numCuentaServicio, "idCuenta", "numCuenta");
          
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PagoServicio(string TipoServicio ,[Bind(Include = "idTransferencia,idcliente,idcuentaOrigen,cuentaDestino,fechaHora,monto,detalle")] Transferencia transferencia)
        {

            Servicio obj = new Servicio();
            //var id = obj.ValidarServicio(TipoServicio);
            //var idcuenta= 
                   
               //pago de un servicio es igual a una transferencia             
          //  CuentaPorCliente obj = new CuentaPorCliente();
            //  obj.idcliente =transferencia.ConsultarCuentaCliente(transferencia.idcliente);
            if (ModelState.IsValid)
            {
                db.Transferencia.Add(transferencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          

            ViewBag.idcliente = new SelectList(db.Cliente, "idCliente", "nombreCliente", transferencia.idcliente);
           
            // ViewBag.idcuentaOrigen = new SelectList(db.Cuenta, "idCuenta", "numCuenta", transferencia.idcuentaOrigen);
            //  ViewBag.idcuentaOrigen = new SelectList(db.Cuenta, "idCliente", "numCuenta", transferencia.idcuentaOrigen);
            return View(transferencia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
