using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Controls.MapCanvasRenderers;

namespace PathFindingVisualization.WPF.Controls
{
    public class MapCanvas : Canvas
    {
        public Map Map
        {
            get => (Map)GetValue(MapProperty);
            set => SetValue(MapProperty, value);
        }
        public static readonly DependencyProperty MapProperty =
            DependencyProperty.Register(
                nameof(Map), typeof(Map), typeof(MapCanvas),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnMapChange)));

        public List<INode> Path
        {
            get => (List<INode>)GetValue(PathProperty);
            set => SetValue(PathProperty, value);
        }
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(
                nameof(Path), typeof(List<INode>), typeof(MapCanvas),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPathChange)));

        public int NumRows => Map.GetLength(0);
        public int NumColumns => Map.GetLength(1);

        internal PathRenderer PathRenderer;
        internal TileRenderer TileRenderer;
        internal GridLineRenderer GridLineRenderer;

        public MapCanvas()
        {
            TileRenderer = new TileRenderer();
            PathRenderer = new PathRenderer(this);
            GridLineRenderer = new GridLineRenderer(this);
        }

        public (int, int) GetNodeIndices(Point position)
        {
            double rowSpacing = ActualHeight / NumRows;
            double colSpacing = ActualWidth / NumColumns;

            int rowIdx = (int)Math.Truncate(position.Y / rowSpacing);
            int colIdx = (int)Math.Truncate(position.X / colSpacing);

            if (rowIdx >= NumRows) rowIdx = NumRows - 1;
            if (colIdx >= NumColumns) colIdx = NumColumns - 1;

            return (rowIdx, colIdx);
        }

        private static void OnMapChange(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var canvas = (MapCanvas)source;
            canvas.TileRenderer.RedrawTiles(source, e);
            canvas.GridLineRenderer.RedrawGridLines(source, e);
        }
        private static void OnPathChange(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var canvas = (MapCanvas)source;
            canvas.PathRenderer.RedrawPath(source, e);
        }
    }
}
