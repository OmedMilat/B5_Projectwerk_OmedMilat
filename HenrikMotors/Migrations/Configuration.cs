namespace HenrikMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using HenrikMotors.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<HenrikMotorsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HenrikMotors.Models.HenrikMotorsContext context)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}
            //context.Database.Delete();
            context.Database.CreateIfNotExists();

            var uit1 = new Uitrusting() {Id=1 ,Naam = "Navigatie" };
            var uit2 = new Uitrusting() { Id = 2,  Naam = "Harman" };
            context.Uitrustingen.Add(uit1);
            context.Uitrustingen.Add(uit2);
            var m = new Merk() { Id = 1, Naam = "Bmw" };
            var m2 = new Merk() { Id = 2, Naam = "Mercedes-Benz" };
            context.Merken.Add(m);
            context.Merken.Add(m2);
            var c = new Carrosserietype() { Id = 1, Naam = "Sedan" };
            context.Carrosserietypes.Add(c);
            var v1 = new Voertuig()
            {
                Id = 1,
                MerkId = 1,
                CarrosserietypeId = 1,
                LijstFotos = "Foto1,Foto2,Foto3",
                Kleur = "zwart",
                Deuren = "2",
                Aandrijving = "Voor",
                Bouwjaar = DateTime.Now,
                Brandstof = "Benzine",
                Cilinders = "6",
                Plaatsen = "5",
                Transmissie = "Manueel",
                Vermogen = "355pk",
                Versnellingen = "6",
                VoertuigUitrusting = new List<VoertuigUitrusting>()
            };

            var v2 = new Voertuig() { Id = 2,
                MerkId = 2,
                CarrosserietypeId = 1,
                Kleur = "silver", Deuren = "3" };

            context.SaveChanges();

            var lol1 = new VoertuigUitrusting { UitrustingId = 1,Uitrusting=uit1,Voertuig=v1 , VoertuigId = 1 };
            var lol2 = new VoertuigUitrusting { UitrustingId = 2, Uitrusting=uit2,Voertuig=v1, VoertuigId = 1 };
            var lol3 = new VoertuigUitrusting { UitrustingId = 1, Uitrusting = uit1, Voertuig = v2, VoertuigId = 2 };
            context.VoertuigUitrusting.Add(lol1);
            context.VoertuigUitrusting.Add(lol2);
            context.VoertuigUitrusting.Add(lol3);

        }
    }
}
