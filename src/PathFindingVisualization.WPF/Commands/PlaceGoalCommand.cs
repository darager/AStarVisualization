using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;
using System;
using System.Windows.Input;

namespace PathFindingVisualization.WPF.Commands
{
    public class PlaceGoalCommand : ICommand
    {
        private readonly MainViewModel _mapViewModel;

        public PlaceGoalCommand(MainViewModel mapViewModel)
        {
            _mapViewModel = mapViewModel;
        }

        public bool CanExecute(object parameter) => _mapViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            _mapViewModel.PlacementMode = Place.Goal;
        }

        public event EventHandler CanExecuteChanged;
    }
}
