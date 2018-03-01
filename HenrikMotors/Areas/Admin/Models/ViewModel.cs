using HenrikMotors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HenrikMotors.Areas.Admin.Models
{
    public class ViewModel
    {
        //public VoertuigDTO Voertuig { get; set; }
        //public MerkDTO Merk { get; set; }
        //public CarrosserietypeDTO Carrosserietype { get; set; }
        public IQueryable<VoertuigDTO> VoertuigList { get; set; }
        public IQueryable<MerkDTO> MerkList { get; set; }
        public IQueryable<CarrosserietypeDTO> CarrosserietypeList { get; set; }
    }
}