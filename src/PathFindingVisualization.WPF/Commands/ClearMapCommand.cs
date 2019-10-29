using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using System;
using System.Windows.Input;

namespace PathFindingVisualization.WPF.Commands
{
    public class ClearMapCommand : ICommand
    {
        private MapEditor _mapEditor;

        public ClearMapCommand(MapEditor mapEditor)
        {
            _mapEditor = mapEditor;
        }

        public bool CanExecute(object parameter) => _mapEditor.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            _mapEditor.Path = new System.Collections.Generic.List<Node>();

            Map map = _mapEditor.Map;
            foreach (Node[] nodes in map)
                foreach (Node node in nodes)
                    node.State = NodeState.Ground;
        }

        public event EventHandler CanExecuteChanged;
    }
}
