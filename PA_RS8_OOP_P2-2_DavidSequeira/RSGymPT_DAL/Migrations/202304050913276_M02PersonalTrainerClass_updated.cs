namespace RSGymPT_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M02PersonalTrainerClass_updated : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PersonalTrainer", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PersonalTrainer", "Code", c => c.String(nullable: false, maxLength: 4));
        }
    }
}
