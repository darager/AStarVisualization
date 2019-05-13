using AStarVisualization.AStarAlgorithm;
using AStarVisualization.Observers.Helpers;
using AStarVisualization.UIElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AStarVisualization.Observers
{
    public class DiagonalPathObserver : IObserver
    {
        CheckBox checkbox;

        public DiagonalPathObserver(UIElements.UIControl uiElements)
        {
            checkbox = (CheckBox)uiElements.AStarControls[ControlNames.DiagonalPathCheckbox];
        }

        public void StartObserving()
        {
            checkbox.Click += DiagonalPathCheckbox_Clicked;
        }
        public void StopObserving()
        {
            checkbox.Click -= DiagonalPathCheckbox_Clicked;
        }

        private void DiagonalPathCheckbox_Clicked(object sender, RoutedEventArgs e)
        {
            AStarValues.DiagonalPathsEnabled = (bool)checkbox.IsChecked;
        }
    }
}
