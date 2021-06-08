using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Title
{
    public class TitleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("TitleView", typeof(Views.TitleBar));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
