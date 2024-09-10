using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MojaBanka.Models
{
    public class LogiraniKorisnik : IPrincipal
    {
        public string Oib {  get; set; }
        public string Ovlast { get; set; }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            if (Ovlast == role) return true;
            return false;
        }

        public LogiraniKorisnik(Korisnik kor)
        {
            this.Identity = new GenericIdentity(kor.Oib);
            this.Oib = kor.Oib;
            this.Ovlast = kor.Ovlast;
        }

        public LogiraniKorisnik(string oib)
        {
            this.Identity = new GenericIdentity(oib);
            this.Oib = oib;
        }
    }
}