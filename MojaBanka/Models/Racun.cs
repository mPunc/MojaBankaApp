using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MojaBanka.Models
{
    [Table("racun")]
    public class Racun
    {
        [Required]
        [Display(Name = "Id računa u bazi")]
        [Key]
        [Column("id")]
        public int Id_racun { get; set; }

        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "Stanje računa")]
        [DisplayFormat(DataFormatString ="{0} €")]
        [Column("stanje_racun")]
        public double Stanje_racun { get; set; }

        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "Id klijenta u bazi")]
        [Column("id_klijent")]
        public int Id_klijent { get; set; }
    }
}