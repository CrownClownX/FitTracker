using FitTracker.Core;
using FitTracker.Core.Model;
using FitTracker.Helper;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.ViewModels
{
    public class ActivityViewModel : NotifyPropertyChanged, INavigationAware
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        private Activity _activity;
        private string _lastWorkout;
        private int _selectedIndex;
        private int _numberOfWorkouts;

        public ActivityViewModel(IUnitOfWork unitOfWork, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _unitOfWork = unitOfWork;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            NavigateToEditCommand = new DelegateCommand<object>(NavigateToEdit);
            RemoveActivityCommand = new DelegateCommand(RemoveActivity);
        }

        public Activity Activity
        {
            get { return _activity; }
            set
            {
                _activity = value;
                OnPropertyChanged("Activity");
            }
        }

        public DelegateCommand<object> NavigateToEditCommand { get; set; }
        public DelegateCommand RemoveActivityCommand { get; set; }

        public string LastWorkout
        {
            get => _lastWorkout;
            set
            {
                _lastWorkout = value;
                OnPropertyChanged("LastWorkout");
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        public int NumberOfWorkouts
        {
            get => _numberOfWorkouts;
            set
            {
                _numberOfWorkouts = value;
                OnPropertyChanged("NumberOfWorkouts");
            }
        }

        private void NavigateToEdit(object path)
        {
            var parameter = new NavigationParameters
            {
                { "Activity", _activity.ID }
            };
            
            _regionManager.RequestNavigate("MainRegion", path.ToString(), parameter);
        }

        private void RemoveActivity()
        {
            _unitOfWork.ActivityRepository.Remove(Activity);
            _unitOfWork.Complete();

            var parameter = new NavigationParameters
            {
                {"HasChanged", true }
            };

            _regionManager.RequestNavigate("MainRegion", "Schedule", parameter);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            SelectedIndex = 0;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            SelectedIndex = 0;
            var data = navigationContext.Parameters.GetValue<int>("Activity");
            Activity = _unitOfWork.ActivityRepository.Get(data);

            if (Activity == null)
            {
                _regionManager.RequestNavigate("MainRegion", "Schedule");
            }
            else
            {
                _eventAggregator.GetEvent<ActivityEvent>().Publish(data);

                LastWorkout = Activity.History != null && Activity.History.Count() > 0
                    ? Activity.History.Max(w => w.Date).ToShortDateString()
                    : "There is no workouts in history";
                NumberOfWorkouts = Activity.History != null 
                    ? Activity.History.Count
                    : 0;
            }
        }
    }
}
