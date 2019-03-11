using FitTracker.Core;
using FitTracker.Core.Model;
using FitTracker.Dto;
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
    public class HistoryViewModel : NotifyPropertyChanged
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        private Activity _activity;
        private Workout _selectedWorkout;
        private List<KeyValuePair> _exercises;

        public HistoryViewModel(IUnitOfWork unitOfWork, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _unitOfWork = unitOfWork;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _eventAggregator.GetEvent<ActivityEvent>().Subscribe(GetActivity);
            _exercises = new List<KeyValuePair>();
            RemoveWorkoutCommand = new DelegateCommand(RemoveWorkout);
        }

        public DelegateCommand RemoveWorkoutCommand { get; set; }

        public Activity Activity
        {
            get { return _activity; }
            set
            {
                _activity = value;
                OnPropertyChanged("Activity");
            }
        }

        public Workout SelectedWorkout
        {
            get => _selectedWorkout;
            set
            {
                _selectedWorkout = value;
                Exercises = SetExercises();
                OnPropertyChanged("SelectedWorkout");
            }
        }

        public List<KeyValuePair> Exercises
        {
            get => _exercises;
            set
            {
                _exercises = value;
                OnPropertyChanged("Exercises");
            }
        }

        private List<KeyValuePair> SetExercises()
        {
            var exercises = new List<KeyValuePair>();

            if (SelectedWorkout == null)
                return exercises;

            foreach(var set in SelectedWorkout.Sets)
            {
                var newPair = new KeyValuePair
                {
                    Name = set.Exercise.Name,
                    Repetitions = string.Join("/", set.Repetitions.Select(r => r.NumberOfRepetitions))
                };

                exercises.Add(newPair);
            }

            return exercises;
        }

        private void GetActivity(int id)
        {
            var activity = _unitOfWork.ActivityRepository.Get(id);

            if (activity == null)
                _regionManager.RequestNavigate("MainRegion", "Schedule");
        }

        private void RemoveWorkout()
        {
            if (SelectedWorkout == null)
                return;

            Exercises = new List<KeyValuePair>();

            Activity.History.Remove(SelectedWorkout);
            _unitOfWork.WorkoutRepository.Remove(SelectedWorkout);
            _unitOfWork.Complete();
        }
    }
}
