using System.Collections;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;

// TODO: make sure that this class works correctly
namespace PathFindingVisualization.Core.PathSolvers.AStar
{
    public class AStarMap : IMap
    {
        public INode this[int i, int j] => (AStarNode)_data[i][j];
        public INode[][] Data
        {
            get => (AStarNode[][])_data;
            set => _data = (AStarNode[][])value;
        }

        private AStarNode[][] _data;

        public AStarMap(IMap map)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            _data = new AStarNode[rows][];
            for (int i = 0; i < rows; i++)
            {
                _data[i] = new AStarNode[cols];

                for (int j = 0; j < cols; j++)
                    _data[i][j] = new AStarNode(map[i, j]);
            }
        }

        public int GetLength(int dimension) => MapExtensions.GetLength(this, dimension);
        public IEnumerator GetEnumerator() => _data.GetEnumerator();
    }
}
