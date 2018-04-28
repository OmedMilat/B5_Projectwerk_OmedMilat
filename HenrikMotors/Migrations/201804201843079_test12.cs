namespace HenrikMotors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Voertuigen", "Zitplaatsen", c => c.String());
            DropColumn("dbo.Voertuigen", "Plaatsen");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Voertuigen", "Plaatsen", c => c.String());
            DropColumn("dbo.Voertuigen", "Zitplaatsen");
        }
    }
}
