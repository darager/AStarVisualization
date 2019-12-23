using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.WPF.Controls.MapCanvasRenderers
{
    public class TileRenderer
    {
        private Dictionary<NodeState, Color> TileColors = new Dictionary<NodeState, Color>
        {
            [NodeState.Wall] = Colors.Black,
            [NodeState.Goal] = Colors.Red,
            [NodeState.Start] = Colors.DarkGoldenrod,
            [NodeState.Ground] = Colors.Transparent,
            [NodeState.GroundVisited] = Colors.LightGreen,
            [NodeState.GroundToBeVisited] = Colors.LightBlue,
        };
        private Rectangle[,] Tiles;

        public void RedrawTiles(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var canvas = (MapCanvas)source;
            var oldMap = (Map)e.OldValue;
            var newMap = (Map)e.NewValue;

            Rectangle[,] oldTiles = Tiles;
            Tiles = new Rectangle[canvas.NumRows, canvas.NumColumns];

            // create the new tiles
            for (int i = 0; i < canvas.NumRows; i++)
                for (int j = 0; j < canvas.NumColumns; j++)
                    Tiles[i, j] = GetRectangle(canvas, (Node)newMap[i, j]);

            foreach (Node node in newMap)
                node.StateChanged += UpdateColor;

            foreach (Rectangle tile in Tiles)
                canvas.Children.Add(tile);

            if (oldMap != null)
            {
                foreach (Node node in oldMap)
                    node.StateChanged -= UpdateColor;

                foreach (Rectangle tile in oldTiles)
                    canvas.Children.Remove(tile);
            }
        }

        private void UpdateColor(object sender, StateChangedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var node = (Node)sender;
                Rectangle rectangle = Tiles[node.RowIndex, node.ColIndex];

                rectangle.Fill = GetColor(e.NewState);
            });
        }
        private Rectangle GetRectangle(MapCanvas canvas, Node node)
        {
            var rect = new Rectangle();

            int numrows = canvas.NumRows;
            int numcolumns = canvas.NumColumns;

            double canvasheight = canvas.ActualHeight;
            double canvaswidth = canvas.ActualWidth;

            double gridHeight = canvasheight / numrows;
            double gridWidth = canvaswidth / numcolumns;

            int gridLineThickness = 1;
            rect.Height = gridHeight - 2 * gridLineThickness;
            rect.Width = gridWidth - 2 * gridLineThickness;

            rect.SnapsToDevicePixels = true;
            rect.Fill = GetColor(node.State);

            double x = node.ColIndex * gridWidth + gridLineThickness;
            double y = node.RowIndex * gridHeight + gridLineThickness;

            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);

            return rect;
        }

        // TODO: choose proper colors
        private SolidColorBrush GetColor(NodeState state) => new SolidColorBrush(TileColors[state]);
    }
}
