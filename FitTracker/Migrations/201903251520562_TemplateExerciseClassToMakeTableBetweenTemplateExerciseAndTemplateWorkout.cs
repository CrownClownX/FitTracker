namespace FitTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TemplateExerciseClassToMakeTableBetweenTemplateExerciseAndTemplateWorkout : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TemplateExercises",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Template_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Templates", t => t.Template_ID)
                .Index(t => t.Template_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TemplateExercises", "Template_ID", "dbo.Templates");
            DropIndex("dbo.TemplateExercises", new[] { "Template_ID" });
            DropTable("dbo.TemplateExercises");
        }
    }
}
