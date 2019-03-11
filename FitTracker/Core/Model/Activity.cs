using FitTracker.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Core.Model
{
    public class Activity : NotifyPropertyChanged
    {
        public Activity()
        {
        }

        private int id;
        private string name;
        private DayOfWeek dayOfWeek;

        private int? workoutTemplateID;
        private Template workoutTemplate;

        private List<int> historyIds;
        private List<Workout> history;

        public int ID {
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

        public DayOfWeek DayOfWeek
        {
            get
            {
                return dayOfWeek;
            }
            set
            {
                dayOfWeek = value;
                OnPropertyChanged("DayOfWeek");
            }
        }

        public Template WorkoutTemplate
        {
            get
            {
                return workoutTemplate;
            }
            set
            {
                workoutTemplate = value;
                OnPropertyChanged("WorkoutTemplate");
            }
        }

        public List<int> HistoryIds
        {
            get
            {
                return historyIds;
            }
            set
            {
                historyIds = value;
                OnPropertyChanged("HistoryIds");
            }
        }

        public List<Workout> History
        {
            get
            {
                return history;
            }
            set
            {
                history = value;
                OnPropertyChanged("History");
            }
        }

        public int? WorkoutTemplateID
        {
            get => workoutTemplateID;
            set
            {
                workoutTemplateID = value;
                OnPropertyChanged("WorkoutTemplateID");
            }
        }
    }
}
