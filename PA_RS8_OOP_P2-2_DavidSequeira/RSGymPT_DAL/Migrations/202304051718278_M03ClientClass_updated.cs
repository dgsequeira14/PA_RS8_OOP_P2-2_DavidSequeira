namespace RSGymPT_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M03ClientClass_updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Client", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Client", "LastName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Client", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Client", "Name", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Client", "LastName");
            DropColumn("dbo.Client", "FirstName");
        }
    }
}
