using AStarVisualization.Core;
using AStarVisualization.Core.Map;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AStarVisualization.WPF.Controls.MapCanvasHelper // TODO: clean up this class
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
            var oldMap = (Map)e.OldValue;
            var newMap = (Map)e.NewValue;


            Rectangle[,] oldTiles = Tiles;
            Tiles = new Rectangle[MapCanvas.NumRows, MapCanvas.NumColumns];

            // create the new tiles
            for (int i = 0; i < MapCanvas.NumRows; i++)
                for (int j = 0; j < MapCanvas.NumColumns; j++)
                    Tiles[i, j] = GetRectangle(MapCanvas, newMap[i, j]);

            // add the new Tiles
            foreach (Rectangle tile in Tiles)
                MapCanvas.Children.Add(tile);

            // remove the old Tiles
            if (oldTiles != null)
                foreach (Rectangle tile in oldTiles)
                    MapCanvas.Children.Remove(tile);
        }

        private Rectangle GetRectangle(Canvas canvas, Node node)
        {
            var rect = new Rectangle();

            int numrows = MapCanvas.NumRows;
            int numcolumns = MapCanvas.NumColumns;

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
