using AStarVisualization.Core;
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

            //customTestControl.DataContext = new TestViewModel();

            var astarVM = new AStarGridViewModel();

            // TODO remove this (only for testing purposes
            astarVM.AStarMap.Map = new Core.Node[,]
            {
                { new Core.Node(NodeState.Wall), new Node(NodeState.Goal) },
                { new Core.Node(NodeState.Start), new Node(NodeState.Ground) }
            };

            astarCanvas.DataContext = astarVM;
        }
    }
}
