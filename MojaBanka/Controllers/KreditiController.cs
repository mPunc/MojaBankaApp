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
    public class KreditiController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Krediti
        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        public ActionResult Index()
        {
            return View(db.Kredits.ToList());
        }

        // GET: Krediti/Details/5
        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kredit kredit = db.Kredits.Find(id);
            if (kredit == null)
            {
                return HttpNotFound();
            }
            Racun povezani_racun = db.Racuni.Find(kredit.Id_racun);
            ViewBag.Racun = povezani_racun;
            Klijent povezani_klijent = db.Klijenti.Find(povezani_racun.Id_klijent);
            ViewBag.Klijent = povezani_klijent;
            return View(kredit);
        }

        // GET: Krediti/Create
        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        public ActionResult Create(int? id)
        {
            Kredit tr = new Kredit();
            if (id != null)
            {
                tr.Id_racun = (int)id;
                tr.Rok_otplate = DateTime.Now;
                return View(tr);
            }
            return View();
        }

        // POST: Krediti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        public ActionResult Create([Bind(Include = "Id_kredit,Id_racun,Iznos_kredita,Kreditna_stopa,Rok_otplate")] Kredit kredit)
        {
            bool valid = true;
            if (!db.Racuni.Any(x => x.Id_racun == kredit.Id_racun)) 
            {
                ModelState.AddModelError("Id_racun", "Ne postoji racun s ovim ID");
                valid = false; 
            }
            if (kredit.Rok_otplate.Date <= DateTime.Now.Date)
            {
                ModelState.AddModelError("Rok_otplate", "Nevaljan datum");
                valid = false;
            }
            if (kredit.Iznos_kredita <= 0)
            {
                ModelState.AddModelError("Iznos_kredita", "Iznos ne može biti nula i manje");
                valid = false;
            }
            if (ModelState.IsValid && valid)
            {
                db.Racuni.Find(kredit.Id_racun).Stanje_racun += kredit.Iznos_kredita;
                db.Kredits.Add(kredit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kredit);
        }

        // GET: Krediti/Edit/5
        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kredit kredit = db.Kredits.Find(id);
            if (kredit == null)
            {
                return HttpNotFound();
            }
            return View(kredit);
        }

        // POST: Krediti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        public ActionResult Edit([Bind(Include = "Id_kredit,Id_racun,Iznos_kredita,Kreditna_stopa,Datum_klijent")] Kredit kredit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kredit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kredit);
        }

        // GET: Krediti/Delete/5
        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kredit kredit = db.Kredits.Find(id);
            if (kredit == null)
            {
                return HttpNotFound();
            }
            Racun povezani_racun = db.Racuni.Find(kredit.Id_racun);
            ViewBag.Racun = povezani_racun;
            Klijent povezani_klijent = db.Klijenti.Find(povezani_racun.Id_klijent);
            ViewBag.Klijent = povezani_klijent;
            return View(kredit);
        }

        // POST: Krediti/Delete/5
        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kredit kredit = db.Kredits.Find(id);
            db.Kredits.Remove(kredit);
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
