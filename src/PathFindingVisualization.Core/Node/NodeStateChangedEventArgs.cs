using System;

namespace PathFindingVisualization.Core.Node
{
    public class StateChangedEventArgs : EventArgs
    {
        public Node Node { get; }
        public NodeState NewState { get; }
        public NodeState OldState { get; }

        public StateChangedEventArgs(Node node, NodeState newState, NodeState oldState)
        {
            Node = node;
            NewState = newState;
            OldState = oldState;
        }
    }
}
