using AStarVisualization.WPF.AStarAlgorithm;
using AStarVisualization.WPF.Renderer.RenderHelpers;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AStarVisualization.WPF.Renderer.TileRenderHelpers
{
    public class TileRenderObject
    {
        public Rectangle Shape;

        public TileRenderObject(Canvas drawingCanvas, AStarTile tile)
        {
            Shape = GetRectangle(drawingCanvas, tile);
        }

        private Rectangle GetRectangle(Canvas DrawingCanvas, AStarTile tile)
        {
            var rect = new Rectangle();

            int numrows = AStarValues.NumGridRows;
            int numcolumns = AStarValues.NumGridColumns;

            double canvasheight = DrawingCanvas.ActualHeight;
            double canvaswidth = DrawingCanvas.ActualWidth;

            double rowspacing = canvasheight / numrows;
            double columnspacing = canvaswidth / numcolumns;

            rect.Height = rowspacing;
            rect.Width = columnspacing;

            rect.Stroke = new SolidColorBrush(Colors.Transparent);
            rect.SnapsToDevicePixels = true;


            switch (tile.TileType)
            {
                case Tile.Empty:
                    rect.Fill = new SolidColorBrush(Colors.Transparent);
                    break;
                case Tile.EmptyClosed:
                    rect.Fill = RenderColors.ClosedTile;
                    break;
                case Tile.EmptyOpen:
                    rect.Fill = RenderColors.OpenTile;
                    break;
                case Tile.Start:
                    rect.Fill = RenderColors.StartTile;
                    break;
                case Tile.Wall:
                    rect.Fill = RenderColors.WallTile;
                    break;
                case Tile.Goal:
                    rect.Fill = RenderColors.GoalTile;
                    break;
            }

            double topdistance = tile.RowIndex * rowspacing;
            double leftdistance = tile.ColumnIndex * columnspacing;

            Canvas.SetLeft(rect, leftdistance);
            Canvas.SetTop(rect, topdistance);

            return rect;
        }
    }

}
