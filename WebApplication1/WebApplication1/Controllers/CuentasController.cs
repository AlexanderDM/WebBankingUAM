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
    public class CuentasController : Controller
    {
        private WebBankingEntities2 db = new WebBankingEntities2();

        // GET: Cuentas
        public ActionResult Index()
        {
            var cuenta = db.Cuenta.Include(c => c.Banco1).Include(c => c.Cliente);
            return View(cuenta.ToList());
        }

        // GET: Cuentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuenta cuenta = db.Cuenta.Find(id);
            if (cuenta == null)
            {
                return HttpNotFound();
            }
            return View(cuenta);
        }

        // GET: Cuentas/Create
        public ActionResult Create()
        {
            ViewBag.banco = new SelectList(db.Banco, "idBanco", "nombre");
            ViewBag.propietarioCuenta = new SelectList(db.Cliente, "idCliente", "nombreCliente");

            return View();
        }

        // POST: Cuentas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCuenta,numCuenta,banco,propietarioCuenta,saldo,estado,direccion")] Cuenta cuenta)
        {
            if (ModelState.IsValid)
            {
                db.Cuenta.Add(cuenta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.banco = new SelectList(db.Banco, "idBanco", "nombre", cuenta.banco);
            ViewBag.propietarioCuenta = new SelectList(db.Cliente, "idCliente", "nombreCliente", cuenta.propietarioCuenta);
            return View(cuenta);
        }

        // GET: Cuentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuenta cuenta = db.Cuenta.Find(id);
            if (cuenta == null)
            {
                return HttpNotFound();
            }
            ViewBag.banco = new SelectList(db.Banco, "idBanco", "nombre", cuenta.banco);
            ViewBag.propietarioCuenta = new SelectList(db.Cliente, "idCliente", "nombreCliente", cuenta.propietarioCuenta);
            return View(cuenta);
        }

        // POST: Cuentas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCuenta,numCuenta,banco,propietarioCuenta,saldo,estado,direccion")] Cuenta cuenta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuenta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.banco = new SelectList(db.Banco, "idBanco", "nombre", cuenta.banco);
            ViewBag.propietarioCuenta = new SelectList(db.Cliente, "idCliente", "nombreCliente", cuenta.propietarioCuenta);
            return View(cuenta);
        }

        // GET: Cuentas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuenta cuenta = db.Cuenta.Find(id);
            if (cuenta == null)
            {
                return HttpNotFound();
            }
            return View(cuenta);
        }

        // POST: Cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cuenta cuenta = db.Cuenta.Find(id);
            db.Cuenta.Remove(cuenta);
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
