using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorModules
{
    public class MonitorModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //var regionManager = containerProvider.Resolve<IRegionManager>();
            //regionManager.RegisterViewWithRegion("MainView", typeof(Views.MonitorPage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
