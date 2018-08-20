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
        private WebBankingEntities1 db = new WebBankingEntities1();
        

        // GET: Clientes
        public ActionResult Index()
        {
            var cliente = db.Cliente.Include(c => c.Cuenta);
            return View(cliente.ToList());
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
            ViewBag.idCliente = new SelectList(db.Cuenta, "idCuenta", "propietario");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCliente,nombreCliente,apellidos,identificacion,estado,email,direccion,telefono,usuarioCliente,contrasenaCliente")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCliente = new SelectList(db.Cuenta, "idCuenta", "propietario", cliente.idCliente);
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
            ViewBag.idCliente = new SelectList(db.Cuenta, "idCuenta", "propietario", cliente.idCliente);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCliente, nombreCliente, apellidos, identificacion, estado, email, direccion, telefono, usuarioCliente, contrasenaCliente")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {

                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.Cuenta, "idCuenta", "propietario", cliente.idCliente);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult InactivarEstado(int? id)
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
            ViewBag.idCliente = new SelectList(db.Cuenta, "idCuenta", "propietario", cliente.idCliente);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InactivarEstado([Bind(Include = "idCliente, nombreCliente, apellidos, identificacion, estado, email, direccion, telefono, usuarioCliente, contrasenaCliente")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(cliente.estado).State = EntityState.Modified;
                db.SaveChanges();  
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.Cuenta, "idCuenta", "propietario", cliente.idCliente);
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
