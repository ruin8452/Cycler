using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingModules
{
    public class SettingModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //var regionManager = containerProvider.Resolve<IRegionManager>();
            //regionManager.RegisterViewWithRegion("MainView", typeof(Views.MainSettingPage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
