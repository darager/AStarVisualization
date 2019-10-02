using System;
using AStarVisualization.Core;
using AStarVisualization.Core.PathSolvers;

namespace AStarVisualization.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            TestNoWayPointsThrown();

            Console.ReadKey();
        }

        public static async void TestNoWayPointsThrown()
        {
            var map = new Node[,]
            {
                { new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Wall) },
                { new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Wall) },
                { new Node(NodeState.Wall), new Node(NodeState.Wall), new Node(NodeState.Ground) }
            };

            IPathSolver pathSolver = new AStarPathSolver(ref map);
            try
            {
                await pathSolver.FindPath();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
