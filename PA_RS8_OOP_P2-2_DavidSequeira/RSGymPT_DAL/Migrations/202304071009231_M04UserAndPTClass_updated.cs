namespace RSGymPT_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M04UserAndPTClass_updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonalTrainer", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.PersonalTrainer", "LastName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.User", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.User", "LastName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.PersonalTrainer", "Name");
            DropColumn("dbo.User", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Name", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.PersonalTrainer", "Name", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.User", "LastName");
            DropColumn("dbo.User", "FirstName");
            DropColumn("dbo.PersonalTrainer", "LastName");
            DropColumn("dbo.PersonalTrainer", "FirstName");
        }
    }
}
