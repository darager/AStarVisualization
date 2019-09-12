using System.Windows.Media;

namespace AStarVisualization.WPF.Renderer.RenderHelpers
{
    public static class RenderColors
    {
        // Grid Colors:
        public static SolidColorBrush Grid = new SolidColorBrush(Colors.LightSlateGray);

        // Path Colors
        public static int PathThickness = 2;
        public static SolidColorBrush Path = new SolidColorBrush(Colors.Black);

        // Tile Colors:
        public static SolidColorBrush WallTile = new SolidColorBrush(Colors.Black);
        public static SolidColorBrush GoalTile = new SolidColorBrush(Color.FromRgb(153, 0, 0));
        public static SolidColorBrush StartTile = new SolidColorBrush(Color.FromRgb(76, 135, 0));

        // Algorithm Tile Colors:
        public static SolidColorBrush OpenTile = new SolidColorBrush(Color.FromRgb(153, 204, 255));
        public static SolidColorBrush ClosedTile = new SolidColorBrush(Color.FromRgb(204, 255, 204));
    }
}
