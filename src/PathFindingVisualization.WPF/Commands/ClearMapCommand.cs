using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Models;
using System;
using System.Windows.Input;

namespace PathFindingVisualization.WPF.Commands
{
    public class ClearMapCommand : ICommand
    {
        private MapEditor _mapEditor;
        private MapCanvasData _data;

        public ClearMapCommand(MapEditor mapEditor, MapCanvasData data)
        {
            _mapEditor = mapEditor;
            _data = data;
        }

        public bool CanExecute(object parameter) => _mapEditor.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            _data.Path = new System.Collections.Generic.List<Node>();

            Map map = _data.Map;
            foreach (Node[] nodes in map)
                foreach (Node node in nodes)
                    node.State = NodeState.Ground;
        }

        public event EventHandler CanExecuteChanged;
    }
}
