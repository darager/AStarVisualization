using AStarVisualization.WPF.AStarAlgorithm;
using AStarVisualization.WPF.Observers.Helpers;
using AStarVisualization.WPF.UIElements;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AStarVisualization.WPF.Observers
{
    public class DiagonalPathObserver : IObserver
    {
        CheckBox checkbox;

        public DiagonalPathObserver(Window window)
        {
            checkbox = (CheckBox)window.FindName(ControlNames.DiagonalPathCheckbox);
        }

        public void StartObserving()
        {
            checkbox.Click += DiagonalPathCheckbox_Clicked;
            AStarValues.AlgorithmStateChanged += AlgorithmStateChanged;
        }
        public void StopObserving()
        {
            checkbox.Click -= DiagonalPathCheckbox_Clicked;
            AStarValues.AlgorithmStateChanged -= AlgorithmStateChanged;
        }

        private void DiagonalPathCheckbox_Clicked(object sender, RoutedEventArgs e)
        {
            AStarValues.DiagonalPathsEnabled = (bool)checkbox.IsChecked;
        }
        private void AlgorithmStateChanged(object sender, EventArgs e)
        {
            checkbox.IsEnabled = (AStarValues.AStarState == State.HasNotStarted);
        }
    }
}
