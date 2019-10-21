using AStarVisualization.Core;
using AStarVisualization.Core.Map;
using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AStarVisualization.WPF.Controls.MapCanvasHelper
{
    public class TileRenderer
    {
        public static MapCanvas MapCanvas;
        public Rectangle[,] Tiles { get; private set; }

        public TileRenderer(MapCanvas canvas)
        {
            MapCanvas = canvas;
        }

        public void HandleMapChange(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
        }

        private Rectangle GetTile(Node node, Canvas canvas, MapCanvas mapCanvas)
        {
            double height = canvas.ActualHeight;
            double width = canvas.ActualWidth;
            double rowSpacing = height / mapCanvas.NumRows;
            double colSpacing = width / mapCanvas.NumColumns;

            int gridLineThickness = 1;
            double x = colSpacing * node.ColIndex;
            double y = rowSpacing * node.RowIndex;

            var stroke = new SolidColorBrush(Colors.Black);

            var rectangle = new Rectangle()
            {
                Height = rowSpacing - gridLineThickness * 2,
                Width = colSpacing - gridLineThickness * 2,
                Stroke = stroke,
            };
            Canvas.SetTop(rectangle, y + gridLineThickness);
            Canvas.SetLeft(rectangle, x + gridLineThickness);

            return rectangle;
        }
    }
}
