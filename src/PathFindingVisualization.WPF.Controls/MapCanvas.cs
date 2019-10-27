using PathFindingVisualization.Core;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Controls.MapCanvasRenderers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// TODO: clean up this class
// TODO: add the tile placing functionality
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

        public ICommand PlaceTile
        {
            get => (ICommand)GetValue(PlaceTileProperty);
            set => SetValue(PlaceTileProperty, value);
        }
        public static readonly DependencyProperty PlaceTileProperty =
            DependencyProperty.Register(
                "PlaceTile", typeof(ICommand), typeof(MapCanvas),
                new PropertyMetadata(default(ICommand)));

        public ICommand RemoveTile
        {
            get => (ICommand)GetValue(RemoveTileProperty);
            set => SetValue(RemoveTileProperty, value);
        }
        public static readonly DependencyProperty RemoveTileProperty =
            DependencyProperty.Register(
                "RemoveTile", typeof(ICommand), typeof(MapCanvas),
                new PropertyMetadata(default(ICommand)));

        public int NumRows => Map.GetLength(0);
        public int NumColumns => Map.GetLength(1);

        public PathRenderer PathRenderer;
        public TileRenderer TileRenderer;
        public GridLineRenderer GridLineRenderer;

        public MapCanvas()
        {
            this.MouseRightButtonDown += HandleRightButtonDown;
            this.MouseLeftButtonDown += HandleLeftButtonDown;
            this.MouseMove += ProccessMouseMovement;

            TileRenderer = new TileRenderer();
            PathRenderer = new PathRenderer(this);
            GridLineRenderer = new GridLineRenderer(this);
        }

        private void HandleLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (PlaceTile is null) return;

            if (PlaceTile.CanExecute(e))
                PlaceTile.Execute(e);
        }
        private void HandleRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (RemoveTile is null) return;

            if (RemoveTile.CanExecute(e))
                RemoveTile.Execute(e);
        }
        private void ProccessMouseMovement(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var args = new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Left);
                HandleLeftButtonDown(sender, args);
            }
            if (e.RightButton == MouseButtonState.Pressed)
            {
                var args = new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Right);
                HandleRightButtonDown(sender, null);
            }
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
