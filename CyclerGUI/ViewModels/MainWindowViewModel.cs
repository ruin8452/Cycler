using MonitorModules.Views;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using RecipyModules.Views;
using SettingModules.Views;

namespace CyclerGUI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IContainerExtension Container;
        IRegionManager RegionManager;
        IRegion MainViewRegion;

        MonitorPage monitorPage;
        RecipyPage recipyPage;
        MainSettingPage mainSettingPage;


        public DelegateCommand LoadCommand { get; set; }
        public DelegateCommand<object> SelectionChangedCommand { get; set; }

        public MainWindowViewModel(IContainerExtension container, IRegionManager regionManager)
        {
            Container = container;
            RegionManager = regionManager;

            monitorPage = Container.Resolve<MonitorPage>();
            recipyPage = Container.Resolve<RecipyPage>();
            mainSettingPage = Container.Resolve<MainSettingPage>();

            LoadCommand = new DelegateCommand(OnLoaded);
            SelectionChangedCommand = new DelegateCommand<object>(OnSelectionChanged);
        }

        private void OnLoaded()
        {
            MainViewRegion = RegionManager.Regions["MainView"];

            MainViewRegion.Add(monitorPage);
            MainViewRegion.Add(recipyPage);
            MainViewRegion.Add(mainSettingPage);
        }

        private void OnSelectionChanged(object selectedIndex)
        {
            switch((int)selectedIndex)
            {
                case 0:
                    MainViewRegion.Activate(monitorPage);
                    break;
                case 1:
                    MainViewRegion.Activate(recipyPage);
                    break;
                case 3:
                    MainViewRegion.Activate(mainSettingPage);
                    break;
            }
        }
    }
}
