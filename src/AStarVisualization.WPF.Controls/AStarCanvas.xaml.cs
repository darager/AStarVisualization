using AStarVisualization.Core;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

// snipet for bindable properties propdbd
namespace AStarVisualization.WPF.Controls
{
    public partial class AStarCanvas : UserControl
    {
        public static readonly DependencyProperty MapProperty =
            DependencyProperty.Register(
                "Map", typeof(Node[,]), typeof(AStarCanvas));
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(
                "Path", typeof(List<Node>), typeof(AStarCanvas));
        public Node[,] Map
        {
            get => (Node[,])GetValue(MapProperty);
            set => SetValue(MapProperty, value);
        }
        public List<Node> Path
        {
            get => (List<Node>)GetValue(PathProperty);
            set => SetValue(PathProperty, value);
        }

        public AStarCanvas()
        {
            InitializeComponent();
        }
    }
}
