using FitTracker.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Persistence.Configurations
{
    public class SetConfiguration : EntityTypeConfiguration<Set>
    {
        public SetConfiguration()
        {
            HasMany(s => s.Repetitions)
                .WithRequired(r => r.Set)
                .WillCascadeOnDelete(true);
        }
    }
}
