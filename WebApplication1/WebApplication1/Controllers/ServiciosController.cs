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
    public class ServiciosController : Controller
    {
        private WebBankingEntities16 db = new WebBankingEntities16();

        // GET: Servicios
        public ActionResult Index()
        {
            var servicio = db.Servicio.Include(s => s.Cuenta);
            return View(servicio.ToList());
        }

        // GET: Servicios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicio servicio = db.Servicio.Find(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            return View(servicio);
        }

        // GET: Servicios/Create
        public ActionResult Create()
        {
            ViewBag.idCuenta = new SelectList(db.Cuenta, "idCuenta", "numCuenta");
            return View();
        }

        // POST: Servicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string TipoServicio, [Bind(Include = "idServicio,tipoServicio,idCuenta,identifidor,nombreServicio,estado,monto")] Servicio servicio)
        {
            Servicio obj = new Servicio();

            obj.tipoServicio = TipoServicio;
           // obj.idCuenta = servicio.ValidarServicio(TipoServicio);
            obj.identifidor = servicio.identifidor;
            obj.nombreServicio = servicio.nombreServicio;
            obj.estado = servicio.estado;
            obj.monto = servicio.monto;

            if (ModelState.IsValid)
            {  
                db.Servicio.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCuenta = new SelectList(db.Cuenta, "idCuenta", "numCuenta", servicio.idCuenta);
            return View(servicio);
        }

        // GET: Servicios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicio servicio = db.Servicio.Find(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCuenta = new SelectList(db.Cuenta, "idCuenta", "numCuenta", servicio.idCuenta);
            return View(servicio);
        }

        // POST: Servicios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idServicio,tipoServicio,idCuenta,identifidor,nombreServicio,estado,monto")] Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCuenta = new SelectList(db.Cuenta, "idCuenta", "numCuenta", servicio.idCuenta);
            return View(servicio);
        }

        public ActionResult CambiarEstado(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicio servicio = db.Servicio.Find(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCuenta = new SelectList(db.Cuenta, "idCuenta", "numCuenta", servicio.idCuenta);
            return View(servicio);
        }

        // POST: Servicios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarEstado([Bind(Include = "idServicio,tipoServicio,idCuenta,identifidor,nombreServicio,estado,monto")] Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCuenta = new SelectList(db.Cuenta, "idCuenta", "numCuenta", servicio.idCuenta);
            return View(servicio);
        }

        // GET: Servicios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicio servicio = db.Servicio.Find(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            return View(servicio);
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Servicio servicio = db.Servicio.Find(id);
            db.Servicio.Remove(servicio);
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
