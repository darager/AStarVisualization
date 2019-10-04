using System;
using System.Threading.Tasks;
using AStarVisualization.Core;
using AStarVisualization.Core.PathSolvers;

namespace AStarVisualization.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new int[] { 10, 2134, 6, 45, 4, 5, 6, 435, 2345, 3246, 324 };
            Parallel.ForEach(array, num =>
            {
                int number = (int)num * 10;
                Console.WriteLine(number);
            });

            Console.ReadKey();
        }

        public static void TestNoWayPointsThrown()
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
                pathSolver.FindPath();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
