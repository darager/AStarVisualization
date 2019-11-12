using System.Collections;
using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.Core.Map
{
    public class Map : IMap
    {
        public INode this[int i, int j] => _data[i][j];
        public INode[][] Data { get; set; }

        private INode[][] _data;

        public Map(int numRows = 50, int numColumns = 50)
        {
            _data = new INode[numRows][];

            for (int i = 0; i < numRows; i++)
            {
                _data[i] = new INode[numColumns];

                for (int j = 0; j < numColumns; j++)
                    _data[i][j] = new Node.Node(NodeState.Ground, i, j);
            }
        }

        public int GetLength(int dimension) => this.GetLength(dimension);
        public IEnumerator GetEnumerator() => _data.GetEnumerator();
    }
}
