using NUnit.Framework;
using AStarVisualization.Core;
using System.Collections.Generic;
using System;

namespace AStarVisualization.Core.UnitTests
{
    [TestFixture]
    public class AStarPathSolverTest
    {
        private Node[,] Map = new Node[,]
            {
                {new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Wall), new Node(NodeState.Ground)},
                {new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground)},
                {new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Wall), new Node(NodeState.Ground)},
                {new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Ground)},
            };


        [Test]
        [TestCase(0, 0, 3, 3, new List<Node>(){ this.Map[1, 1], this.Map[2, 3] })]
        public async void FindPath_PathExists_ReturnsPath(int startRowIdx, int startColIdx, int goalRowIdx, int goalColIdx, List<Node> requiredPath)
        {
            Node[,] map = GetMapWithStartAndGoal(startRowIdx, startColIdx, goalRowIdx, goalColIdx, this.Map);
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
        public void FindPath_PathDoesNotExist_ThrowsError()
        {
            Node[,] map = null;
            IPathSolver pathfinder = new AStarPathSolver(ref map);

            Assert.That(async () => await pathfinder.FindPath(),
                Throws.Exception
                .TypeOf<NoPathFoundException>());
        }
    }
}
