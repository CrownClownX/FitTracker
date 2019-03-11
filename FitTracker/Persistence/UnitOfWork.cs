using FitTracker.Core;
using FitTracker.Core.Repositories;
using FitTracker.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FitTrackerContext _context;

        public IActivityRepository ActivityRepository { get; private set; }
        public IExerciseRepository ExerciseRepository { get; private set; }
        public IWorkoutRepository WorkoutRepository { get; private set; }

        public UnitOfWork(FitTrackerContext context)
        {
            _context = context;
            ActivityRepository = new ActivityRepository(_context);
            ExerciseRepository = new ExerciseRepository(_context);
            WorkoutRepository = new WorkoutRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
