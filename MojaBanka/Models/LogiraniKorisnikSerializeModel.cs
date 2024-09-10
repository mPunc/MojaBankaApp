using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojaBanka.Models
{
    public class LogiraniKorisnikSerializeModel
    {
        public string Oib { get; set; }
        public string Ovlast { get; set; }

        internal void CopyFromUser(LogiraniKorisnik user)
        {
            this.Oib = user.Oib;
            this.Ovlast = user.Ovlast;
        }
    }
}