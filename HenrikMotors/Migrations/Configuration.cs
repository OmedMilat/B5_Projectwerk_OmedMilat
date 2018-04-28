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

            //categorie: 1= Veiligheid, 2= Comfort, 3= Multimedia, 4= Extra opties
            var uit1 = new Uitrusting() {Id=1 ,Naam = "Navigatie",Categorie= 3};
            var uit2 = new Uitrusting() { Id = 2,  Naam = "Harman",Categorie= 3 };
            var uit3 = new Uitrusting() { Id = 3, Naam = "Airbag", Categorie = 1 };
            var uit4 = new Uitrusting() { Id = 4, Naam = "Armsteun", Categorie = 2 };
            var uit5 = new Uitrusting() { Id = 5, Naam = "Getinte ramen", Categorie = 4 };

            context.Uitrustingen.AddOrUpdate(uit1);
            context.Uitrustingen.AddOrUpdate(uit2);
            context.Uitrustingen.AddOrUpdate(uit3);
            context.Uitrustingen.AddOrUpdate(uit4);
            context.Uitrustingen.AddOrUpdate(uit5);
            var m = new Merk() { Id = 1, Naam = "Bmw" };
            var m2 = new Merk() { Id = 2, Naam = "Mercedes-Benz" };
            context.Merken.AddOrUpdate(m);
            context.Merken.AddOrUpdate(m2);
            var c = new Carrosserietype() { Id = 1, Naam = "Sedan" };
            context.Carrosserietypes.AddOrUpdate(c);
            var v1 = new Voertuig()
            {
                Id = 1,
                MerkId = 2,
                Model= "GT-R",
                CarrosserietypeId = 1,
                Kleur = "Groen",
                Deuren = "2",
                Aandrijving = "Achter",
                Bouwjaar = DateTime.Now,
                Brandstof = "Benzine",
                Cilinders = "6",
                Zitplaatsen = "5",
                Transmissie = "Manueel",
                Vermogen = "547 pk",
                Versnellingen = "6",
                VoertuigUitrusting = new List<VoertuigUitrusting>()
            };

            var v2 = new Voertuig()
            {
                Id = 2,
                MerkId = 1,
                Model = "M4 GTS",
                CarrosserietypeId = 1,
                Kleur = "Grijs",
                Deuren = "2",
                Aandrijving = "Achter",
                Bouwjaar = DateTime.Now,
                Brandstof = "Benzine",
                Cilinders = "6",
                Zitplaatsen = "5",
                Transmissie = "Manueel",
                Vermogen = "500 pk",
                Versnellingen = "6",
                VoertuigUitrusting = new List<VoertuigUitrusting>()
            };
            context.SaveChanges();

            var lol1 = new VoertuigUitrusting { UitrustingId = 1,Uitrusting=uit1,Voertuig=v1 , VoertuigId = 1 };
            var lol2 = new VoertuigUitrusting { UitrustingId = 5, Uitrusting=uit5,Voertuig=v1, VoertuigId = 1 };
            var lol3 = new VoertuigUitrusting { UitrustingId = 1, Uitrusting = uit1, Voertuig = v2, VoertuigId = 2 };
            context.VoertuigUitrusting.AddOrUpdate(lol1);
            context.VoertuigUitrusting.AddOrUpdate(lol2);
            context.VoertuigUitrusting.AddOrUpdate(lol3);

        }
    }
}
