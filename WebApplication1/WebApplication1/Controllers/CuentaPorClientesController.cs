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
    public class CuentaPorClientesController : Controller
    {
        private WebBankingEntities16 db = new WebBankingEntities16();

        // GET: CuentaPorClientes
        public ActionResult Index()
        {
            var cuentaPorCliente = db.CuentaPorCliente.Include(c => c.Cliente1).Include(c => c.Cuenta1);
            return View(cuentaPorCliente.ToList());
        }

        // GET: CuentaPorClientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaPorCliente cuentaPorCliente = db.CuentaPorCliente.Find(id);
            if (cuentaPorCliente == null)
            {
                return HttpNotFound();
            }
            return View(cuentaPorCliente);
        }

        // GET: CuentaPorClientes/Create
        public ActionResult Create()
        {
            ViewBag.cliente = new SelectList(db.Cliente, "idCliente", "nombreCliente");
            ViewBag.cuenta = new SelectList(db.Cuenta, "idCuenta", "numCuenta");
            return View();
        }

        // POST: CuentaPorClientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCliente,idCuenta,cliente,cuenta")] CuentaPorCliente cuentaPorCliente)
        {
            if (ModelState.IsValid)
            {
                db.CuentaPorCliente.Add(cuentaPorCliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cliente = new SelectList(db.Cliente, "idCliente", "nombreCliente", cuentaPorCliente.cliente);
            ViewBag.cuenta = new SelectList(db.Cuenta, "idCuenta", "numCuenta", cuentaPorCliente.cuenta);
            return View(cuentaPorCliente);
        }

        // GET: CuentaPorClientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaPorCliente cuentaPorCliente = db.CuentaPorCliente.Find(id);
            if (cuentaPorCliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.cliente = new SelectList(db.Cliente, "idCliente", "nombreCliente", cuentaPorCliente.cliente);
            ViewBag.cuenta = new SelectList(db.Cuenta, "idCuenta", "numCuenta", cuentaPorCliente.cuenta);
            return View(cuentaPorCliente);
        }

        // POST: CuentaPorClientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCliente,idCuenta,cliente,cuenta")] CuentaPorCliente cuentaPorCliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuentaPorCliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cliente = new SelectList(db.Cliente, "idCliente", "nombreCliente", cuentaPorCliente.cliente);
            ViewBag.cuenta = new SelectList(db.Cuenta, "idCuenta", "numCuenta", cuentaPorCliente.cuenta);
            return View(cuentaPorCliente);
        }

        // GET: CuentaPorClientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaPorCliente cuentaPorCliente = db.CuentaPorCliente.Find(id);
            if (cuentaPorCliente == null)
            {
                return HttpNotFound();
            }
            return View(cuentaPorCliente);
        }

        // POST: CuentaPorClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CuentaPorCliente cuentaPorCliente = db.CuentaPorCliente.Find(id);
            db.CuentaPorCliente.Remove(cuentaPorCliente);
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
