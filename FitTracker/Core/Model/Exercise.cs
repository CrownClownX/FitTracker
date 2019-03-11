using FitTracker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Core.Model
{
    [Serializable]
    public class Exercise : NotifyPropertyChanged
    {
        public Exercise()
        {
            _sets = new List<Set>();
            workoutTemplates = new List<Template>();
        }

        private int id;
        private string name;

        [field: NonSerialized]
        private ICollection<Set> _sets;

        [field: NonSerialized]
        private ICollection<Template> workoutTemplates;

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public ICollection<Set> Sets
        {
            get => _sets;
            set
            {
                _sets = value;
                OnPropertyChanged("Sets");
            }
        }
        public ICollection<Template> WorkoutTemplates
        {
            get => workoutTemplates;
            set
            {
                workoutTemplates = value;
                OnPropertyChanged("WorkoutTemplates");
            }
        }
    }
}
