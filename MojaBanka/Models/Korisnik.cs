using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MojaBanka.Models
{
    [Table("korisnik")]
    public class Korisnik
    {
        [Key]
        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "OIB")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "{0} mora bit duljine 11 znakova")]
        [Column("oib")]
        public string Oib { get; set; }

        public string Lozinka { get; set; }

        [Required]
        [Display(Name = "Ovlast")]
        public string Ovlast { get; set; }

        [Required]
        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string LozinkaUnos { get; set; }
    }
}