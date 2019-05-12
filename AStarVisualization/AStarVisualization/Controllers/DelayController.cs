using System;
using System.Windows.Controls;
using AStarVisualization.UIElements;
using AStarVisualization.AStarAlgorithm;

namespace AStarVisualization.Controllers
{
    class DelayController : IController
    {
        private Label LblDelay;
        private Slider NumDelay;

        public DelayController(UIElements.UIControl uiElements)
        {
            LblDelay = (Label)uiElements.AStarControls[ControlID.DelaySliderDisplay];
            NumDelay = (Slider)uiElements.AStarControls[ControlID.DelaySlider];
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
