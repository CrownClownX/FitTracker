namespace FitTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 35),
                        DayOfWeek = c.Int(nullable: false),
                        WorkoutTemplateID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ActivityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.Sets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExerciseId = c.Int(nullable: false),
                        WorkoutId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercises", t => t.ExerciseId)
                .ForeignKey("dbo.Workouts", t => t.WorkoutId, cascadeDelete: true)
                .Index(t => t.ExerciseId)
                .Index(t => t.WorkoutId);
            
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 35),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Activities", t => t.ID, cascadeDelete: true)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Repetitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumberOfRepetitions = c.Int(nullable: false),
                        SetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sets", t => t.SetId, cascadeDelete: true)
                .Index(t => t.SetId);
            
            CreateTable(
                "dbo.ExerciseTemplates",
                c => new
                    {
                        Exercise_ID = c.Int(nullable: false),
                        Template_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Exercise_ID, t.Template_ID })
                .ForeignKey("dbo.Exercises", t => t.Exercise_ID, cascadeDelete: true)
                .ForeignKey("dbo.Templates", t => t.Template_ID, cascadeDelete: true)
                .Index(t => t.Exercise_ID)
                .Index(t => t.Template_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Templates", "ID", "dbo.Activities");
            DropForeignKey("dbo.Workouts", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.Sets", "WorkoutId", "dbo.Workouts");
            DropForeignKey("dbo.Repetitions", "SetId", "dbo.Sets");
            DropForeignKey("dbo.ExerciseTemplates", "Template_ID", "dbo.Templates");
            DropForeignKey("dbo.ExerciseTemplates", "Exercise_ID", "dbo.Exercises");
            DropForeignKey("dbo.Sets", "ExerciseId", "dbo.Exercises");
            DropIndex("dbo.ExerciseTemplates", new[] { "Template_ID" });
            DropIndex("dbo.ExerciseTemplates", new[] { "Exercise_ID" });
            DropIndex("dbo.Repetitions", new[] { "SetId" });
            DropIndex("dbo.Templates", new[] { "ID" });
            DropIndex("dbo.Sets", new[] { "WorkoutId" });
            DropIndex("dbo.Sets", new[] { "ExerciseId" });
            DropIndex("dbo.Workouts", new[] { "ActivityId" });
            DropTable("dbo.ExerciseTemplates");
            DropTable("dbo.Repetitions");
            DropTable("dbo.Templates");
            DropTable("dbo.Exercises");
            DropTable("dbo.Sets");
            DropTable("dbo.Workouts");
            DropTable("dbo.Activities");
        }
    }
}
