using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

//Panel.SetZIndex(PathLine, 1); // TODO: make sure this works properly
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

            List<Line> oldLines = GridLines;
            foreach (Line line in newLines)
                canvas.Children.Add(line);
            foreach (Line line in oldLines)
                canvas.Children.Remove(line);

            GridLines = newLines;
        }
    }
}
