using AStarVisualization.AStarAlgorithm;
using AStarVisualization.AStarAlgorithm.AStarImplementation;
using AStarVisualization.Controllers;
using AStarVisualization.Observers.Helpers;
using AStarVisualization.Renderer;
using AStarVisualization.Renderer.RenderHelpers;
using AStarVisualization.UIElements;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;
using AStarVisualization.Observers;

namespace AStarVisualization.AStarVisualizer
{
    public class AStarVisualizer
    {
        public AStarVisualizer(UIElements.UIControl uiElements)
        {
            AStarValues.InitAStarTiles();

            InitUIStartupValues(uiElements);
            InitObservers(uiElements);
            InitControllers(uiElements);
            InitRenderers(uiElements);

            InitAlgorithmThreadController();
        }

        private void InitUIStartupValues(UIElements.UIControl uiElements)
        {
            // Drawing Canvas:
            Canvas drawingCanvas = (Canvas)uiElements.AStarControls[ControlNames.DrawingCanvas];

            drawingCanvas.Background = new SolidColorBrush(Colors.Transparent);

            // Griddimension Textboxes:
            TextBox NumRowsTextBlock = (TextBox)uiElements.AStarControls[ControlNames.NumRowsField];
            TextBox NumColumnsTextBlock = (TextBox)uiElements.AStarControls[ControlNames.NumColumnsField];

            NumRowsTextBlock.Text = StartupValues.NumGridRows.ToString();
            NumColumnsTextBlock.Text = StartupValues.NumGridColumns.ToString();

            // Algorithm Control Buttons:
            Button StartButton = (Button)uiElements.AStarControls[ControlNames.StartButton];
            Button ResetButton = (Button)uiElements.AStarControls[ControlNames.ResetButton];
            Button PauseButton = (Button)uiElements.AStarControls[ControlNames.PauseButton];
            Slider DelaySlider = (Slider)uiElements.AStarControls[ControlNames.DelaySlider];

            StartButton.IsEnabled = true;
            ResetButton.IsEnabled = false;
            PauseButton.IsEnabled = false;
            DelaySlider.Minimum = StartupValues.MinDelay;
            DelaySlider.Maximum = StartupValues.MaxDelay;
            DelaySlider.Value = StartupValues.CurrentDelay;

            // Delay Slider:
            Label DelayLabel = (Label)uiElements.AStarControls[ControlNames.DelaySliderDisplay];
            DelayLabel.Content = "Delay: " + DelaySlider.Value + "ms";

            // Tileplacement Buttons:
            Button SetStartTileButton = (Button)uiElements.AStarControls[ControlNames.SetStartTileButton];
            Button SetWallTileButton = (Button)uiElements.AStarControls[ControlNames.SetWallTileButton];
            Button SetGoalTileButton = (Button)uiElements.AStarControls[ControlNames.SetGoalTileButton];
            Button ClearTilesButton = (Button)uiElements.AStarControls[ControlNames.ClearTilesButton];

            SetStartTileButton.IsEnabled = true;
            SetWallTileButton.IsEnabled = true;
            SetGoalTileButton.IsEnabled = true;
            ClearTilesButton.IsEnabled = true;
        }
        private void InitControllers(UIElements.UIControl uiElements)
        {
            var controllers = new List<IController>();

            controllers.Add(new DelayController(uiElements));
            controllers.Add(new DimensionController(uiElements));
            controllers.Add(new TileController(uiElements));
            controllers.Add(new StateController(uiElements));

            foreach (IController controller in controllers)
                controller.StartControlling();
        }
        private void InitObservers(UIElements.UIControl uiElements)
        {
            var observers = new List<IObserver>();

            observers.Add(new StateObserver());
            observers.Add(new DiagonalPathObserver(uiElements));

            foreach (IObserver observer in observers)
                observer.StartObserving();
        }
        private void InitRenderers(UIElements.UIControl uiElements)
        {
            Canvas canvas = (Canvas)uiElements.AStarControls[ControlNames.DrawingCanvas];
            var renderers = new List<IRenderer>();

            renderers.Add(new GridRenderer(canvas));
            renderers.Add(new TileRenderer(canvas));
            renderers.Add(new PathRenderer(canvas));

            foreach (IRenderer renderer in renderers)
                renderer.StartRendering();
        }
        private void InitAlgorithmThreadController()
        {
            var currentThreadDispatcher = Dispatcher.CurrentDispatcher;

            var algorithmController = new AlgorithmThreadController(currentThreadDispatcher);
            algorithmController.StartControlling();
        }
    }
}
