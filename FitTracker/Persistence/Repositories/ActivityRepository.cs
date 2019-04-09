using FitTracker.Core.Model;
using FitTracker.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Persistence.Repositories
{
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        public ActivityRepository(FitTrackerContext context)
           : base(context)
        {
        }

        public FitTrackerContext FitTrackerContext
        {
            get { return Context as FitTrackerContext; }
        }

        public override Activity Get(int id)
        {
            return FitTrackerContext.Activities
                .Include(a => a.WorkoutTemplate)
                .Include(a => a.WorkoutTemplate.Exercises)
                .Include(a => a.History)
                .Include(a => a.History.Select(w => w.Sets))
                .Include(a => a.History.Select(w => w.Sets.Select(s => s.Repetitions)))
                .SingleOrDefault(a => a.ID == id);
        }
    }
}
