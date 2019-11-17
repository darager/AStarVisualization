using System.Reflection;
using System.Windows;
using Ninject;

namespace PathFindingVisualization.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var window = kernel.Get<MainWindow>();
            window.Show();
        }
    }
}
