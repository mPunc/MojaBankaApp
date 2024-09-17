using MojaBanka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MojaBanka.Controllers
{
    public class ProfilController : Controller
    {
        private MyDbContext db = new MyDbContext();

        [Authorize(Roles = OvlastiKorisnik.Klijent)]
        public ActionResult MojiPodaci()
        {
            List<Klijent> lista = db.Klijenti.ToList();
            Klijent klijent = lista.FirstOrDefault(x => x.Oib_klijent == ((User as LogiraniKorisnik).Oib));
            List<Racun> klijent_racuni = db.Racuni.Where(x => x.Id_klijent == klijent.Id_klijent).ToList();
            ViewBag.Racuni = klijent_racuni;
            return View(klijent);
        }

        [Authorize(Roles = OvlastiKorisnik.Klijent)]
        public ActionResult MojRacun(int? id)
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
            if (povezani_klijent.Oib_klijent != ((User as LogiraniKorisnik).Oib))
            {
                return HttpNotFound();
            }
            ViewBag.Klijent = povezani_klijent;
            List<Transakcija> povezane_transakcije = db.Transakcije.Where(x => x.Id_racun == id).ToList();
            ViewBag.Transakcije = povezane_transakcije;
            List<Kredit> povezani_krediti = db.Kredits.Where(x => x.Id_racun == id).ToList();
            ViewBag.Krediti = povezani_krediti;

            return View(racun);
        }

        [Authorize(Roles = OvlastiKorisnik.Klijent)]
        public ActionResult MojeTransakcije(int? id)
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

            Racun povezani_racun = db.Racuni.Find(transakcija.Id_racun);
            ViewBag.Racun = povezani_racun;
            Klijent povezani_klijent = db.Klijenti.Find(povezani_racun.Id_klijent);
            ViewBag.Klijent = povezani_klijent;

            if (povezani_klijent.Oib_klijent != ((User as LogiraniKorisnik).Oib))
            {
                return HttpNotFound();
            }

            return View(transakcija);
        }

        [Authorize(Roles = OvlastiKorisnik.Klijent)]
        public ActionResult MojiKrediti(int? id)
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

            if (povezani_klijent.Oib_klijent != ((User as LogiraniKorisnik).Oib))
            {
                return HttpNotFound();
            }

            return View(kredit);
        }
    }
}