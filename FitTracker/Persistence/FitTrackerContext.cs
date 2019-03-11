using FitTracker.Core.Model;
using FitTracker.Persistence.Configurations;
using FitTracker.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Persistence
{
    public class FitTrackerContext : DbContext
    {
        public FitTrackerContext()
            : base("name=FitTrackerContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<Workout> Workouts { get; set; }
        public virtual DbSet<Template> WorkoutTemplates { get; set; }
        public virtual DbSet<Set> Sets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ActivityConfiguration());
            modelBuilder.Configurations.Add(new ExerciseConfiguration());
            modelBuilder.Configurations.Add(new WorkoutConfiguration());
            modelBuilder.Configurations.Add(new SetConfiguration());
        }
    }
}
