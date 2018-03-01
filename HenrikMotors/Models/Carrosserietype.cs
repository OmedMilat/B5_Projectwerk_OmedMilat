using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HenrikMotors.Models
{
    [Table("Carrosserietypes")]
    public class Carrosserietype
    {
        public int Id { get; set; }
        [Display(Name = "Carrosserietype")]
        public string Naam { get; set; }
        public virtual ICollection<Voertuig> Voertuigen { get; set; }
    }
}