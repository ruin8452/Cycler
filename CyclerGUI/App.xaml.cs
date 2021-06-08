using CyclerGUI.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using SettingModules.Models;
using System.IO;
using System.Windows;

namespace CyclerGUI
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            Directory.CreateDirectory("./Settings/");
            Directory.CreateDirectory("./Process/");

            StatusSetting.ReadSetting();
            EquipmentSetting.ReadSetting();
            SafetySetting.ReadSetting();
            DataPathSetting.ReadSetting();

            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }


        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<Title.TitleModule>();
            moduleCatalog.AddModule<SettingModules.SettingModule>();
            moduleCatalog.AddModule<MonitorModules.MonitorModule>();
            moduleCatalog.AddModule<RecipyModules.RecipyModule>();
        }
    }
}
