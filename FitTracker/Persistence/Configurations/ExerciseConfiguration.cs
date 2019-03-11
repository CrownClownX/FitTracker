using FitTracker.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Persistence.Configurations
{
    public class ExerciseConfiguration : EntityTypeConfiguration<Exercise>
    {
        public ExerciseConfiguration()
        {
            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(35);

            HasMany(e => e.Sets)
                .WithRequired(s => s.Exercise)
                .WillCascadeOnDelete(false);

            HasMany(e => e.WorkoutTemplates)
                .WithMany(t => t.Exercises);
        }
    }
}
