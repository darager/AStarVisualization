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
                Interval = 3000,
                AutoReset = false,
            };
            timer.Elapsed += (s, e) =>
            {
                astarVM.AStarMap.Map = new Node[][]
                {
                    new Node[]{ new Node(NodeState.Wall), new Node(NodeState.Wall) },
                    new Node[]{ new Node(NodeState.Wall), new Node(NodeState.Wall) },
                };
                astarVM.AStarPath = new List<Node>();
            };
            timer.Start();
        }
    }
}
