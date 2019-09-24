using AStarVisualization.Core.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AStarVisualization.Core.UnitTests
{
    [TestFixture]
    public class AStarPathSolverTest
    {
        private static Node[,] Map = new Node[,]
            {
                {new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Wall), new Node(NodeState.Ground)},
                {new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground)},
                {new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Wall), new Node(NodeState.Ground)},
                {new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Ground)},
            };


        private static object[] VerifyPathsThatCanBeFound =
        {
            new object[] {0, 0, 3, 3, Map[0,0], Map[1,0], Map[1,1], Map[1,2], Map[2,3], Map[3,3]},

        };

        [Test, TestCaseSource("VerifyPathsThatCanBeFound")]
        public async void FindPath_PathExists_ReturnsPath(object[] parameters)
        {
            int startRowIdx = (int)parameters[0];
            int startColIdx = (int)parameters[0];
            int goalRowIdx = (int)parameters[0];
            int goalColIdx = (int)parameters[0];

            List<Node> requiredPath = (List<Node>)parameters.Where((n, idx) => (idx > 3)).ToList<Node>();

            Node[,] map = GetMapWithStartAndGoal(startRowIdx, startColIdx, goalRowIdx, goalColIdx, Map);
            IPathSolver pathfinder = new AStarPathSolver(ref map);
            var path = await pathfinder.FindPath();

            Assert.AreEqual(path, requiredPath);
        }
        private Node[,] GetMapWithStartAndGoal(int startRowIdx, int startColIdx, int goalRowIdx, int goalColIdx, Node[,] map)
        {
            map[startIndices.Item1, startIndices.Item2] = new Node(NodeState.Start);
            map[goalIndices.Item1, goalIndices.Item2] = new Node(NodeState.Goal);
            return map;
        }

        [Test]
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
    }
}
