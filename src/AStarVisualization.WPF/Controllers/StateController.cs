using AStarVisualization.WPF.AStarAlgorithm;
using AStarVisualization.WPF.UIElements;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AStarVisualization.WPF.Controllers
{
    class StateController : IController
    {
        Button StartButton;
        Button PauseButton;
        Button ResetButton;

        public StateController(Window window)
        {
            StartButton = (Button)window.FindName(ControlNames.StartButton);
            PauseButton = (Button)window.FindName(ControlNames.PauseButton);
            ResetButton = (Button)window.FindName(ControlNames.ResetButton);
        }

        public void StartControlling()
        {
            StartButton.Click += Start;
            PauseButton.Click += Pause;
            ResetButton.Click += Reset;

            StateObserver.AlgorithmIsDone += AlgorithmDone;
            StateObserver.ResetAlgorithm += EnableControls;
        }
        public void StopControlling()
        {
            StartButton.Click -= Start;
            PauseButton.Click -= Pause;
            ResetButton.Click -= Reset;

            StateObserver.AlgorithmIsDone -= AlgorithmDone;
            StateObserver.ResetAlgorithm -= EnableControls;
        }

        private void Start(object sender, EventArgs args)
        {
            AStarValues.AStarState = State.Run;

            StartButton.IsEnabled = false;
            PauseButton.IsEnabled = true;
            ResetButton.IsEnabled = true;
        }
        private void Pause(object sender, EventArgs args)
        {
            AStarValues.AStarState = State.Pause;

            StartButton.IsEnabled = true;
            ResetButton.IsEnabled = true;
            PauseButton.IsEnabled = false;
        }
        private void Reset(object sender, EventArgs args)
        {
            AStarValues.AStarState = State.HasToBeReset;

            PauseButton.IsEnabled = false;
            StartButton.IsEnabled = true;
            ResetButton.IsEnabled = false;
        }
        private void AlgorithmDone(object sender, EventArgs args)
        {
            StartButton.IsEnabled = false;
            PauseButton.IsEnabled = false;
            ResetButton.IsEnabled = true;
        }

        private void EnableControls(object sender, EventArgs args)
        {
            StartButton.IsEnabled = true;
            PauseButton.IsEnabled = false;
            ResetButton.IsEnabled = false;
        }
    }
}
