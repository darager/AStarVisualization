using Ninject;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace PathFindingVisualization.WPF
{
    public class MapEditor
    {
        [Inject, Named("PlaceTileCommand")]
        public ICommand PlaceTile;
        [Inject, Named("RemoveTileCommand")]
        public ICommand RemoveTile;
        //[Inject, Named("ProcessMouseMovement")]
        public ICommand ProcessMouseMovement;
        [Inject, Named("ClearMapCommand")]
        public ICommand ClearMap;

        public List<Node> Path { get; set; }
        public Map Map { get; private set; }

        public bool MapDesignPhaseActive { get; set; }
        public NodeState PlacementMode { get; set; } = NodeState.Wall;

        public MapEditor(MapCanvasData data)
        {
            Map = data.Map;
            Path = data.Path;
        }
    }
}
