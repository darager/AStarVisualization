using AStarVisualization.WPF.AStarAlgorithm;
using AStarVisualization.WPF.AStarAlgorithm.AStarImplementation;
using AStarVisualization.WPF.Controllers;
using AStarVisualization.WPF.Observers;
using AStarVisualization.WPF.Observers.Helpers;
using AStarVisualization.WPF.Renderer;
using AStarVisualization.WPF.Renderer.RenderHelpers;
using AStarVisualization.WPF.UIElements;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace AStarVisualization.WPF
{
    public class AStarVisualizer
    {
        public AStarVisualizer(Window window)
        {
            AStarValues.InitAStarTiles();

            InitUIStartupValues(window);
            InitObservers(window);
            InitControllers(window);
            InitRenderers(window);

            InitAlgorithmThreadController();
        }

        private void InitUIStartupValues(Window window)
        {
            // Drawing Canvas:
            Canvas drawingCanvas = (Canvas)window.FindName(ControlNames.DrawingCanvas);

            drawingCanvas.Background = new SolidColorBrush(Colors.Transparent);

            // Griddimension Textboxes:
            TextBox NumRowsTextBlock = (TextBox)window.FindName(ControlNames.NumRowsField);
            TextBox NumColumnsTextBlock = (TextBox)window.FindName(ControlNames.NumColumnsField);

            NumRowsTextBlock.Text = StartupValues.StartupValues.NumGridRows.ToString();
            NumColumnsTextBlock.Text = StartupValues.StartupValues.NumGridColumns.ToString();

            // Algorithm Control Buttons:
            Button StartButton = (Button)window.FindName(ControlNames.StartButton);
            Button ResetButton = (Button)window.FindName(ControlNames.ResetButton);
            Button PauseButton = (Button)window.FindName(ControlNames.PauseButton);
            Slider DelaySlider = (Slider)window.FindName(ControlNames.DelaySlider);

            StartButton.IsEnabled = true;
            ResetButton.IsEnabled = false;
            PauseButton.IsEnabled = false;
            DelaySlider.Minimum = StartupValues.StartupValues.MinDelay;
            DelaySlider.Maximum = StartupValues.StartupValues.MaxDelay;
            DelaySlider.Value = StartupValues.StartupValues.CurrentDelay;

            // Delay Slider:
            Label DelayLabel = (Label)window.FindName(ControlNames.DelaySliderDisplay);
            DelayLabel.Content = "Delay: " + DelaySlider.Value + "ms";

            // Tileplacement Buttons:
            Button SetStartTileButton = (Button)window.FindName(ControlNames.SetStartTileButton);
            Button SetWallTileButton = (Button)window.FindName(ControlNames.SetWallTileButton);
            Button SetGoalTileButton = (Button)window.FindName(ControlNames.SetGoalTileButton);
            Button ClearTilesButton = (Button)window.FindName(ControlNames.ClearTilesButton);

            SetStartTileButton.IsEnabled = true;
            SetWallTileButton.IsEnabled = true;
            SetGoalTileButton.IsEnabled = true;
            ClearTilesButton.IsEnabled = true;
        }
        private void InitControllers(Window window)
        {
            var controllers = new List<IController>()
            {
                new DelayController(window),
                new DimensionController(window),
                new TileController(window),
                new StateController(window)
            };
            controllers.ForEach(c => c.StartControlling());
        }
        private void InitObservers(Window window)
        {
            var observers = new List<IObserver>()
            {
                new StateObserver(),
                new DiagonalPathObserver(window)
            };
            observers.ForEach(o => o.StartObserving());
        }
        private void InitRenderers(Window window)
        {
            Canvas canvas = (Canvas)window.FindName(ControlNames.DrawingCanvas);
            var renderers = new List<IRenderer>()
            {
                new GridRenderer(canvas),
                new TileRenderer(canvas),
                new PathRenderer(canvas)
            };
            renderers.ForEach(r => r.StartRendering());
        }
        private void InitAlgorithmThreadController()
        {
            var currentThreadDispatcher = Dispatcher.CurrentDispatcher;

            var algorithmController = new AlgorithmThreadController(currentThreadDispatcher);
            algorithmController.StartControlling();
        }
    }
}
