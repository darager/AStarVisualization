using System.Reflection;
using System.Windows;
using Ninject;
using PathFindingVisualization.WPF.ViewModels;

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
            AlgorithmControls.DataContext = kernel.Get<AlgorithmControlViewModel>();
        }
    }
}
