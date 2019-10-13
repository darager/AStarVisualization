using AStarVisualization.Core;
using AStarVisualization.WPF.Cleanup.ViewModels;
using Ninject;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace AStarVisualization.WPF.Cleanup
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            // TODO: use xaml to inject the viewmodels instead
            var astarVM = new AStarGridViewModel();
            mapCanvas.DataContext = astarVM;
        }
    }
}
