using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MDKA;

namespace MDKA.Controllers
{
    public class RuanganController : Controller
    {
        private MDKAReservasiEntities db = new MDKAReservasiEntities();

        // GET: Ruangan
        public ActionResult Index()
        {
            return View(db.tblM_Ruangan.ToList());
        }

        // GET: Ruangan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblM_Ruangan tblM_Ruangan = db.tblM_Ruangan.Find(id);
            if (tblM_Ruangan == null)
            {
                return HttpNotFound();
            }
            return View(tblM_Ruangan);
        }

        // GET: Ruangan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ruangan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ruangan_PK,NamaRuangan,Lantai,DayaTampung,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,Status_FK")] tblM_Ruangan tblM_Ruangan)
        {
            if (ModelState.IsValid)
            {
                db.tblM_Ruangan.Add(tblM_Ruangan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblM_Ruangan);
        }

        // GET: Ruangan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblM_Ruangan tblM_Ruangan = db.tblM_Ruangan.Find(id);
            if (tblM_Ruangan == null)
            {
                return HttpNotFound();
            }
            return View(tblM_Ruangan);
        }

        // POST: Ruangan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ruangan_PK,NamaRuangan,Lantai,DayaTampung,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,Status_FK")] tblM_Ruangan tblM_Ruangan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblM_Ruangan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblM_Ruangan);
        }

        // GET: Ruangan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblM_Ruangan tblM_Ruangan = db.tblM_Ruangan.Find(id);
            if (tblM_Ruangan == null)
            {
                return HttpNotFound();
            }
            return View(tblM_Ruangan);
        }

        // POST: Ruangan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblM_Ruangan tblM_Ruangan = db.tblM_Ruangan.Find(id);
            db.tblM_Ruangan.Remove(tblM_Ruangan);
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
