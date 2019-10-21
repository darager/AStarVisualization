using System;

namespace AStarVisualization.Core
{
    public class NodeStateChangedEventArgs : EventArgs
    {
        public Node Node { get; }
        public NodeState NewState { get; }
        public NodeState OldState { get; }

        public NodeStateChangedEventArgs(Node node, NodeState newState, NodeState oldState)
        {
            Node = node;
            NewState = newState;
            OldState = oldState;
        }
    }
}
