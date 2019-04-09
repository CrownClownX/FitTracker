namespace FitTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeExerciseInTemplateAndSetToString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sets", "ExerciseId", "dbo.Exercises");
            DropForeignKey("dbo.ExerciseTemplates", "Exercise_ID", "dbo.Exercises");
            DropForeignKey("dbo.ExerciseTemplates", "Template_ID", "dbo.Templates");
            DropIndex("dbo.Sets", new[] { "ExerciseId" });
            DropIndex("dbo.ExerciseTemplates", new[] { "Exercise_ID" });
            DropIndex("dbo.ExerciseTemplates", new[] { "Template_ID" });
            AddColumn("dbo.Sets", "Exercise", c => c.String());
            DropColumn("dbo.Sets", "ExerciseId");
            DropTable("dbo.ExerciseTemplates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExerciseTemplates",
                c => new
                    {
                        Exercise_ID = c.Int(nullable: false),
                        Template_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Exercise_ID, t.Template_ID });
            
            AddColumn("dbo.Sets", "ExerciseId", c => c.Int(nullable: false));
            DropColumn("dbo.Sets", "Exercise");
            CreateIndex("dbo.ExerciseTemplates", "Template_ID");
            CreateIndex("dbo.ExerciseTemplates", "Exercise_ID");
            CreateIndex("dbo.Sets", "ExerciseId");
            AddForeignKey("dbo.ExerciseTemplates", "Template_ID", "dbo.Templates", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ExerciseTemplates", "Exercise_ID", "dbo.Exercises", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Sets", "ExerciseId", "dbo.Exercises", "ID");
        }
    }
}
