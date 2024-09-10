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
    public class RacuniController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Racuni
        [Authorize(Roles = OvlastiKorisnik.Administrator + ", " + OvlastiKorisnik.Editor)]
        public ActionResult Index()
        {
            ViewBag.Klijenti = db.Klijenti.ToList();
            return View(db.Racuni.ToList());
        }

        // GET: Racuni/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuni.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }

            Klijent povezani_klijent = db.Klijenti.FirstOrDefault(x => x.Id_klijent == db.Racuni.FirstOrDefault(y => y.Id_racun == id).Id_klijent);
            ViewBag.Klijent = povezani_klijent;
            List<Transakcija> povezane_transakcije = db.Transakcije.Where(x => x.Id_racun == id).ToList();
            ViewBag.Transakcije = povezane_transakcije;

            return View(racun);
        }

        // GET: Racuni/Create
        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Racuni/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Stanje_racun,Oib_klijent")] RacunCreate sent)
        {
            if (ModelState.IsValid)
            {
                if (db.Klijenti.Any(x => x.Oib_klijent == sent.Oib_klijent))
                {
                    Racun racun = new Racun
                    {
                        Stanje_racun = sent.Stanje_racun,
                        Id_klijent = db.Klijenti.FirstOrDefault(x => x.Oib_klijent == sent.Oib_klijent).Id_klijent
                    };
                    db.Racuni.Add(racun);
                    db.SaveChanges();
                    return RedirectToAction("Index");   
                }
                else
                {
                    ModelState.AddModelError("Oib_klijent", "Ovaj OIB ne postoji u zapisima klijenata");
                    return View(sent);
                }
            }

            return View(sent);
        }

        // GET: Racuni/Edit/5
        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuni.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }

            Klijent povezani_klijent = db.Klijenti.FirstOrDefault(x => x.Id_klijent == db.Racuni.FirstOrDefault(y => y.Id_racun == id).Id_klijent);
            ViewBag.Klijent = povezani_klijent;

            return View(racun);
        }

        // POST: Racuni/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_racun,Stanje_racun,Id_klijent")] Racun racun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(racun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(racun);
        }

        // GET: Racuni/Delete/5
        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuni.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            Klijent povezani_klijent = db.Klijenti.FirstOrDefault(x => x.Id_klijent == db.Racuni.FirstOrDefault(y => y.Id_racun == id).Id_klijent);
            ViewBag.Klijent = povezani_klijent;
            return View(racun);
        }

        // POST: Racuni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        public ActionResult DeleteConfirmed(int id)
        {
            Racun racun = db.Racuni.Find(id);
            db.Racuni.Remove(racun);
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
