using FitTracker.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Core.Model
{
    [Serializable]
    public class Set : NotifyPropertyChanged
    {
        private ObservableCollection<Repetition> _repetitions;
        private string _exercise;

        public Set()
        {
            Repetitions = new ObservableCollection<Repetition>();
        }

        public int Id { get; set; }

        public ObservableCollection<Repetition> Repetitions
        {
            get => _repetitions;
            set
            {
                _repetitions = value;
                OnPropertyChanged("Repetitions");
            }
        }

        public string Exercise
        {
            get => _exercise;
            set
            {
                _exercise = value;
                OnPropertyChanged("Exercise");
            }
        }

        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
    }
}
