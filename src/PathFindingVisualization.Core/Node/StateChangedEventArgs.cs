using System;

namespace PathFindingVisualization.Core.Node
{
    public class StateChangedEventArgs : EventArgs
    {
        public Node Node { get; private set; }
        public NodeState NewState { get; private set; }
        public NodeState OldState { get; private set; }

        public StateChangedEventArgs(Node node, NodeState newState, NodeState oldState)
        {
            Node = node;
            NewState = newState;
            OldState = oldState;
        }
    }
}
