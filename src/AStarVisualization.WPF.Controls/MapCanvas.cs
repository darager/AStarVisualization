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
        public AStarMap Map
        {
            get => (AStarMap)GetValue(MapProperty);
            set => SetValue(MapProperty, value);
        }
        public static readonly DependencyProperty MapProperty =
            DependencyProperty.Register(
                "Map", typeof(AStarMap), typeof(MapCanvas),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnMapChanged)));

        public List<Node> Path
        {
            get => (List<Node>)GetValue(PathProperty);
            set => SetValue(PathProperty, value);
        }
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(
                "Path", typeof(List<Node>), typeof(MapCanvas),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPathChanged)));

        public int NumRows => ((AStarMap)GetValue(MapProperty)).GetLength(0);
        public int NumColumns => ((AStarMap)GetValue(MapProperty)).GetLength(1);

        // canvas elements
        public Polyline PathLine = new Polyline();
        public List<Line> GridLines = new List<Line>();

        public MapCanvas()
        {
            this.Children.Add(PathLine);
            foreach (Line line in GridLines)
                this.Children.Add(line);
        }

        private static void OnMapChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            // TODO: render the grid including the lines and the tiles
            if (e.NewValue.GetType() == typeof(AStarMap))
                RedrawGrid();
            else if (e.NewValue.GetType() == typeof(Node))
            {

            }

            void RedrawGrid()
            {
                MapCanvas canvas = source as MapCanvas;
                AStarMap map = (AStarMap)e.NewValue;

                double height = canvas.ActualHeight;
                double width = canvas.ActualWidth;
                double rowSpacing = height / canvas.NumRows;
                double colSpacing = width / canvas.NumColumns;
                int numRows = canvas.NumRows;
                int numCols = canvas.NumColumns;

                var stroke = new SolidColorBrush(Colors.DarkGray);
                int thickness = 1;

                var newLines = new List<Line>();
                for (int i = 0; i < numRows; i++)
                {
                    double Y = i * rowSpacing + rowSpacing;
                    var rowLine = new Line()
                    {
                        X1 = 0,
                        X2 = width,
                        Y1 = Y,
                        Y2 = Y,
                        Stroke = stroke,
                        StrokeThickness = thickness
                    };

                    newLines.Add(rowLine);
                }
                for (int i = 0; i < numCols; i++)
                {
                    double X = i * colSpacing + colSpacing;
                    var rowLine = new Line()
                    {
                        X1 = X,
                        X2 = X,
                        Y1 = 0,
                        Y2 = height,
                        Stroke = stroke,
                        StrokeThickness = thickness
                    };

                    newLines.Add(rowLine);
                }

                var oldLines = canvas.GridLines;
                foreach (Line line in newLines)
                    canvas.Children.Add(line);
                foreach (Line line in oldLines)
                    canvas.Children.Remove(line);

                canvas.GridLines = newLines;
            }
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

            canvas.PathLine = pathLine;
        }
    }
}
