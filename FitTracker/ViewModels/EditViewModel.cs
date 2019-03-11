using FitTracker.Core;
using FitTracker.Core.Model;
using FitTracker.Helper;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTracker.ViewModels
{
    public class EditViewModel : NotifyPropertyChanged, INavigationAware
    {
        private Activity _activity;
        private List<DayOfWeek> _daysOfWeek;

        private Exercise _newTemplateExercise;
        private Exercise _selectedTemplateExercise;

        private ObservableCollection<Exercise> _templateWorkoutExercises;
        private ObservableCollection<Exercise> _allExercises;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegionManager _regionManager;

        public EditViewModel(IUnitOfWork unitOfWork, IRegionManager regionManager)
        {
            _unitOfWork = unitOfWork;
            _regionManager = regionManager;
            AddToCurrentCommand = new DelegateCommand(AddToCurrent);
            RemoveFromCurrentCommand = new DelegateCommand(RemoveFromCurrent);
            SaveToDbCommand = new DelegateCommand(SaveToDb);

            _daysOfWeek = Enum.GetValues(typeof(DayOfWeek))
                .OfType<DayOfWeek>()
                .ToList();
        }

        private void AddToCurrent()
        {
            if (NewTemplateExercise == null)
                return;

            var ifexsist = TemplateWorkoutExercises
                .SingleOrDefault(t => t.ID == NewTemplateExercise.ID);

            if (ifexsist == null)
            {
                TemplateWorkoutExercises.Add(NewTemplateExercise);
                NewTemplateExercise = null;
            }
        }

        private void RemoveFromCurrent()
        {
            if (SelectedTemplateExercise != null)
                TemplateWorkoutExercises.Remove(SelectedTemplateExercise);
        }

        private void SaveToDb()
        {
            if (Activity.Name == null && Activity.Name.Count() == 0)
                return;

            if(Activity.WorkoutTemplate == null)
                Activity.WorkoutTemplate = new Template();

            Activity.WorkoutTemplate.Exercises = TemplateWorkoutExercises;

            if (Activity.ID == 0)
                _unitOfWork.ActivityRepository.Add(Activity);
            
            _unitOfWork.Complete();

            var parameter = new NavigationParameters
            {
                { "HasChanged", true }
            };

            _regionManager.RequestNavigate("MainRegion", "Schedule", parameter);
        }

        public DelegateCommand AddToCurrentCommand { get; private set; }
        public DelegateCommand RemoveFromCurrentCommand { get; private set; }
        public DelegateCommand SaveToDbCommand { get; set; }

        public Activity Activity
        {
            get { return _activity; }
            set
            {
                _activity = value;
                OnPropertyChanged("Activity");
            }
        }

        public Exercise NewTemplateExercise
        {
            get { return _newTemplateExercise; }
            set
            {
                _newTemplateExercise = value;
                OnPropertyChanged("NewTemplateExercise");
            }
        }

        public Exercise SelectedTemplateExercise
        {
            get { return _selectedTemplateExercise; }
            set
            {
                _selectedTemplateExercise = value;
                OnPropertyChanged("SelectedTemplateExercise");
            }
        }

        public ObservableCollection<Exercise> TemplateWorkoutExercises
        {
            get { return _templateWorkoutExercises; }
            set
            {
                _templateWorkoutExercises = value;
                OnPropertyChanged("TemplateWorkoutExercises");
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

        public List<DayOfWeek> DaysOfWeek
        {
            get => _daysOfWeek;
            set
            {
                _daysOfWeek = value;
                OnPropertyChanged("DaysOfWeek");
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var data = navigationContext.Parameters.GetValue<int>("Activity");

            Activity = data != 0 
                ? _unitOfWork.ActivityRepository.Get(data)
                : new Activity();

            TemplateWorkoutExercises = Activity.WorkoutTemplate != null
                ? new ObservableCollection<Exercise>(Activity.WorkoutTemplate.Exercises)
                : new ObservableCollection<Exercise>();

            AllExercises = new ObservableCollection<Exercise>(_unitOfWork.ExerciseRepository.GetAll());
        }
    
    }
}
