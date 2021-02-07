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
    public class StatusController : Controller
    {
        private MDKAReservasiEntities db = new MDKAReservasiEntities();

        // GET: Status
        public ActionResult Index()
        {
            return View(db.tblM_Status.ToList());
        }

        // GET: Status/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblM_Status tblM_Status = db.tblM_Status.Find(id);
            if (tblM_Status == null)
            {
                return HttpNotFound();
            }
            return View(tblM_Status);
        }

        // GET: Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Status_PK,NamaStatus,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] tblM_Status tblM_Status)
        {
            if (ModelState.IsValid)
            {
                db.tblM_Status.Add(tblM_Status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblM_Status);
        }

        // GET: Status/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblM_Status tblM_Status = db.tblM_Status.Find(id);
            if (tblM_Status == null)
            {
                return HttpNotFound();
            }
            return View(tblM_Status);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Status_PK,NamaStatus,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] tblM_Status tblM_Status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblM_Status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblM_Status);
        }

        // GET: Status/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblM_Status tblM_Status = db.tblM_Status.Find(id);
            if (tblM_Status == null)
            {
                return HttpNotFound();
            }
            return View(tblM_Status);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblM_Status tblM_Status = db.tblM_Status.Find(id);
            db.tblM_Status.Remove(tblM_Status);
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
