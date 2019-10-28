using PathFindingVisualization.Core.Node;
using System.Windows.Input;

namespace PathFindingVisualization.WPF
{
    public class NodeStateChanger
    {
        public ICommand HandleRightClick;
        public ICommand HandleLeftClick;
        public ICommand ProcessMouseMovement;

        private NodeState PlacementMode = NodeState.Ground;


        public void ChangeNodeState()
        {
        }
    }
}
