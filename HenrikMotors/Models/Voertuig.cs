using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HenrikMotors.Models
{
    [Table("Voertuigen")]
    public class Voertuig
    {

        public int Id { get; set; }
        [NotMapped]
        private string artikelNummer;
        public string ArtikelNummer
        {
            get { return artikelNummer= "HMPV-" + Id;}
            set {}
        }
        public string Model { get; set; }
        //public string LijstFotosString
        //{
        //    get { return string.Join(",", LijstFotos); }
        //    set { LijstFotos = value.Split(',').ToList(); }
        //}
        //[NotMapped]
        //public List<string> LijstFotos { get { return Merk + Model + Id.ToString(); } set { } }

        public string LijstFotos { get; set; }
        public DateTime? Bouwjaar { get; set; }
        public string Kleur { get; set; }
        public string Vermogen { get; set; }
        public string Brandstof { get; set; }
        public string Deuren { get; set; }
        public string Plaatsen { get; set; }
        public string Transmissie { get; set; }
        public string Versnellingen { get; set; }
        public string Cilinders { get; set; }
        public string Aandrijving { get; set; }
        public virtual ICollection<VoertuigUitrusting> VoertuigUitrusting { get; set; }
        public int MerkId { get; set; }
        [ForeignKey("MerkId")]
        public virtual Merk Merk { get; set; }
        public int CarrosserietypeId { get; set; }
        [ForeignKey("CarrosserietypeId")]
        public virtual Carrosserietype Carrosserietype { get; set; }
    }


    [Table("Uitrustingen")]
    public class Uitrusting
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public virtual ICollection<VoertuigUitrusting> VoertuigUitrusting { get; set; }
    }
    [Table("VoertuigUitrusting")]
    public class VoertuigUitrusting
    {
        public virtual Voertuig Voertuig { get; set; }
        public virtual Uitrusting Uitrusting { get; set; }
        public int VoertuigId { get; set; }
        public int UitrustingId { get; set; }
    }
}