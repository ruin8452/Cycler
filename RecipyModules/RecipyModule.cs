using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipyModules
{
    public class RecipyModule : IModule
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
