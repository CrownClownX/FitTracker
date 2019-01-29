using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Core.Model
{
    public class Activity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

        public Workout CurrentWorkoutId { get; set; }
        public Workout CurrentWorkout { get; set; }

        public List<Workout> HistoryIds { get; set; }
        public List<Workout> History { get; set; }
    }
}
