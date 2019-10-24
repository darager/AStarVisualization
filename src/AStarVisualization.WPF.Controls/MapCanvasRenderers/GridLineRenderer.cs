using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AStarVisualization.WPF.Controls.MapCanvasRenderers
{
    public class GridLineRenderer
    {
        private List<Line> GridLines = new List<Line>();

        public GridLineRenderer(MapCanvas canvas)
        {
            foreach (Line line in GridLines)
                canvas.Children.Add(line);
        }

        public void HandleMapChange(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var canvas = (MapCanvas)source;

            double height = canvas.ActualHeight;
            double width = canvas.ActualWidth;
            double rowSpacing = height / canvas.NumRows;
            double colSpacing = width / canvas.NumColumns;

            var stroke = new SolidColorBrush(Colors.DarkGray);
            int lineThickness = 1;

            var newLines = new List<Line>();
            for (int i = 0; i < canvas.NumRows; i++)
            {
                double y = i * rowSpacing + rowSpacing;
                var rowLine = new Line()
                {
                    X1 = 0,
                    X2 = width,
                    Y1 = y,
                    Y2 = y,
                    Stroke = stroke,
                    StrokeThickness = lineThickness
                };

                newLines.Add(rowLine);
            }
            for (int i = 0; i < canvas.NumColumns; i++)
            {
                double x = i * colSpacing + colSpacing;
                var rowLine = new Line()
                {
                    X1 = x,
                    X2 = x,
                    Y1 = 0,
                    Y2 = height,
                    Stroke = stroke,
                    StrokeThickness = lineThickness
                };

                newLines.Add(rowLine);
            }

            List<Line> oldLines = GridLines;
            newLines.ForEach(line => canvas.Children.Add(line));
            oldLines.ForEach(line => canvas.Children.Remove(line));

            GridLines = newLines;
        }
    }
}
