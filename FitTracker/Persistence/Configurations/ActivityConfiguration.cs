using FitTracker.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Persistence.Configurations
{
    public class ActivityConfiguration : EntityTypeConfiguration<Activity>
    {
        public ActivityConfiguration()
        {
            Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(35);

            Property(a => a.DayOfWeek)
                .IsRequired();

            HasMany(a => a.History)
                .WithRequired(w => w.Activity)
                .WillCascadeOnDelete(true);

            HasOptional(a => a.WorkoutTemplate)
                .WithRequired(t => t.Activity)
                .WillCascadeOnDelete(true);
        }
    }
}
