using AStarVisualization.Core;
using AStarVisualization.WPF.Controls.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AStarVisualization.WPF.Controls
{
    public class MapCanvas : Canvas
    {
        public static readonly DependencyProperty MapProperty =
            DependencyProperty.Register(
                "Map", typeof(AStarMap), typeof(MapCanvas),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnMapChanged)));
        public AStarMap Map
        {
            get => (AStarMap)GetValue(MapProperty);
            set => SetValue(MapProperty, value);
        }
        private static void OnMapChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: render the grid including the lines and the tiles
            if (e.NewValue.GetType() == typeof(List<Node>))
            {

            }
            else if (e.NewValue.GetType() == typeof(Node))
            {

            }
        }

        public List<Node> Path
        {
            get => (List<Node>)GetValue(PathProperty);
            set => SetValue(PathProperty, value);
        }
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(
                "Path", typeof(List<Node>), typeof(MapCanvas),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPathChanged)));
        public Polyline PathLine;

        public int NumRows => ((AStarMap)GetValue(MapProperty)).GetLength(0);
        public int NumColumns => ((AStarMap)GetValue(MapProperty)).GetLength(1);

        public MapCanvas()
        {
            PathLine = new Polyline();
        }

        private static void OnPathChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) // TODO: Render the path
        {
            MapCanvas canvas = source as MapCanvas;
            List<Node> path = (List<Node>)e.NewValue;

            double rowSpacing = canvas.ActualHeight / canvas.NumRows;
            double colSpacing = canvas.ActualWidth / canvas.NumColumns;

            var points = new PointCollection();
            foreach (Node node in path)
            {
                double x = node.ColIndex * colSpacing + colSpacing / 2;
                double y = node.RowIndex * rowSpacing + rowSpacing / 2;

                points.Add(new Point(x, y));
            }

            var pathLine = canvas.PathLine;
            pathLine.Points = points;
            pathLine.Stroke = new SolidColorBrush(Colors.Black); // TODO: make this bindable
            pathLine.StrokeThickness = 2;

            canvas.Children.Add(pathLine);
        }
    }
}
