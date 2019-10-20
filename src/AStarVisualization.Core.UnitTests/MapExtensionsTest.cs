using NUnit.Framework;
using AStarVisualization.Core.Map;

namespace AStarVisualization.Core.UnitTests
{
    [TestFixture]
    public class MapExtensionsTest
    {
        public static Map.Map Map = new Map.Map(4, 4)
        {
            Data = new Node[][]
            {
                new Node[]{ new Node(NodeState.Goal), new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Wall) },
                new Node[]{ new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Wall) },
                new Node[]{ new Node(NodeState.Wall), new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Wall) },
                new Node[]{ new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Wall), new Node(NodeState.Start) },
            }
        };

        [Test]
        [TestCase(0, 0, true, 3)]
        [TestCase(1, 1, false, 4)]
        [TestCase(1, 3, true, 5)]
        [TestCase(2, 3, false, 3)]
        [TestCase(3, 0, false, 2)]
        public void GetNeighborCount_HasNeighbors_ReturnsRightAmount(int rowIdx, int colIdx, bool diagonalsEnabled, int expectedNeighborCount)
        {
            int actualNeighborCount = Map.GetNeighborCount(rowIdx, colIdx, diagonalsEnabled);

            Assert.AreEqual(actualNeighborCount, expectedNeighborCount);
        }
    }
}
