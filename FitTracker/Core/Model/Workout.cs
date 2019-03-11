using FitTracker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Core.Model
{
    [Serializable]
    public class Workout : NotifyPropertyChanged
    {
        public Workout()
        {
            _sets = new List<Set>();
            Date = DateTime.Now;
        }

        private int id;
        private DateTime date;

        private ICollection<Set> _sets;

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
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }
        public ICollection<Set> Sets
        {
            get
            {
                return _sets;
            }
            set
            {
                _sets = value;
                OnPropertyChanged("Sets");
            }
        }

        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
