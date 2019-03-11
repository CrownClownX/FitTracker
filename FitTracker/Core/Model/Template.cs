using FitTracker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Core.Model
{
    [Serializable]
    public class Template : NotifyPropertyChanged
    {
        private int _id;
        private ICollection<Exercise> _exercises;

        public Template()
        {
            _exercises = new List<Exercise>();
        }

        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        public ICollection<Exercise> Exercises
        {
            get { return _exercises; }
            set
            {
                _exercises = value;
                OnPropertyChanged("Exercises");
            }
        }

        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
