using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;



namespace WebApplication1.Controllers
{
    public class ClientesController : Controller
    {
        private WebBankingEntities16 db = new WebBankingEntities16();

        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.Cliente.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
           
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCliente,nombreCliente,usuario,identificacion,email,tipoUsuario,contrasena,estado")] Cliente cliente)
        {
            Cliente obj = new Cliente();
            obj.email = cliente.email;
           // cliente.EnviarCorreoElectronicos(obj.email);
            
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCliente,nombreCliente,usuario,identificacion,email,contrasena")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }


        public ActionResult InactivarCliente(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InactivarCliente([Bind(Include = "idCliente,nombreCliente,usuario,identificacion,email,tipoUsuario,contrasena,estado")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                Cliente obj = new Cliente();
                if (cliente.tipoUsuario.Equals("Usuario")) { 
                    obj.nombreCliente = cliente.nombreCliente;
                    db.Entry(cliente).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
      //  public ActionResult ListadoClientes()
        //{


            //  ClienteModel modelo = new ClienteModel();
            //  modelo.cliente = new ConsultaCliente().ListadoClientes().ToList();
            //  modelo.Cuenta = modelo.CXC.idCuenta; ;
          /*  Cliente lisCliente = new Cliente();
            lisCliente.listaCliente = (from clie in db.Cliente
                                       join CXC in db.CuentaPorCliente
                                       on clie.idCliente equals CXC.idCliente
                                       join cuen in db.Cuenta
                                       on CXC.idCuenta equals cuen.idCuenta
                                       where (clie.estado.Equals("Activo"))

                                       select new Cliente{ nombreCliente = clie.nombreCliente, numCuenta = cuen.numCuenta, estado = clie.estado }).ToList();


            return View(lisCliente);*/
       // }
        [HttpPost]
        public ActionResult AddOrEdit(Cliente clienteModel)
        {
            using (WebBankingEntities16 bModel = new WebBankingEntities16()) 
            return View(clienteModel);
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
