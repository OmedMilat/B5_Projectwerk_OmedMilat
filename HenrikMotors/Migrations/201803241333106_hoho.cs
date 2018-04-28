namespace HenrikMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hoho : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carrosserietypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Voertuigen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArtikelNummer = c.String(),
                        Model = c.String(),
                        LijstFotos = c.String(),
                        Bouwjaar = c.DateTime(),
                        Kleur = c.String(),
                        Vermogen = c.String(),
                        Brandstof = c.String(),
                        Deuren = c.String(),
                        Plaatsen = c.String(),
                        Transmissie = c.String(),
                        Versnellingen = c.String(),
                        Cilinders = c.String(),
                        Aandrijving = c.String(),
                        MerkId = c.Int(nullable: false),
                        CarrosserietypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carrosserietypes", t => t.CarrosserietypeId, cascadeDelete: true)
                .ForeignKey("dbo.Merken", t => t.MerkId, cascadeDelete: true)
                .Index(t => t.MerkId)
                .Index(t => t.CarrosserietypeId);
            
            CreateTable(
                "dbo.Merken",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VoertuigUitrusting",
                c => new
                    {
                        VoertuigId = c.Int(nullable: false),
                        UitrustingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VoertuigId, t.UitrustingId })
                .ForeignKey("dbo.Uitrustingen", t => t.UitrustingId, cascadeDelete: true)
                .ForeignKey("dbo.Voertuigen", t => t.VoertuigId, cascadeDelete: true)
                .Index(t => t.VoertuigId)
                .Index(t => t.UitrustingId);
            
            CreateTable(
                "dbo.Uitrustingen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Categorie = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VoertuigUitrusting", "VoertuigId", "dbo.Voertuigen");
            DropForeignKey("dbo.VoertuigUitrusting", "UitrustingId", "dbo.Uitrustingen");
            DropForeignKey("dbo.Voertuigen", "MerkId", "dbo.Merken");
            DropForeignKey("dbo.Voertuigen", "CarrosserietypeId", "dbo.Carrosserietypes");
            DropIndex("dbo.VoertuigUitrusting", new[] { "UitrustingId" });
            DropIndex("dbo.VoertuigUitrusting", new[] { "VoertuigId" });
            DropIndex("dbo.Voertuigen", new[] { "CarrosserietypeId" });
            DropIndex("dbo.Voertuigen", new[] { "MerkId" });
            DropTable("dbo.Uitrustingen");
            DropTable("dbo.VoertuigUitrusting");
            DropTable("dbo.Merken");
            DropTable("dbo.Voertuigen");
            DropTable("dbo.Carrosserietypes");
        }
    }
}
