using AStarVisualization.Core;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AStarVisualization.WPF.Controls.MapCanvasRenderers
{
    public class PathRenderer
    {
        private Polyline PathLine = new Polyline();

        public PathRenderer(MapCanvas canvas)
        {
            canvas.Children.Add(PathLine);
            // ensures that the PathLine is in the foreground
            Panel.SetZIndex(PathLine, 100);
        }

        public void HandlePathChange(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var canvas = (MapCanvas)source;
            var path = (List<Node>)e.NewValue;

            double rowSpacing = canvas.ActualHeight / canvas.NumRows;
            double colSpacing = canvas.ActualWidth / canvas.NumColumns;

            var points = new PointCollection();
            foreach (Node node in path)
            {
                double x = (node.ColIndex * colSpacing) + (colSpacing / 2);
                double y = (node.RowIndex * rowSpacing) + (rowSpacing / 2);

                points.Add(new Point(x, y));
            }
            PathLine.Points = points;

            PathLine.Stroke = new SolidColorBrush(Colors.Black);
            PathLine.StrokeThickness = Math.Min(rowSpacing, colSpacing) * 0.2;
        }
    }
}
