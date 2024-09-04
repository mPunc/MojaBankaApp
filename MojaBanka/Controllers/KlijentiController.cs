﻿using System;
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

            List<Racun> klijent_racuni = new List<Racun>();

            foreach(Racun rac in db.Racuni)
            {
                if(rac.Id_klijent == id)
                {
                    klijent_racuni.Add(rac);
                }
            }

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
            if (ModelState.IsValid)
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
            if (ModelState.IsValid)
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
