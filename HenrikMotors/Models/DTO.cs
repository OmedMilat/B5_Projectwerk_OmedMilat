using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HenrikMotors.Models
{
    public class VoertuigDetailDTO
    {
        public int Id { get; set; }
        public string ArtikelNummer { get; set; }
        public string Model { get; set; }
        public string LijstFotos { get; set; }
        public List<string> FotoNaam { get
            {
                if (LijstFotos != null)
                    return LijstFotos.Split(',').ToList();
                else
                    return null;
            } set { } }
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
        public int UitrustingId { get; set; }
        public string UitrustingNaam { get; set; }
        public int MerkId { get; set; }
        public string MerkNaam { get; set; }
        public int CarrosserietypeId { get; set; }
        public string CarrosserietypeNaam { get; set; }
        public ICollection<VoertuigUitrustingDTO> VoertuigUitrusting { get; set; }
    }
    public class VoertuigDTO
    {
        public int Id { get; set; }
        public string ArtikelNummer { get; set; }
        public int MerkId { get; set; }
        public string MerkNaam { get; set; }
        public string Model { get; set; }
        public DateTime? Bouwjaar { get; set; }
        public string Vermogen { get; set; }
    }
    public class MerkDTO
    {
        public int Id { get; set; }
        public string Naam { get; set; }
    }
    public class CarrosserietypeDTO
    {
        public int Id { get; set; }
        public string Naam { get; set; }
    }
    public class UitrustingDTO
    {
        public int Id { get; set; }
        public string Naam { get; set; }
    }
    public class VoertuigUitrustingDTO
    {
        public int VoertuigId { get; set; }
        public string UitrustingNaam { get; set; }
        public string UitrustingId { get; set; }
    }
}