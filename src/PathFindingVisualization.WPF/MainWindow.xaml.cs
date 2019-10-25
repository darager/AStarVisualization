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

            // TODO: use xaml to inject the viewmodels instead
            var mapViewModel = new MapCanvasViewModel();
            mapCanvas.DataContext = mapViewModel;

            // TODO: remove mock data
            mapViewModel.Map = new Core.Map.Map(50, 50);
        }
    }
}
