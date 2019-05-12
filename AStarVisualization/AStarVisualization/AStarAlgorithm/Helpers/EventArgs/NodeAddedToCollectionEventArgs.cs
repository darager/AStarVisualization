using System;

namespace AStarVisualization.AStarAlgorithm.AStarImplementation.Algorithmthread.Helpers
{
    public class NodeAddedToCollectionEventArgs : EventArgs
    {
        public NodeAddedToCollectionEventArgs(Node node)
        {
            this.node = node;
        }

        public Node node { get; set; }
    }
}
