using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MojaBanka.Models
{
    [Table("klijent")]
    public class Klijent
    {
        [Required]
        [Display(Name = "Id klijenta u bazi")]
        [Key]
        [Column("id")]
        public int Id_klijent { get; set; }

        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "Ime klijenta")]
        [Column("ime")]
        public string Ime_klijent { get; set; }

        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "Prezime klijenta")]
        [Column("prezime")]
        public string Prezime_klijent { get; set; }

        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "Adresa klijenta")]
        [Column("adresa")]
        public string Adresa_klijent { get; set; }

        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "OIB klijenta")]
        [Column("oib")]
        //[StringLength(11, MinimumLength = 11, ErrorMessage = "{0} mora bit duljine 11 znakova")]
        public string Oib_klijent { get; set; }

        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "Email klijenta")]
        [Column("email")]
        public string Email_klijent { get; set; }

        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "Datum rođenja")]
        [DisplayFormat(DataFormatString ="{0:dd. MM. yyyy}", ApplyFormatInEditMode = true)]
        [Column("datum")]
        [DataType(DataType.Date)]
        public DateTime Datum_klijent { get; set; }
    }
}