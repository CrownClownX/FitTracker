using FitTracker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Dto
{
    public class KeyValuePair : NotifyPropertyChanged
    {
        private string _name;
        private string _repetitions;

        public string Repetitions
        {
            get => _repetitions;
            set
            {
                _repetitions = value;
                OnPropertyChanged("Repetition");
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
    }
}
