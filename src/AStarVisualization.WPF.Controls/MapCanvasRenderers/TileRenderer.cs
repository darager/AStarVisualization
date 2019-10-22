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

            rect.Stroke = new SolidColorBrush(Colors.Transparent);
            rect.SnapsToDevicePixels = true;

            switch (node.State)
            {
                case NodeState.Ground:
                    rect.Fill = new SolidColorBrush(Colors.Transparent);
                    break;
                case NodeState.GroundVisited:
                    rect.Fill = new SolidColorBrush(Colors.LightGreen);
                    break;
                case NodeState.GroundToBeVisited:
                    rect.Fill = new SolidColorBrush(Colors.LightBlue);
                    break;
                case NodeState.Start:
                    rect.Fill = new SolidColorBrush(Colors.DarkGoldenrod);
                    break;
                case NodeState.Wall:
                    rect.Fill = new SolidColorBrush(Colors.Black);
                    break;
                case NodeState.Goal:
                    rect.Fill = new SolidColorBrush(Colors.Red);
                    break;
                default:
                    break;
            }

            double topdistance = node.RowIndex * rowspacing + gridLineThickness;
            double leftdistance = node.ColIndex * columnspacing + gridLineThickness;

            Canvas.SetLeft(rect, leftdistance);
            Canvas.SetTop(rect, topdistance);

            return rect;
        }
    }
}
