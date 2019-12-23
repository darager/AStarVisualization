using System.Collections;

// TODO: clean up this class
namespace PathFindingVisualization.Core.PathSolvers.AStar
{
    public class AStarMap
    {
        public AStarNode this[int i, int j] => _data[i, j];
        public AStarNode[,] Data => _data;

        public int GetLength(int dimension) => _map.GetLength(dimension);

        private Map.Map _map;
        private AStarNode[,] _data;

        public AStarMap(Map.Map map)
        {
            _map = map;

            _data = new AStarNode[GetLength(0), GetLength(1)];
            foreach (Node.Node[] nodes in _map)
                foreach (Node.Node node in nodes)
                    _data[node.RowIndex, node.ColIndex] = new AStarNode(node);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (Node.Node[] nodes in _map)
                foreach (Node.Node node in nodes)
                    yield return node;
        }
    }
}