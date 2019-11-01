using NUnit.Framework;
using PathFindingVisualization.Core.PathSolvers;

namespace PathFindingVisualization.Core.UnitTests
{
    [TestFixture]
    public class PathSolverFactoryTest
    {
        [Test]
        public void GetPathSolver_RequestsAStarPathSolver_ReturnsAStarPathSolver()
        {
            Map.Map map = null;
            IPathSolverFactory pathSolverFactory = new PathSolverFactory();

            IPathSolver pathsolver = pathSolverFactory.GetPathSolver(ref map, PathSolver.AStar, false);

            Assert.That(pathsolver.GetType() == typeof(AStarPathSolver));
        }
    }
}
