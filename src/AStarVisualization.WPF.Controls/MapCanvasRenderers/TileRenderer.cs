using AStarVisualization.Core;
using AStarVisualization.Core.Map;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AStarVisualization.WPF.Controls.MapCanvasRenderers
{
    public class TileRenderer
    {
        public Rectangle[,] Tiles { get; private set; }

        public void HandleMapChange(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var canvas = source as MapCanvas;
            var oldMap = (Map)e.OldValue;
            var newMap = (Map)e.NewValue;


            Rectangle[,] oldTiles = Tiles;
            Tiles = new Rectangle[canvas.NumRows, canvas.NumColumns];

            // create the new tiles
            for (int i = 0; i < canvas.NumRows; i++)
                for (int j = 0; j < canvas.NumColumns; j++)
                    Tiles[i, j] = GetRectangle(canvas, newMap[i, j]);

            // add the new Tiles
            foreach (Rectangle tile in Tiles)
                canvas.Children.Add(tile);

            // remove the old Tiles
            if (oldTiles != null)
                foreach (Rectangle tile in oldTiles)
                    canvas.Children.Remove(tile);
        }

        private Rectangle GetRectangle(MapCanvas canvas, Node node)
        {
            var rect = new Rectangle();

            int numrows = canvas.NumRows;
            int numcolumns = canvas.NumColumns;

            double canvasheight = canvas.ActualHeight;
            double canvaswidth = canvas.ActualWidth;

            double rowspacing = canvasheight / numrows;
            double columnspacing = canvaswidth / numcolumns;

            int gridLineThickness = 1;
            rect.Height = rowspacing - 2 * gridLineThickness;
            rect.Width = columnspacing - 2 * gridLineThickness;

            rect.SnapsToDevicePixels = true;
            rect.Fill = GetStateColor(node.State);

            double x = node.ColIndex * columnspacing + gridLineThickness;
            double y = node.RowIndex * rowspacing + gridLineThickness;

            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);

            return rect;
        }
        private SolidColorBrush GetStateColor(NodeState state) // TODO: choose proper colors
        {
            switch (state)
            {
                case NodeState.Ground:
                    return new SolidColorBrush(Colors.Transparent);
                case NodeState.GroundVisited:
                    return new SolidColorBrush(Colors.LightGreen);
                case NodeState.GroundToBeVisited:
                    return new SolidColorBrush(Colors.LightBlue);
                case NodeState.Start:
                    return new SolidColorBrush(Colors.DarkGoldenrod);
                case NodeState.Wall:
                    return new SolidColorBrush(Colors.Black);
                case NodeState.Goal:
                    return new SolidColorBrush(Colors.Red);
                default:
                    return null;
            }

        }
    }
}
