using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.WPF.Controls.MapCanvasRenderers
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

        public void RedrawPath(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var canvas = (MapCanvas)source;
            var path = (List<INode>)e.NewValue;

            double gridHeight = canvas.ActualHeight / canvas.NumRows;
            double gridWidth = canvas.ActualWidth / canvas.NumColumns;

            PathLine.Stroke = new SolidColorBrush(Colors.Black);
            PathLine.StrokeThickness = Math.Min(gridHeight, gridWidth) * 0.2;

            var points = new PointCollection();
            foreach (INode node in path)
            {
                double x = (node.ColIndex * gridWidth) + (gridWidth / 2);
                double y = (node.RowIndex * gridHeight) + (gridHeight / 2);

                points.Add(new Point(x, y));
            }
            PathLine.Points = points;
        }
    }
}
