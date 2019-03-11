using FitTracker.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace FitTracker.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);

            _regionManager.RegisterViewWithRegion("MainRegion", typeof(ScheduleView));
        }

        public void Navigate(string path)
        {
            //Navigation
            if (path != null)
            {
                _regionManager.RequestNavigate("MainRegion", path);
            }
        }
    }
}
