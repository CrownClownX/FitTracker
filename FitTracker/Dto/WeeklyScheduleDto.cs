using FitTracker.Core.Model;
using FitTracker.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Dto
{
    public class WeeklyScheduleDto : NotifyPropertyChanged
    {
        private ObservableCollection<Activity> _monday;
        private ObservableCollection<Activity> _tuesday;
        private ObservableCollection<Activity> _wednesday;
        private ObservableCollection<Activity> _thursday;
        private ObservableCollection<Activity> _friday;
        private ObservableCollection<Activity> _saturday;
        private ObservableCollection<Activity> _sunday;

        public WeeklyScheduleDto(List<Activity> activities)
        {
            Monday = new ObservableCollection<Activity>(QueryActivities(activities, DayOfWeek.Monday));
            Tuesday = new ObservableCollection<Activity>(QueryActivities(activities, DayOfWeek.Tuesday));
            Wednesday = new ObservableCollection<Activity>(QueryActivities(activities, DayOfWeek.Wednesday));
            Thursday = new ObservableCollection<Activity>(QueryActivities(activities, DayOfWeek.Thursday));
            Friday = new ObservableCollection<Activity>(QueryActivities(activities, DayOfWeek.Friday));
            Saturday = new ObservableCollection<Activity>(QueryActivities(activities, DayOfWeek.Saturday));
            Sunday = new ObservableCollection<Activity>(QueryActivities(activities, DayOfWeek.Sunday));
        }

        private List<Activity> QueryActivities(List<Activity> activities, DayOfWeek dayOfWeek)
        {
            if(activities == null)
            {
                return new List<Activity>();
            }

            return activities.Where(a => a.DayOfWeek == dayOfWeek).ToList();
        }

        public ObservableCollection<Activity> Monday
        {
            get => _monday;
            set
            {
                _monday = value;
                OnPropertyChanged("Monday");
            }
        }

        public ObservableCollection<Activity> Tuesday
        {
            get => _tuesday;
            set
            {
                _tuesday = value;
                OnPropertyChanged("Tuesday");
            }
        }

        public ObservableCollection<Activity> Wednesday
        {
            get => _wednesday;
            set
            {
                _wednesday = value;
                OnPropertyChanged("Wednesday");
            }
        }
        public ObservableCollection<Activity> Thursday
        {
            get => _thursday;
            set
            {
                _thursday = value;
                OnPropertyChanged("Thursday");
            }
        }
        public ObservableCollection<Activity> Friday
        {
            get => _friday;
            set
            {
                _friday = value;
                OnPropertyChanged("Friday");
            }
        }
        public ObservableCollection<Activity> Saturday
        {
            get => _saturday;
            set
            {
                _saturday = value;
                OnPropertyChanged("Saturday");
            }
        }
        public ObservableCollection<Activity> Sunday
        {
            get => _sunday;
            set
            {
                _sunday = value;
                OnPropertyChanged("Sunday");
            }
        }
    }
}
