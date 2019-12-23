using System.Collections;
using PathFindingVisualization.Core.Node;

// TODO: use a normal 2 dimensional array for the internal data
namespace PathFindingVisualization.Core.Map
{
    public class Map : IEnumerable
    {
        public Node.Node this[int i, int j] => _data[i][j];
        public Node.Node[][] Data
        {
            get => _data;
            set
            {
                if (_data == value)
                    return;
            }
        }

        private Node.Node[][] _data;

        public Map(int numRows = 50, int numColumns = 50)
        {
            _data = new Node.Node[numRows][];

            for (int i = 0; i < numRows; i++)
            {
                _data[i] = new Node.Node[numColumns];

                for (int j = 0; j < numColumns; j++)
                    _data[i][j] = new Node.Node(NodeState.Ground, i, j);
            }
        }

        public int GetLength(int dimension) => MapExtensions.GetLength(this, dimension);
        public IEnumerator GetEnumerator()
        {
            foreach (Node.Node[] row in _data)
                foreach (Node.Node node in row)
                    yield return node;
        }
    }
}
