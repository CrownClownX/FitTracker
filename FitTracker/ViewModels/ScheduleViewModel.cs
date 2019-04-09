using FitTracker.Core;
using FitTracker.Core.Model;
using FitTracker.Dto;
using FitTracker.Helper;
using FitTracker.Persistence;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FitTracker.ViewModels
{
    public class ScheduleViewModel : NotifyPropertyChanged, INavigationAware
    {
        private IUnitOfWork _unitOfWork;
        private readonly IRegionManager _regionManager;

        private WeeklyScheduleDto _schedule;

        public ScheduleViewModel(IRegionManager regionManager, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _regionManager = regionManager;

            Schedule = CreateSchedule();
            NavigateCommand = new DelegateCommand<object>(Navigate);
            AddActivityCommand = new DelegateCommand(AddActivity);
            GoToExerciseManagerCommand = new DelegateCommand(GoToExerciseManager);
        }

        public DelegateCommand<object> NavigateCommand { get; private set; }
        public DelegateCommand AddActivityCommand { get; private set; }
        public DelegateCommand GoToExerciseManagerCommand { get; private set; }


        public WeeklyScheduleDto Schedule
        {
            get => _schedule;
            set
            {
                _schedule = value;
                OnPropertyChanged("Schedule");
            }
        }

        public void Navigate(object activity)
        {
            var eventData = int.Parse(string.Format("{0}", activity));

            var parameter = new NavigationParameters
            {
                { "Activity", eventData }
            };

            _regionManager.RequestNavigate("MainRegion", "Activity", parameter);
        }

        private void AddActivity()
        {
            var parameter = new NavigationParameters
            {
                { "Activity", 0 }
            };

            _regionManager.RequestNavigate("MainRegion", "Edit", parameter);
        }

        private void GoToExerciseManager()
        {
            _regionManager.RequestNavigate("MainRegion", "Exercise");
        }

        private WeeklyScheduleDto CreateSchedule()
        {
            var activities = _unitOfWork.ActivityRepository
                .GetAll()
                .ToList();

            return new WeeklyScheduleDto(activities);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var data = navigationContext.Parameters.GetValue<bool>("HasChanged");

            if(data == true)
            {
                Schedule = CreateSchedule();
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
