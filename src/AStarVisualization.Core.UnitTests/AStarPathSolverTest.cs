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
        public async void FindPath_PathExists_ReturnsPath(List<Node> requiredPath)
        {
            Node[,] map = null;
            IPathSolver pathfinder = new AStarPathSolver(ref map);
            var path = await pathfinder.FindPath();

            Assert.AreEqual(path, requiredPath);
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
