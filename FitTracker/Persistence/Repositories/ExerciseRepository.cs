using FitTracker.Core.Model;
using FitTracker.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Persistence.Repositories
{
    public class ExerciseRepository : Repository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(FitTrackerContext context)
           : base(context)
        {
        }

        public FitTrackerContext FitTrackerContext
        {
            get { return Context as FitTrackerContext; }
        }
    }
}
