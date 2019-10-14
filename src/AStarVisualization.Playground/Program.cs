using System;
using System.Collections.Generic;
using AStarVisualization.Core;
using AStarVisualization.Core.PathSolvers;

namespace AStarVisualization.Playground
{
    class Program
    {
        static void Main()
        {
            var map = new Node[][]
            {
                new Node[]{ new Node(NodeState.Start), new Node(NodeState.Wall), new Node(NodeState.Wall), new Node(NodeState.Wall) },
                new Node[]{ new Node(NodeState.Goal), new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground) },
                new Node[]{ new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Wall), new Node(NodeState.Wall) },
                new Node[]{ new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground) }
            };
            List<Node> expectedPath = new List<Node>
            {
                map[0][0], map[1][0]
            };

            IPathSolver pathSolver = new AStarPathSolver(ref map, false);
            var path = pathSolver.FindPath();

            foreach (Node node in path)
                Console.WriteLine($"[{node.RowIndex}, {node.ColIndex}]");

            if (path == expectedPath)
                Console.WriteLine("this is working");

            Console.ReadKey();
        }

        public static void TestNoWayPointsThrown()
        {
            var map = new Node[][]
            {
                new Node[]{ new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Wall) },
                new Node[]{ new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Wall) },
                new Node[]{ new Node(NodeState.Wall), new Node(NodeState.Wall), new Node(NodeState.Ground) }
            };

            IPathSolver pathSolver = new AStarPathSolver(ref map);
            try
            {
                pathSolver.FindPath();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
