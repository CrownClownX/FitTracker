using FitTracker.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Persistence.Configurations
{
    public class WorkoutConfiguration : EntityTypeConfiguration<Workout>
    {
        public WorkoutConfiguration()
        {
            HasMany(w => w.Sets)
                .WithRequired(s => s.Workout)
                .WillCascadeOnDelete(true);
        }
    }
}
