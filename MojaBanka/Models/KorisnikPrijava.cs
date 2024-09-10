using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MojaBanka.Models
{
    public class KorisnikPrijava
    {
        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "OIB")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "{0} mora bit duljine 11 znakova")]
        public string Oib { get; set; }

        [Required]
        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }
    }
}