using System.Collections;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.Core.PathSolvers.AStar
{
    public class AStarMap : IMap
    {
        public INode this[int i, int j] => _data[i][j];
        public INode[][] Data
        {
            get => _data;
            set => _data = (AStarNode[][])value;
        }

        private AStarNode[][] _data;

        public AStarMap(IMap map)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            _data = new AStarNode[rows][];
            for (int i = 0; i <= rows; i++)
            {
                _data[i] = new AStarNode[cols];

                for (int j = 0; j <= cols; j++)
                    _data[i][j] = new AStarNode(map[i, j]);
            }
        }

        public int GetLength(int dimension) => this.GetLength(dimension);
        public IEnumerator GetEnumerator() => _data.GetEnumerator();
    }
}
