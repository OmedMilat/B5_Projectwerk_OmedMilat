using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HenrikMotors.Models
{
    [Table("Merken")]
    public class Merk
    {
        public int Id { get; set; }
        //[Column(TypeName = "varchar")]
        //[StringLength(500)]
        //[Index(IsUnique = true)]
        [Display(Name = "Merk")]
        public string Naam { get; set; }
        public virtual ICollection<Voertuig> Voertuigen { get; set; }
    }
}