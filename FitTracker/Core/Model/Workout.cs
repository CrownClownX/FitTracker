using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Core.Model
{
    public class Workout
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public List<Exercise> ExercisesIds { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}
