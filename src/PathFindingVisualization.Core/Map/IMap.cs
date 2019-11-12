using System.Collections;
using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.Core.Map
{
    public interface IMap : IEnumerable
    {
        public INode this[int i, int j] { get; }
        public INode[][] Data { get; set; }
        public int GetLength(int dimension);
    }
}
