using FitTracker.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Core
{
    public interface IUnitOfWork
    {
        IActivityRepository ActivityRepository { get; }
        IExerciseRepository ExerciseRepository { get; }
        IWorkoutRepository WorkoutRepository { get; }

        int Complete();
    }
}
