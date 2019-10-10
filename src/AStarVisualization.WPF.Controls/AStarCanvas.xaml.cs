using AStarVisualization.Core;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

// TODO: implement the rendering through the callbacks
// https://docs.microsoft.com/en-us/dotnet/api/system.windows.dependencypropertychangedeventargs?view=netframework-4.8
namespace AStarVisualization.WPF.Controls
{
    public partial class AStarCanvas : UserControl
    {
        public static readonly DependencyProperty MapProperty =
            DependencyProperty.Register(
                "Map", typeof(Node[,]), typeof(AStarCanvas),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnMapChanged)));

        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(
                "Path", typeof(List<Node>), typeof(AStarCanvas),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPathChanged)));

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

        public AStarCanvas() => InitializeComponent();

        private static void OnMapChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: render the grid including the lines and the tiles
            if(e.NewValue.GetType() == typeof(List<Node>))
            {

            }
            else if(e.NewValue.GetType() == typeof(Node))
            {

            }
        }
        private static void OnPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            List<Node> path = (List<Node>)e.NewValue;
            // TODO: Render the path
        }
    }
}
