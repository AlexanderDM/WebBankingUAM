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
    public class TransferenciasController : Controller
    {
        private WebBankingEntities15 db = new WebBankingEntities15();

        // GET: Transferencias
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
            ViewBag.idcuentaOrigen = new SelectList(db.Cuenta, "idCuenta", "numCuenta");
            return View();
        }

        // POST: Transferencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTransferencia,idcliente,idcuentaOrigen,cuentaDestino,fechaHora,monto,detalle")] Transferencia transferencia)
        {
            Transferencia obj = new Transferencia();
          //  obj.idcliente =transferencia.ConsultarCuentaCliente(transferencia.idcliente);
            if (ModelState.IsValid)
            {
                db.Transferencia.Add(transferencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idcliente = new SelectList(db.Cliente, "idCliente", "nombreCliente", transferencia.idcliente);
          //  ViewBag.idcuentaOrigen = new SelectList(db.Cuenta, "idCuenta", "numCuenta", transferencia.idcuentaOrigen);
            ViewBag.idcuentaOrigen1 = new SelectList(db.CuentaPorCliente.Where(c =>c.idCliente == transferencia.idcliente), "idCuenta", "numCuenta", transferencia.idcuentaOrigen);
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
