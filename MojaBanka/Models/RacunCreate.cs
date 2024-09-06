using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MojaBanka.Models
{
    public class RacunCreate
    {
        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "Stanje računa")]
        [DisplayFormat(DataFormatString = "{0} €")]
        public double Stanje_racun { get; set; }

        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "OIB klijenta")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "{0} mora bit duljine 11 znakova")]
        public string Oib_klijent { get; set; }
    }
}