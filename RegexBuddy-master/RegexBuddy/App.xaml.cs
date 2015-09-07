using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;

namespace RegexBuddy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IUnityContainer container = new UnityContainer();

            container.RegisterType<IRegexService, RegexService>();

            container.RegisterType<IMainWindowViewModel, MainWindowViewModel>();


            var mainWindow = container.Resolve<MainWindow>(); // Creating Main window


            mainWindow.Show();
        }
    }
}
