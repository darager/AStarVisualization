using AStarVisualization.Core;
using AStarVisualization.WPF.ViewModels;
using Ninject;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace AStarVisualization.WPF
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

            // timer for testing
            var timer = new System.Timers.Timer()
            {
                Interval = 4000,
                AutoReset = false,
            };
            timer.Elapsed += (s, e) =>
            {
                // TODO: make sure that the mapview is updated by the change in the viewmodel
                var map = astarVM.AStarMap.Map;
                astarVM.AStarPath = new List<Node>() { map[0][0], map[0][1], map[1][1] };
            };
            timer.Start();
        }
    }
}
