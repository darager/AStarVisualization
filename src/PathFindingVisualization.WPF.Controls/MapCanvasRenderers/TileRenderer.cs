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
                    Tiles[i, j] = GetRectangle(canvas, newMap[i, j]);

            foreach (Node[] nodes in newMap)
                foreach (Node node in nodes)
                    node.StateChanged += UpdateColor;

            foreach (Rectangle tile in Tiles)
                canvas.Children.Add(tile);

            if (oldMap != null)
            {
                foreach (Node[] nodes in oldMap)
                    foreach (Node node in nodes)
                        node.StateChanged -= UpdateColor;

                foreach (Rectangle tile in oldTiles)
                    canvas.Children.Remove(tile);
            }
        }

        private void UpdateColor(object sender, StateChangedEventArgs e)
        {
            var node = (Node)sender;
            Rectangle rectangle = Tiles[node.RowIndex, node.ColIndex];

            rectangle.Fill = GetStateColor(e.NewState);
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
            return state switch
            {
                NodeState.Ground => new SolidColorBrush(Colors.Transparent),
                NodeState.GroundVisited => new SolidColorBrush(Colors.LightGreen),
                NodeState.GroundToBeVisited => new SolidColorBrush(Colors.LightBlue),
                NodeState.Start => new SolidColorBrush(Colors.DarkGoldenrod),
                NodeState.Wall => new SolidColorBrush(Colors.Black),
                NodeState.Goal => new SolidColorBrush(Colors.Red),
                _ => null
            };
        }
    }
}
