namespace RSGymPT_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M03ClientClass_updated : DbMigration       // ToDo: Nesta migration, tomei a decisão de dividir a propriedade Name em First Name e Last Name, mantendo a Name apenas para efeitos de listagem em consola.
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
