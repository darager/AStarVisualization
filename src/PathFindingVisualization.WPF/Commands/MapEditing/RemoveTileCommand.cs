using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Controls;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands.MapEditing
{
    public class RemoveTileCommand : ICommand
    {
        private MapCanvasData _data;
        private MainViewModel _mainViewModel;

        public RemoveTileCommand(MapCanvasData data, MainViewModel mainViewModel)
        {
            _data = data;
            _mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter) => _mainViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            ICommand placeTile = _mainViewModel.PlaceTileCommand;

            if (placeTile.CanExecute(parameter))
            {
                _mainViewModel.PlacementMode = NodeState.Ground;
                placeTile.Execute(parameter);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
