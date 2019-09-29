using System.Collections.Generic;

namespace AStarVisualization.WPF.AStarAlgorithm.AStarImplementation.Algorithmthread.Helpers
{
    public partial class ClosedSet
    {
        HashSet<Node> closedSet = new HashSet<Node>();

        public ClosedSet()
        {
        }

        public void Add(Node node)
        {
            closedSet.Add(node);
            CallEvent(node);
        }
        public void Remove(Node node)
        {
            closedSet.Remove(node);
        }
        public bool Contains(Node node)
        {
            return closedSet.Contains(node);
        }

        private void CallEvent(Node node)
        {
            var handler = NodeAddedToCollection;

            if (handler != null)
            {
                var args = new NodeAddedToCollectionEventArgs(node);
                NodeAddedToCollection(this, args);
            }
        }
        public delegate void NodeAddedToCollectionEventHandler(object sender, NodeAddedToCollectionEventArgs args);
        public static event NodeAddedToCollectionEventHandler NodeAddedToCollection;
    }
}
