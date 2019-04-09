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
        }
    }
}
