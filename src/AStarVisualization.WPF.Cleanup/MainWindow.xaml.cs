using AStarVisualization.Core;
using AStarVisualization.Core.Map;
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
            var mapViewModel = new MapCanvasViewModel();
            mapCanvas.DataContext = mapViewModel;

            // TODO: remove the testing Timer
            #region testing autoupdating of the MapCanvas
            var timer = new System.Timers.Timer()
            {
                Interval = 2000,
                AutoReset = false,
            };
            timer.Elapsed += (s, e) =>
            {
                //TODO: make sure that the mapview is updated by the change in the viewmodel
                mapViewModel.AStarMap = new Map(2, 3)
                {
                    Data = new Node[][]
                    {
                        new Node[] { new Node(NodeState.Wall), new Node(NodeState.Ground), new Node(NodeState.Wall)},
                        new Node[] { new Node(NodeState.Start), new Node(NodeState.Ground), new Node(NodeState.Ground)},
                        new Node[] { new Node(NodeState.Wall), new Node(NodeState.Ground), new Node(NodeState.Wall)},
                        new Node[] { new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground)},
                        new Node[] { new Node(NodeState.Wall), new Node(NodeState.Ground), new Node(NodeState.Goal)},
                    }
                };

                Map map = mapViewModel.AStarMap;
                mapViewModel.AStarPath = new List<Node>() { map[0, 0], map[0, 1], map[1, 1] };

                map[1, 1].State = NodeState.GroundToBeVisited; // this should call the propertychanged of one of the nodes
            };
            timer.Start();

            mapViewModel.AStarMap[1, 1].State = NodeState.GroundVisited;
            #endregion
        }
    }
}
