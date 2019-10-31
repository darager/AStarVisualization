using Ninject;
using PathFindingVisualization.Core.Node;
using System.Windows.Input;

namespace PathFindingVisualization.WPF
{
    public class MapEditor
    {
        [Inject, Named("PlaceTileCommand")]
        public ICommand PlaceTile { get; set; }
        [Inject, Named("RemoveTileCommand")]
        public ICommand RemoveTile { get; set; }
        [Inject, Named("ProcessMouseMovementCommand")]
        public ICommand ProcessMouseMovement { get; set; }
        [Inject, Named("ClearMapCommand")]
        public ICommand ClearMap { get; set; }

        public bool MapDesignPhaseActive { get; set; } = true; // TODO: handle this property differently
        public NodeState PlacementMode { get; set; } = NodeState.Wall;

        public MapEditor()
        {
        }
    }
}
