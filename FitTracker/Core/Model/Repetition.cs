using FitTracker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Core.Model
{
    [Serializable]
    public class Repetition : NotifyPropertyChanged
    {
        private int _id;
        private int _numberOfRepetitions;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public int NumberOfRepetitions
        {
            get => _numberOfRepetitions;
            set
            {
                _numberOfRepetitions = value;
                OnPropertyChanged("NumberOfRepetitions");
            }
        }

        public int SetId { get; set; }
        public Set Set { get; set; }
    }
}
