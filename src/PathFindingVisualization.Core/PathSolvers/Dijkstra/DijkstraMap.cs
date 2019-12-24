using System.Collections;

namespace PathFindingVisualization.Core.PathSolvers.Dijkstra
{
    public class DijkstraMap : IEnumerable
    {
        public DijkstraNode this[int i, int j] => _data[i, j];
        public DijkstraNode[,] Data => _data;

        public int GetLength(int dimension) => _map.GetLength(dimension);

        private Map.Map _map;
        private DijkstraNode[,] _data;

        public DijkstraMap(Map.Map map)
        {
            _map = map;

            _data = new DijkstraNode[GetLength(0), GetLength(1)];
            foreach (Node.Node node in _map)
                _data[node.RowIndex, node.ColIndex] = new DijkstraNode(node);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (DijkstraNode node in _data)
                yield return node;
        }
    }
}
