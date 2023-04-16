namespace RSGymPT_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M02PersonalTrainerClass_updated : DbMigration      // ToDo: Nesta migration, tomei a decisão de criar uma propriedade Read-Only com a concatenação do código do PT:
                                                                                     // public string Code => $"PT0{PersonalTrainerID}";
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
