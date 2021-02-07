using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MDKA;

namespace MDKA.Controllers
{
    public class TransactionReservasiController : Controller
    {
        private MDKAReservasiEntities db = new MDKAReservasiEntities();

        // GET: TransactionReservasi
        public ActionResult Index()
        {
            return View(db.tblT_Reservasi.ToList());
        }

        // GET: TransactionReservasi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblT_Reservasi tblT_Reservasi = db.tblT_Reservasi.Find(id);
            if (tblT_Reservasi == null)
            {
                return HttpNotFound();
            }
            return View(tblT_Reservasi);
        }

        // GET: TransactionReservasi/Create
        public ActionResult Create()
        {
            MDKAReservasiEntities ds = new MDKAReservasiEntities();

            var items = ds.tblM_Ruangan.Where(x => x.Status_FK == 2).ToList();
            ViewBag.data = items;
            return View();
        }

        // POST: TransactionReservasi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Reservasi_PK,Ruangan_FK,SubjectReservasi,TanggalReservasi,JamMulai,JamSelesai,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] tblT_Reservasi tblT_Reservasi)
        {
            if (ModelState.IsValid)
            {
                string Ruangan_FK = Request.Form["Ruangan_FK"].ToString();
                string TanggalReservasi = Request.Form["TanggalReservasi"].ToString();

                SqlConnection conn = new SqlConnection("Data Source=NB20-0931; Initial Catalog=MDKAReservasi; Integrated Security=True;");
                conn.Open();

                SqlCommand command = new SqlCommand("Select * from tblT_Reservasi where Ruangan_FK=@Ruangan_FK and TanggalReservasi = @TanggalReservasi", conn);
                command.Parameters.AddWithValue("@Ruangan_FK", Ruangan_FK);
                command.Parameters.AddWithValue("@TanggalReservasi", TanggalReservasi);
                // int result = command.ExecuteNonQuery();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
                        SqlConnection sqlconn = new SqlConnection(mainconn);
                        SqlCommand cmd = new SqlCommand("UPDATE TblM_Ruangan SET [Status_FK] = 3 WHERE [Ruangan_PK] = '" + Ruangan_FK + "'", sqlconn);

                        sqlconn.Open();
                        cmd.ExecuteNonQuery();
                        sqlconn.Close();

                        db.tblT_Reservasi.Add(tblT_Reservasi);
                        db.SaveChanges();
                    }
                }

                conn.Close();
                return RedirectToAction("Index");
            }

            return View(tblT_Reservasi);
        }

        // GET: TransactionReservasi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblT_Reservasi tblT_Reservasi = db.tblT_Reservasi.Find(id);
            if (tblT_Reservasi == null)
            {
                return HttpNotFound();
            }
            return View(tblT_Reservasi);
        }

        // POST: TransactionReservasi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Reservasi_PK,Ruangan_FK,SubjectReservasi,TanggalReservasi,JamMulai,JamSelesai,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] tblT_Reservasi tblT_Reservasi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblT_Reservasi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblT_Reservasi);
        }

        // GET: TransactionReservasi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblT_Reservasi tblT_Reservasi = db.tblT_Reservasi.Find(id);
            if (tblT_Reservasi == null)
            {
                return HttpNotFound();
            }
            return View(tblT_Reservasi);
        }

        // POST: TransactionReservasi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string Ruangan_FK = Request.Form["Ruangan_FK"].ToString();

            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            SqlCommand cmd = new SqlCommand("UPDATE TblM_Ruangan SET [Status_FK] = 2 WHERE [Ruangan_PK] = '" + Ruangan_FK + "'", sqlconn);

            sqlconn.Open();
            cmd.ExecuteNonQuery();
            sqlconn.Close();

            tblT_Reservasi tblT_Reservasi = db.tblT_Reservasi.Find(id);
            db.tblT_Reservasi.Remove(tblT_Reservasi);
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
