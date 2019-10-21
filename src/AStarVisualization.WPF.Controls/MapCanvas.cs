using AStarVisualization.Core;
using AStarVisualization.Core.Map;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AStarVisualization.WPF.Controls
{
    public class MapCanvas : Canvas
    {
        public Map Map
        {
            get => (Map)GetValue(MapProperty);
            set => SetValue(MapProperty, value);
        }
        public static readonly DependencyProperty MapProperty =
            DependencyProperty.Register(
                "Map", typeof(Map), typeof(MapCanvas),
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

        public int NumRows => ((Map)GetValue(MapProperty)).GetLength(0);
        public int NumColumns => ((Map)GetValue(MapProperty)).GetLength(1);

        public Polyline PathLine = new Polyline();
        public List<Line> GridLines = new List<Line>();
        public Rectangle[,] GridTiles = new Rectangle[0,0];

        public MapCanvas()
        {
            this.Children.Add(PathLine);
            foreach (Line line in GridLines)
                this.Children.Add(line);
            foreach (Rectangle tile in GridTiles)
                this.Children.Add(tile);
        }

        private static void OnMapChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var newMap = e.NewValue as Map;
            var oldMap = e.OldValue as Map;
            if (oldMap != null)
                foreach (Node[] nodes in oldMap)
                    foreach (Node node in nodes)
                        node.PropertyChanged -= RenderGridTile;
            foreach (Node[] nodes in newMap)
                foreach (Node node in nodes)
                    node.PropertyChanged += RenderGridTile;

            RenderGridLines(source, e);
        }
        private static void RenderGridLines(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var canvas = source as MapCanvas;

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

            List<Line> oldLines = canvas.GridLines;
            foreach (Line line in newLines)
                canvas.Children.Add(line);
            foreach (Line line in oldLines)
                canvas.Children.Remove(line);

            canvas.GridLines = newLines;
        }
        private static void RenderGridTiles()
        {
        }
        private static void RenderGridTile(object sender, PropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        private static void RenderTile(MapCanvas mapCanvas, Canvas canvas, Node node)
        {
            double height = canvas.ActualHeight;
            double width = canvas.ActualWidth;
            double rowSpacing = height / mapCanvas.NumRows;
            double colSpacing = width / mapCanvas.NumColumns;

            int gridLineThickness = 1;
            double x = colSpacing * node.ColIndex;
            double y = rowSpacing * node.RowIndex;

            var rectangle = new Rectangle()
            {
                Height = rowSpacing - gridLineThickness * 2,
                Width = colSpacing - gridLineThickness * 2,
            };
            Canvas.SetTop(rectangle, y + gridLineThickness);
            Canvas.SetLeft(rectangle, x + gridLineThickness);
        }

        private static void OnPathChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var canvas = source as MapCanvas;
            var path = (List<Node>)e.NewValue;

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
            pathLine.Stroke = new SolidColorBrush(Colors.Black);
            pathLine.StrokeThickness = 2;

            canvas.PathLine = pathLine;
        }
    }
}
