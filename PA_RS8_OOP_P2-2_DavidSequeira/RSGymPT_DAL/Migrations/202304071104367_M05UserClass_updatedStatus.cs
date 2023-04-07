namespace RSGymPT_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M05UserClass_updatedStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "Profile", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Profile", c => c.Int(nullable: false));
        }
    }
}
