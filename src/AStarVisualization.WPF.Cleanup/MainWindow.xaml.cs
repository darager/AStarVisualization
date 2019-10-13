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

            //customTestControl.DataContext = new TestViewModel();

            // TODO remove this (only for testing purposes
            var astarVM = new AStarGridViewModel();

            astarVM.AStarMap.Map = new Core.Node[,]
            {
                { new Core.Node(NodeState.Wall), new Node(NodeState.Goal) },
                { new Core.Node(NodeState.Start), new Node(NodeState.Ground) }
            };
            var map = astarVM.AStarMap;
            astarVM.AStarPath = new List<Node>
            {
                map[1,0], map[1,1], map[0,1]
            };

            mapCanvas.DataContext = astarVM;
        }
    }
}
