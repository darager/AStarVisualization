using AStarVisualization.Core;
using AStarVisualization.Core.Map;
using AStarVisualization.WPF.Controls.MapCanvasRenderers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// TODO: clean up this class
// TODO: add the tile placing functionality
namespace AStarVisualization.WPF.Controls
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
                "Map", typeof(Map), typeof(MapCanvas),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnMapChange)));

        public List<Node> Path
        {
            get => (List<Node>)GetValue(PathProperty);
            set => SetValue(PathProperty, value);
        }
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(
                "Path", typeof(List<Node>), typeof(MapCanvas),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPathChange)));

        public int NumRows => Map.GetLength(0);
        public int NumColumns => Map.GetLength(1);

        public PathRenderer PathRenderer;
        public TileRenderer TileRenderer;
        public GridLineRenderer GridLineRenderer;

        public MapCanvas()
        {
            TileRenderer = new TileRenderer();
            PathRenderer = new PathRenderer(this);
            GridLineRenderer = new GridLineRenderer(this);
        }

        private static void OnMapChange(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var canvas = source as MapCanvas;
            canvas.TileRenderer.HandleMapChange(source, e);
            canvas.GridLineRenderer.HandleMapChange(source, e);
        }
        private static void OnPathChange(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var canvas = source as MapCanvas;
            canvas.PathRenderer.HandlePathChange(source, e);
        }
    }
}
