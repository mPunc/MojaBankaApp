using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MojaBanka.Models
{
    [Table("transakcija")]
    public class Transakcija
    {
        [Required]
        [Display(Name = "Id transakcije")]
        [Key]
        [Column("id")]
        public int Id_transakcija { get; set; }

        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "Iznos transakcije")]
        [DisplayFormat(DataFormatString = "{0} €")]
        [Column("iznos")]
        public double Iznos_transakcije { get; set; }

        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "Id računa u bazi")]
        [Column("id_racun")]
        public int Id_racun { get; set; }

        [Display(Name = "Datum transakcije")]
        [DisplayFormat(DataFormatString = "{0:dd. MM. yyyy HH:mm}", ApplyFormatInEditMode = false)]
        [DataType(DataType.DateTime)]
        [Column("datum_vrijeme")]
        public DateTime Datum_transakcije { get; set; }

        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "Opis")]
        [Column("opis")]
        public string Opis_transakcije { get; set; }
    }
}