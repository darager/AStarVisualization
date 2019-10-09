using AStarVisualization.WPF.Cleanup.ViewModels;
using Ninject;
using System.Reflection;
using System.Windows;

namespace AStarVisualization.WPF.Cleanup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var testvm = new TestViewModel();
            customTestControl.DataContext = testvm;
        }
    }
}
