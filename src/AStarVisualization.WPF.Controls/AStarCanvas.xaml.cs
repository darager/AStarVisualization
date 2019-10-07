using AStarVisualization.Core;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AStarVisualization.WPF.Controls
{
    /// <summary>
    /// Interaction logic for AStarCanvas.xaml
    /// </summary>
    public partial class AStarCanvas : UserControl
    {
        //public static readonly DependencyProperty MapProperty =
        //    DependencyProperty.Register(
        //        "Map", typeof(Node[,]), typeof(AStarCanvas),
        //    new PropertyMetadata(null));
        //public static readonly DependencyProperty PathProperty =
        //    DependencyProperty.Register(
        //        "Path", typeof(List<Node>), typeof(AStarCanvas),
        //    new PropertyMetadata(new List<Node>()));
        //public Node[,] Map
        //{
        //    get => (Node[,])GetValue(MapProperty);
        //    set => SetValue(MapProperty, value);
        //}
        //public List<Node> Path
        //{
        //    get => (List<Node>)GetValue(PathProperty);
        //    set => SetValue(PathProperty, value);
        //}

        // snipet for bindable properties propdbd


        public Node[,] MyProperty
        {
            get { return (Node[,])GetValue(MapProperty); }
            set { SetValue(MapProperty, value); }
        }
        public static readonly DependencyProperty MapProperty =
            DependencyProperty.Register("MyProperty", typeof(Node[,]), typeof(AStarCanvas), new PropertyMetadata(0));


        public AStarCanvas()
        {
            InitializeComponent();
        }
    }
}
