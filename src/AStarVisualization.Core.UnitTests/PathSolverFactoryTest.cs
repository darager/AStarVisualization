using AStarVisualization.Core.PathSolvers;
using NUnit.Framework;

namespace AStarVisualization.Core.UnitTests
{
    [TestFixture]
    public class PathSolverFactoryTest
    {
        [Test]
        public void GetPathSolver_ReturnsAStarPathSolver()
        {
            Node[][] map = null;
            IPathSolverFactory pathSolverFactory = new PathSolverFactory();

            var pathsolver = pathSolverFactory.GetPathSolver(ref map, PathSolver.AStar, false);

            Assert.IsInstanceOf(typeof(AStarPathSolver), pathsolver.GetType());
        }
    }
}
