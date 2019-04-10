using FitTracker.Core;
using FitTracker.Core.Model;
using FitTracker.Helper;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;

namespace FitTracker.ViewModels
{
    public class NewWorkoutViewModel : NotifyPropertyChanged
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        private Activity _activity;
        private Workout _newWorkout;
        private Exercise _newExercise;
        private Set _selectedSet;
        private string _customeNewExercise;

        private ObservableCollection<Set> _sets;
        private ObservableCollection<Exercise> _allExercises;

        private Repetition _newRepetition;
        private Repetition _selectedRepetition;

        public NewWorkoutViewModel(IUnitOfWork unitOfWork, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _unitOfWork = unitOfWork;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _newExercise = new Exercise();
            _newRepetition = new Repetition();
            _newWorkout = new Workout();

            AddWorkoutCommand = new DelegateCommand(AddWorkout);
            AddExerciseCommand = new DelegateCommand(AddExercise);
            RemoveExerciseCommand = new DelegateCommand(RemoveExercise);
            AddRepetitionCommand = new DelegateCommand(AddRepetition);
            RemoveRepetitionCommand = new DelegateCommand(RemoveRepetition);

            _eventAggregator.GetEvent<ActivityEvent>().Subscribe(GetActivity);
        }

        public DelegateCommand AddWorkoutCommand { get; private set; }
        public DelegateCommand AddExerciseCommand { get; private set; }
        public DelegateCommand RemoveExerciseCommand { get; private set; }
        public DelegateCommand AddRepetitionCommand { get; private set; }
        public DelegateCommand RemoveRepetitionCommand { get; private set; }

        public Activity Activity
        {
            get { return _activity; }
            set
            {
                _activity = value;
                OnPropertyChanged("Activity");
            }
        }

        public Workout NewWorkout
        {
            get { return _newWorkout; }
            set
            {
                _newWorkout = value;
                OnPropertyChanged("NewWorkout");
            }
        }

        public Exercise NewExercise
        {
            get { return _newExercise; }
            set
            {
                _newExercise = value;
                OnPropertyChanged("NewExercise");

                if (_newExercise != null)
                {
                    Sets.Add(new Set { Exercise = value.Name });
                }
            }
        }

        public string CustomeNewExercise
        {
            get => _customeNewExercise;
            set
            {
                _customeNewExercise = value;
                OnPropertyChanged("CustomeNewExercise");
            }
        }

        public Set SelectedSet
        {
            get { return _selectedSet; }
            set
            {
                _selectedSet = value;
                OnPropertyChanged("SelectedSet");
            }
        }

        public ObservableCollection<Set> Sets
        {
            get { return _sets; }
            set
            {
                _sets = value;
                OnPropertyChanged("Sets");
            }
        }

        public ObservableCollection<Exercise> AllExercises
        {
            get => _allExercises;
            set
            {
                _allExercises = value;
                OnPropertyChanged("AllExercises");
            }
        }

        public Repetition NewRepetition
        {
            get => _newRepetition;
            set
            {
                _newRepetition = value;
                OnPropertyChanged("NewRepetition");
            }
        }

        public Repetition SelectedRepetition
        {
            get => _selectedRepetition;
            set
            {
                _selectedRepetition = value;
                OnPropertyChanged("SelectedRepetition");
            }
        }

        private void AddWorkout()
        {
            if (Sets.Count() == 0)
                return;

            NewWorkout.Sets = Sets.ToList();
            Activity.History.Add(NewWorkout);

            Sets = GetTemplateExercise();
            NewWorkout = new Workout();

            _unitOfWork.Complete();
        }

        private void AddExercise()
        {
            if (CustomeNewExercise == null || CustomeNewExercise.Count() == 0)
                return;

            Sets.Add(new Set { Exercise = CustomeNewExercise});

            CustomeNewExercise = "";
        }

        private void RemoveExercise()
        {
            if (SelectedSet != null)
                Sets.Remove(SelectedSet);
        }

        private void AddRepetition()
        {
            if (SelectedSet == null)
                return;

            SelectedSet.Repetitions.Add(NewRepetition.Clone());
            NewRepetition.NumberOfRepetitions = 0;
        }

        private void RemoveRepetition()
        {
            if(SelectedSet != null && SelectedRepetition != null)
                SelectedSet.Repetitions.Remove(SelectedRepetition);
        }

        private void GetActivity(int id)
        {
            Activity = _unitOfWork.ActivityRepository.Get(id);

            if (Activity == null)
                _regionManager.RequestNavigate("MainRegion", "Schedule");
            else
            {
                Sets = GetTemplateExercise();
                AllExercises = new ObservableCollection<Exercise>(
                    _unitOfWork.ExerciseRepository.GetAll());
            }
        }

        private ObservableCollection<Set> GetTemplateExercise()
        {
            if (Activity == null)
                return null;

            return Activity.WorkoutTemplate != null
                    ? new ObservableCollection<Set>(Activity.WorkoutTemplate.Exercises
                        .Select(e => new Set { Exercise = e.Name }))
                    : new ObservableCollection<Set>();
        }
    }
}
