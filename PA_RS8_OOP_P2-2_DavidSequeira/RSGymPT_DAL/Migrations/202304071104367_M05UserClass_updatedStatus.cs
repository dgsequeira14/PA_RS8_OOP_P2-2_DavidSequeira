namespace RSGymPT_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M05UserClass_updatedStatus : DbMigration       // ToDo: Nesta migration, tornei a propriedade Profile em String para que apareça na base de dados o Profile e não um número inteiro representativo do mesmo Enum.
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
