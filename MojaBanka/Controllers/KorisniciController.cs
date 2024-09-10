using MojaBanka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MojaBanka.Controllers
{
    public class KorisniciController : Controller
    {
        MyDbContext db = new MyDbContext();
        // GET: Korisnici
        [Authorize(Roles = OvlastiKorisnik.Administrator + ", " + OvlastiKorisnik.Editor)]
        public ActionResult Index()
        {
            var listaKorisnika = db.Korisnici.OrderBy(x => x.Ovlast).ToList();
            ViewBag.Ovlasti = db.Ovlasti.ToList();
            return View(listaKorisnika);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registracija()
        {
            Korisnik model = new Korisnik();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Registracija(Korisnik model)
        {
            bool okReg = true;
            if (db.Korisnici.Any(x => x.Oib == model.Oib))
            {
                okReg = false;
                ModelState.AddModelError("Oib", "Korisnik s istim OIB-om već postoji");
            }
            if (model.Ovlast == "KL")
            {
                if (db.Klijenti.FirstOrDefault(x => x.Oib_klijent == model.Oib) == null)
                {
                    okReg = false;
                    ModelState.AddModelError("Oib", "Klijent s ovim OIB-om ne postoji u bazi podataka");
                }
            }
            else if (db.Klijenti.Any(x => x.Oib_klijent == model.Oib))
            {
                okReg = false;
                ModelState.AddModelError("Oib", "Klijent s ovim OIB-om već postoji u bazi podataka (nije ok za admina i editora)");
            }
            if (ModelState.IsValid && okReg)
            {
                model.Lozinka = PasswordHelper.GetHash(model.LozinkaUnos);
                db.Korisnici.Add(model);
                db.SaveChanges();
                return RedirectToAction("Prijava");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Prijava(string returnUrl)
        {
            KorisnikPrijava model = new KorisnikPrijava();
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Prijava(KorisnikPrijava model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var korisnikBaza = db.Korisnici.FirstOrDefault(x => x.Oib == model.Oib);
                if (korisnikBaza != null)
                {
                    var passwordOK = korisnikBaza.Lozinka == PasswordHelper.GetHash(model.Lozinka);
                    //var passwordOK = korisnikBaza.Lozinka == model.Lozinka;
                    if (passwordOK)
                    {
                        LogiraniKorisnik prijavljeniKorisnik = new LogiraniKorisnik(korisnikBaza);
                        LogiraniKorisnikSerializeModel serializeModel = new LogiraniKorisnikSerializeModel();
                        serializeModel.CopyFromUser(prijavljeniKorisnik);
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        string korisnickiPodaci = serializer.Serialize(serializeModel);

                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                            1,
                            prijavljeniKorisnik.Identity.Name,
                            DateTime.Now,
                            DateTime.Now.AddDays(1),
                            false,
                            korisnickiPodaci
                            );

                        string ticketEncrypted = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketEncrypted);
                        Response.Cookies.Add(cookie);

                        if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Neispravan OIB ili lozinka");
            return View(model);
        }

        [OverrideAuthorization]
        [Authorize]
        public ActionResult Odjava()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        public ActionResult Delete(string oib)
        {
            if (oib == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Korisnik korisnik = db.Korisnici.Find(oib);
            if (korisnik == null)
            {
                return HttpNotFound();
            }
            return View(korisnik);
        }

        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string oib)
        {
            Korisnik korisnik = db.Korisnici.Find(oib);
            db.Korisnici.Remove(korisnik);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}