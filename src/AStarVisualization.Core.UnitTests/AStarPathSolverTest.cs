using AStarVisualization.Core.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AStarVisualization.Core.UnitTests
{
    [TestFixture]
    public class AStarPathSolverTest
    {
        private static Node[,] Map = new Node[,]
        {
            {new Node(NodeState.Start), new Node(NodeState.Wall), new Node(NodeState.Wall), new Node(NodeState.Ground)},
            {new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground)},
            {new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Wall), new Node(NodeState.Ground)},
            {new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Goal)},
        };

        [Test, TestCaseSource("FindPath_PathExists_ReturnsPath_Cases")]
        public async Task FindPath_PathExists_ReturnsPath(object[] parameters)
        {
            (int startRowIdx, int startColIdx, int goalRowIdx, int goalColIdx, var expectedPath) = GetTestParameters(parameters);
            Node[,] map = GetMapWithWaypoints(startRowIdx, startColIdx, goalRowIdx, goalColIdx, Map);

            IPathSolver pathfinder = new AStarPathSolver(ref map);
            List<Node> actualPath = await pathfinder.FindPath();

            Assert.That(actualPath, Is.EquivalentTo(expectedPath));
        }
        #region TestUtils FindPath_PathExists_ReturnsPath
        private (int, int, int, int, List<Node>) GetTestParameters(object[] parameters)
        {
            var path = parameters.Skip(4).Cast<Node>().ToList();
            return ((int)parameters[0], (int)parameters[1], (int)parameters[2], (int)parameters[3], path);
        }
        private Node[,] GetMapWithWaypoints(int startRowIdx, int startColIdx, int goalRowIdx, int goalColIdx, Node[,] map)
        {
            map[startRowIdx, startColIdx] = new Node(NodeState.Start);
            map[goalRowIdx, goalColIdx] = new Node(NodeState.Goal);
            return map;
        }
        private static object[] FindPath_PathExists_ReturnsPath_Cases =
        {
            new object[] {0, 0, 3, 3, Map[0,0], Map[1,0], Map[1,1], Map[1,2], Map[2,3], Map[3,3]},
            // TODO add more testcases
        };
        #endregion
        //[Test] //TODO implement this test
        public void FindPath_NoPathExists_ThrowsError()
        {
            Node[,] map = null;
            IPathSolver pathfinder = new AStarPathSolver(ref map);

            Assert.That(async () => await pathfinder.FindPath(),
                Throws.Exception
                .TypeOf<NoPathFoundException>());
        }
        [Test]
        public void FindPath_MapIsNull_ThrowsError()
        {
            Node[,] map = null;
            IPathSolver pathfinder = new AStarPathSolver(ref map);

            Assert.That(async () => await pathfinder.FindPath(),
                Throws.Exception
                .TypeOf<ArgumentNullException>());
        }
        [Test]
        public void FindPath_MapIsTooSmall_ThrowsError()
        {
            var map = new Node[,] { { new Node(NodeState.Wall), new Node(NodeState.Goal) } };

            IPathSolver pathfinder = new AStarPathSolver(ref map);

            Assert.That(async () => await pathfinder.FindPath(),
                Throws.Exception
                .TypeOf<MapTooSmallException>());
        }
        [Test] // TODO add more testcases
        public void FindPath_NoWayPoints_ThrowsError()
        {
            var map = new Node[,]
            {
                {new Node(NodeState.Wall), new Node(NodeState.Ground)},
                {new Node(NodeState.Start), new Node(NodeState.Ground)},
            };

            IPathSolver pathfinder = new AStarPathSolver(ref map);

            Assert.That(async () => await pathfinder.FindPath(),
                Throws.Exception
                .TypeOf<NoWayPointsException>());
        }
    }
}
