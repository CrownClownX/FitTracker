using FitTracker.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Persistence
{
    public class FitTrackerDbContextcs : DbContext
    {
        public FitTrackerDbContextcs()
            : base("name=FitTrackerContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Workout> Workouts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
