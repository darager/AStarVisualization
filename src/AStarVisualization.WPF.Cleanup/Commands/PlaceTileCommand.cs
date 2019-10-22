using System;
using System.Windows.Input;

namespace AStarVisualization.WPF.Cleanup.Commands
{
    public class PlaceTileCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }
        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;
    }
}
