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
    public class KlijentiController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Klijenti
        public ActionResult Index()
        {
            return View(db.Klijenti.ToList());
        }

        // GET: Klijenti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klijent klijent = db.Klijenti.Find(id);
            if (klijent == null)
            {
                return HttpNotFound();
            }

            List<Racun> klijent_racuni = db.Racuni.Where(x => x.Id_klijent == id).ToList();
            ViewBag.Racuni = klijent_racuni;
            
            return View(klijent);
        }

        // GET: Klijenti/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Klijenti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_klijent,Ime_klijent,Prezime_klijent,Adresa_klijent,Oib_klijent,Email_klijent,Datum_klijent")] Klijent klijent)
        {
            bool valid = true;
            if (db.Klijenti.Any(x => x.Oib_klijent == klijent.Oib_klijent))
            {
                ModelState.AddModelError("Oib_klijent", "Klijent s istim OIB-om već postoji u bazi");
                valid = false;
            }
            if (db.Klijenti.Any(x => x.Email_klijent == klijent.Email_klijent))
            {
                ModelState.AddModelError("Email_klijent", "Klijent s istom e-mail adresom već postoji u bazi");
                valid = false;
            }
            if (ModelState.IsValid && valid)
            {
                db.Klijenti.Add(klijent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(klijent);
        }

        // GET: Klijenti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klijent klijent = db.Klijenti.Find(id);
            if (klijent == null)
            {
                return HttpNotFound();
            }
            return View(klijent);
        }

        // POST: Klijenti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_klijent,Ime_klijent,Prezime_klijent,Adresa_klijent,Oib_klijent,Email_klijent,Datum_klijent")] Klijent klijent)
        {
            bool valid = true;
            if (db.Klijenti.Any(x => x.Oib_klijent == klijent.Oib_klijent && x.Id_klijent != klijent.Id_klijent))
            {
                ModelState.AddModelError("Oib_klijent", "Klijent s istim OIB-om već postoji u bazi");
                valid = false;
            }
            if (db.Klijenti.Any(x => x.Email_klijent == klijent.Email_klijent && x.Id_klijent != klijent.Id_klijent))
            {
                ModelState.AddModelError("Email_klijent", "Klijent s istom e-mail adresom već postoji u bazi");
                valid = false;
            }
            if (ModelState.IsValid && valid)
            {
                db.Entry(klijent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(klijent);
        }

        // GET: Klijenti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klijent klijent = db.Klijenti.Find(id);
            if (klijent == null)
            {
                return HttpNotFound();
            }

            List<Racun> klijent_racuni = db.Racuni.Where(x => x.Id_klijent == id).ToList();
            ViewBag.Racuni = klijent_racuni;

            return View(klijent);
        }

        // POST: Klijenti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Klijent klijent = db.Klijenti.Find(id);
            db.Klijenti.Remove(klijent);
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
