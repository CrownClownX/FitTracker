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

namespace FitTracker.ViewModels
{
    public class ExerciseViewModel : NotifyPropertyChanged, INavigationAware
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegionManager _regionManager;

        private ObservableCollection<Exercise> _exercises;
        private Exercise _selectedExercise;
        private string _newExercise;

        public ExerciseViewModel(IUnitOfWork unitOfWork, IRegionManager regionManager)
        {
            _unitOfWork = unitOfWork;
            _regionManager = regionManager;

            AddExerciseCommand = new DelegateCommand(AddExercise);
            RemoveExerciseCommand = new DelegateCommand(RemoveExercise);
        }

        public DelegateCommand AddExerciseCommand { get; set; }
        public DelegateCommand RemoveExerciseCommand { get; set; }

        public ObservableCollection<Exercise> Exercises
        {
            get => _exercises;
            set
            {
                _exercises = value;
                OnPropertyChanged("Exercises");
            }
        }

        public Exercise SelectedExercise
        {
            get => _selectedExercise;
            set
            {
                _selectedExercise = value;
                OnPropertyChanged("SelectedExercise");
            }
        }

        public string NewExercise
        {
            get => _newExercise;
            set
            {
                _newExercise = value;
                OnPropertyChanged("NewExercise");
            }
        }

        private void AddExercise()
        {
            if (NewExercise == null || NewExercise.Count() == 0)
                return;

            var exercise = new Exercise { Name = NewExercise };

            Exercises.Add(exercise);

            _unitOfWork.ExerciseRepository.Add(exercise);
            _unitOfWork.Complete();
        }

        private void RemoveExercise()
        {
            if (SelectedExercise == null)
                return;

            _unitOfWork.ExerciseRepository.Remove(SelectedExercise);
            _unitOfWork.Complete();

            Exercises.Remove(SelectedExercise);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            NewExercise = "";
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Exercises = new ObservableCollection<Exercise>
                (_unitOfWork.ExerciseRepository.GetAll());
        }
    }
}
