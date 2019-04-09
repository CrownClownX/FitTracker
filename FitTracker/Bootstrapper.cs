using CommonServiceLocator;
using FitTracker.Core;
using FitTracker.Persistence;
using FitTracker.ViewModels;
using FitTracker.Views;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity.Injection;
using Unity.Lifetime;
using Unity;
using Prism.Modularity;

namespace FitTracker
{
    class Bootstrapper : UnityBootstrapper
    {
        //Specify top level window for a Prism application, in this case it is MainWindow
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<MainWindow>();
        }

        //Creating and setting main window of application
        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        //Place for dependency registration
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterTypeForNavigation<ScheduleView>("Schedule");
            Container.RegisterTypeForNavigation<Edit>("Edit");
            Container.RegisterTypeForNavigation<NewWorkoutView>("NewWorkout");
            Container.RegisterTypeForNavigation<ActivityView>("Activity");
            Container.RegisterTypeForNavigation<HistoryView>("History");
            Container.RegisterTypeForNavigation<ExerciseView>("Exercise");

            Container.RegisterInstance<FitTrackerContext>(new FitTrackerContext());
            Container.RegisterType<IUnitOfWork, UnitOfWork>(new ContainerControlledLifetimeManager());
        }
    }
}
