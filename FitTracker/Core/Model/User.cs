using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Core.Model
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<Activity> ActivitiesIds { get; set; }
        public List<Activity> Activities { get; set; }
    }
}
