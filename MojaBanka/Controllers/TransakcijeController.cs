using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MojaBanka.Models;

namespace MojaBanka.Controllers
{
    public class TransakcijeController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Transakcije
        public ActionResult Index()
        {
            return View(db.Transakcije.ToList());
        }

        // GET: Transakcije/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transakcija transakcija = db.Transakcije.Find(id);
            if (transakcija == null)
            {
                return HttpNotFound();
            }
            return View(transakcija);
        }

        // GET: Transakcije/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transakcije/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_transakcija,Iznos_transakcije,Id_racun")] Transakcija transakcija)
        {
            if (ModelState.IsValid)
            {
                db.Transakcije.Add(transakcija);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transakcija);
        }

        // GET: Transakcije/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transakcija transakcija = db.Transakcije.Find(id);
            if (transakcija == null)
            {
                return HttpNotFound();
            }
            return View(transakcija);
        }

        // POST: Transakcije/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_transakcija,Iznos_transakcije,Id_racun")] Transakcija transakcija)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transakcija).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transakcija);
        }

        // GET: Transakcije/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transakcija transakcija = db.Transakcije.Find(id);
            if (transakcija == null)
            {
                return HttpNotFound();
            }
            return View(transakcija);
        }

        // POST: Transakcije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transakcija transakcija = db.Transakcije.Find(id);
            db.Transakcije.Remove(transakcija);
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
