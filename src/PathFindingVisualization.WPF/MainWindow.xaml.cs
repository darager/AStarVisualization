using PathFindingVisualization.WPF.ViewModels;
using Ninject;
using System.Reflection;
using System.Windows;

namespace PathFindingVisualization.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            DataContext = kernel.Get<MainViewModel>();
        }
    }
}
