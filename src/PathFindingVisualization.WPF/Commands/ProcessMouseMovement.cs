using System;
using System.Windows.Input;

namespace PathFindingVisualization.WPF.Commands
{
    public class ProcessMouseMovement : ICommand
    {
        private MapEditor _mapEditor;

        public ProcessMouseMovement(MapEditor mapEditor)
        {
            _mapEditor = mapEditor;
        }

        public bool CanExecute(object parameter) => _mapEditor.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            var args = (MouseEventArgs)parameter;

            if (args.LeftButton == MouseButtonState.Pressed)
            {
                if (_mapEditor.PlaceTile.CanExecute(parameter))
                {
                    _mapEditor.PlaceTile.Execute(parameter);
                }
            }

            if (args.RightButton == MouseButtonState.Pressed)
            {
                if (_mapEditor.RemoveTile.CanExecute(parameter))
                {
                    _mapEditor.RemoveTile.Execute(parameter);
                }
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
