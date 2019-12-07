using NUnit.Framework;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.PathSolvers;
using PathFindingVisualization.Core.PathSolvers.AStar;

namespace PathFindingVisualization.Core.UnitTests
{
    [TestFixture]
    public class PathSolverFactoryTest
    {
        [Test]
        public void GetPathSolver_RequestsAStarPathSolver_GetsAStarPathSolver()
        {
            Map map = new Map.Map();
            PathSolverFactory pathSolverFactory = new PathSolverFactory();

            IPathSolver pathsolver = pathSolverFactory.GetPathSolver(map, PathSolver.AStar, false);

            Assert.That(pathsolver.GetType() == typeof(AStarPathSolver));
        }
    }
}
