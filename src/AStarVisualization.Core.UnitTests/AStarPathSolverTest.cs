using AStarVisualization.Core.Exceptions;
using AStarVisualization.Core.PathSolvers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

// TODO: clean up this testFixture

namespace AStarVisualization.Core.UnitTests
{
    [TestFixture]
    public class AStarPathSolverTest
    {
        private static readonly Node[,] Map = new Node[,]
        {
            {new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Wall), new Node(NodeState.Ground)},
            {new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground)},
            {new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Wall), new Node(NodeState.Ground)},
            {new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Ground)},
        };

        [Test, TestCaseSource("FindPath_PathExists_ReturnsPath_Cases")]
        public void FindPath_PathExists_ReturnsPath(object[] parameters)
        {
            // TODO clean up this test
            // TODO the problem is the expected path since it is derived from the static map without the waypoints
            (int startRowIdx, int startColIdx, int goalRowIdx, int goalColIdx, var expectedPathIndices) = GetTestParameters(parameters);
            Node[,] map = GetMapWithWaypoints(startRowIdx, startColIdx, goalRowIdx, goalColIdx, Map);
            List<Node> expectedPath = new List<Node>();
            foreach ((int, int) indices in expectedPathIndices)
                expectedPath.Add(map[indices.Item1, indices.Item2]);

            IPathSolver pathfinder = new AStarPathSolver(ref map);
            List<Node> actualPath = pathfinder.FindPath();

            Assert.That(actualPath, Is.EquivalentTo(expectedPath));
        }
        #region TestUtils FindPath_PathExists_ReturnsPath
        private (int, int, int, int, List<(int, int)>) GetTestParameters(object[] parameters)
        {
            var path = parameters.Skip(4).Cast<(int, int)>().ToList();
            return ((int)parameters[0], (int)parameters[1], (int)parameters[2], (int)parameters[3], path);
        }
        private Node[,] GetMapWithWaypoints(int startRowIdx, int startColIdx, int goalRowIdx, int goalColIdx, Node[,] map)
        {
            map[startRowIdx, startColIdx] = new Node(NodeState.Start);
            map[goalRowIdx, goalColIdx] = new Node(NodeState.Goal);
            return map;
        }
        private static readonly object[] FindPath_PathExists_ReturnsPath_Cases = // TODO add more testcases
        {
            new object[] {0, 0, 1, 0, (0,0), (1,0) },
            new object[] {0, 0, 1, 1, (0,0), (1,0), (1,1) },
            new object[] {0, 0, 3, 3, (0,0), (1,0), (1,1), (1,2), (2,3), (3,3) },
            new object[] {1, 3, 3, 1, (1,3), (1,2), (1,1), (1,0), (2,0), (3,0), (3,1) }
        };
        #endregion
        [Test]
        public void FindPath_NoPathExists_ThrowsError()
        {
            Node[,] map = new Node[,]
            {
                { new Node(NodeState.Start), new Node(NodeState.Wall), new Node(NodeState.Goal) },
                { new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Ground) }
            };
            IPathSolver pathfinder = new AStarPathSolver(ref map);

            Assert.That(() => pathfinder.FindPath(),
                Throws.Exception
                .TypeOf<NoPathFoundException>());
        }
        [Test]
        public void FindPath_MapIsNull_ThrowsError()
        {
            Node[,] map = null;
            IPathSolver pathfinder = new AStarPathSolver(ref map);

            Assert.That(() => pathfinder.FindPath(),
                Throws.Exception
                .TypeOf<ArgumentNullException>());
        }
        [Test]
        public void FindPath_MapIsTooSmall_ThrowsError()
        {
            var map = new Node[,] { { new Node(NodeState.Wall), new Node(NodeState.Goal) } };

            IPathSolver pathfinder = new AStarPathSolver(ref map);

            Assert.That(() => pathfinder.FindPath(),
                Throws.Exception
                .TypeOf<MapTooSmallException>());
        }
        [Test]
        public void FindPath_NoWayPoints_ThrowsError()
        {
            var map = new Node[,]
            {
                {new Node(NodeState.Wall), new Node(NodeState.Ground)},
                {new Node(NodeState.Start), new Node(NodeState.Ground)},
            };

            IPathSolver pathfinder = new AStarPathSolver(ref map);

            Assert.That(() => pathfinder.FindPath(),
                Throws.Exception
                .TypeOf<NoWayPointsException>());
        }
    }
}
