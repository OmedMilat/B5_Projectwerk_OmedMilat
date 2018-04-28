using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HenrikMotors.Models
{
    public class HenrikMotorsContext : DbContext
    {
        public HenrikMotorsContext() : base("name=HenrikMotorsContext")
        {

        }
        public DbSet<Voertuig> Voertuigen { get; set; }
        public DbSet<Merk> Merken { get; set; }
        public DbSet<Carrosserietype> Carrosserietypes { get; set; }
        public DbSet<Uitrusting> Uitrustingen { get; set; }
        public DbSet<VoertuigUitrusting> VoertuigUitrusting { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Voertuig>().HasKey(v => v.Id);
            modelBuilder.Entity<Uitrusting>().HasKey(u => u.Id);
            modelBuilder.Entity<VoertuigUitrusting>().HasKey(vu =>
                new
                {
                    vu.VoertuigId,
                    vu.UitrustingId
                });

            modelBuilder.Entity<VoertuigUitrusting>()
                .HasRequired(vu => vu.Voertuig)
                .WithMany(vu => vu.VoertuigUitrusting)
                .HasForeignKey(vu => vu.VoertuigId);

            modelBuilder.Entity<VoertuigUitrusting>()
                .HasRequired(vu => vu.Uitrusting)
                .WithMany(vu => vu.VoertuigUitrusting)
                .HasForeignKey(vu => vu.UitrustingId);
        }
    }
}