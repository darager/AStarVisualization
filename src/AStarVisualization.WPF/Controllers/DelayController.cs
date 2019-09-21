using AStarVisualization.WPF.AStarAlgorithm;
using AStarVisualization.WPF.UIElements;
using System;
using System.Windows.Controls;
<<<<<<< HEAD:AStarVisualization/AStarVisualization/Controllers/DelayController.cs
using AStarVisualization.WPF.UIElements;
using AStarVisualization.WPF.AStarAlgorithm;
using System.Windows;
=======
>>>>>>> e92bf3931e56c010cb6668335a283a2f1a7e25a2:src/AStarVisualization.WPF/Controllers/DelayController.cs

namespace AStarVisualization.WPF.Controllers
{
    class DelayController : IController
    {
        private Label LblDelay;
        private Slider NumDelay;

        public DelayController(Window window)
        {
            this.LblDelay = (Label)window.FindName(ControlNames.DelaySliderDisplay);
            this.NumDelay = (Slider)window.FindName(ControlNames.DelaySlider);
        }

        public void StartControlling()
        {
            NumDelay.ValueChanged += ChangeDelayValue;
            StateObserver.AlgorithmIsDone += DisableControl;
            StateObserver.ResetAlgorithm += EnableControl;
        }
        public void StopControlling()
        {
            NumDelay.ValueChanged -= ChangeDelayValue;
        }

        private void ChangeDelayValue(object sender, EventArgs args)
        {
            uint value = (uint)NumDelay.Value;

            LblDelay.Content = $"Delay: {value} ms";
            AStarValues.Delay = value;
        }

        private void EnableControl(object sender, EventArgs e)
        {
            NumDelay.IsEnabled = true;
        }
        private void DisableControl(object sender, EventArgs e)
        {
            NumDelay.IsEnabled = false;
        }
    }
}
