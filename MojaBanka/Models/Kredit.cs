using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MojaBanka.Models
{
    [Table("kredit")]
    public class Kredit
    {
        [Required]
        [Display(Name = "Id kredita")]
        [Key]
        [Column("id")]
        public int Id_kredit { get; set; }

        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "Id računa u bazi")]
        [Column("id_racun")]
        public int Id_racun { get; set; }

        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "Iznos kredita (€)")]
        [DisplayFormat(DataFormatString = "{0} €")]
        [Column("iznos")]
        public double Iznos_kredita { get; set; }

        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "Kreditna stopa (%)")]
        [DisplayFormat(DataFormatString = "{0}%")]
        [Column("stopa")]
        [Range(0, 100, ErrorMessage = "Pogrešna kreditna stopa")]
        public double Kreditna_stopa { get; set; }

        [Required(ErrorMessage = "{0} je obavezan podatak")]
        [Display(Name = "Rok otplate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column("rok")]
        public DateTime Rok_otplate { get; set; }
    }
}