using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PathFindingVisualization.Core.Exceptions;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.Core.PathSolvers;
using PathFindingVisualization.Core.PathSolvers.AStar;

// TODO: write proper and clean tests for the pathsolvers

namespace PathFindingVisualization.Core.UnitTests
{
    [TestFixture]
    public class AStarPathSolverTest
    {
        //private static readonly Map.Map Map = new Map.Map(4, 4) { Data = map };
        //private static readonly INode[][] map = new Node.Node[][]
        //{
        //    new Node.Node[]{ new Node.Node(NodeState.Ground, 0, 0), new Node.Node(NodeState.Wall, 0, 1), new Node.Node(NodeState.Wall, 0, 2), new Node.Node(NodeState.Ground, 0, 3) },
        //    new Node.Node[]{ new Node.Node(NodeState.Ground, 1, 0), new Node.Node(NodeState.Ground, 1, 1), new Node.Node(NodeState.Ground, 1, 2), new Node.Node(NodeState.Ground, 1, 3) },
        //    new Node.Node[]{ new Node.Node(NodeState.Ground, 2, 0), new Node.Node(NodeState.Wall, 2, 1), new Node.Node(NodeState.Wall, 2, 2), new Node.Node(NodeState.Ground, 2, 3) },
        //    new Node.Node[]{ new Node.Node(NodeState.Ground, 3, 0), new Node.Node(NodeState.Ground, 3, 1), new Node.Node(NodeState.Wall, 3, 2), new Node.Node(NodeState.Ground, 3, 3) },
        //};

        //[Test, TestCaseSource("FindPath_PathExists_ReturnsPath_Cases")]
        //public void FindPath_PathExists_ReturnsPath(object[] parameters)
        //{
        //    // TODO: clean up this test, the problem with the test is the way that the paths are compared
        //    (int startRowIdx, int startColIdx, int goalRowIdx, int goalColIdx, var expectedPathIndices) = GetTestParameters(parameters);
        //    Map.Map map = GetMapWithWaypoints(startRowIdx, startColIdx, goalRowIdx, goalColIdx, Map);
        //    var expectedPath = new List<INode>();
        //    foreach ((int, int) indices in expectedPathIndices)
        //        expectedPath.Add(map[indices.Item1, indices.Item2]);

        //    IPathSolver pathfinder = new AStarPathSolver(ref map);
        //    List<INode> actualPath = pathfinder.FindPath().Result;

        //    Assert.That(actualPath, Is.EquivalentTo(expectedPath));
        //}
        //#region TestUtils FindPath_PathExists_ReturnsPath
        //private (int, int, int, int, List<(int, int)>) GetTestParameters(object[] parameters)
        //{
        //    var path = parameters.Skip(4).Cast<(int, int)>().ToList();
        //    return ((int)parameters[0], (int)parameters[1], (int)parameters[2], (int)parameters[3], path);
        //}
        //private Map.Map GetMapWithWaypoints(int startRowIdx, int startColIdx, int goalRowIdx, int goalColIdx, Map.Map map)
        //{
        //    map[startRowIdx, startColIdx] = new Node.Node(NodeState.Start);
        //    map[goalRowIdx, goalColIdx] = new Node.Node(NodeState.Goal);
        //    return map;
        //}
        //private static readonly object[] FindPath_PathExists_ReturnsPath_Cases = // TODO: add more testcases
        //{
        //    new object[] {0, 0, 1, 0, (0,0), (1,0) },
        //    new object[] {0, 0, 1, 1, (0,0), (1,0), (1,1) },
        //    new object[] {0, 0, 3, 3, (0,0), (1,0), (1,1), (1,2), (2,3), (3,3) },
        //    new object[] {1, 3, 3, 1, (1,3), (1,2), (1,1), (1,0), (2,0), (3,0), (3,1) }
        //};
        //#endregion
        //[Test]
        //public void FindPath_NoPathExists_ThrowsError()
        //{
        //    IMap map = new Map.Map(2, 3)
        //    {
        //        Data = new INode[][]
        //        {
        //            new Node.Node[]{ new Node.Node(NodeState.Start, 0, 0), new Node.Node(NodeState.Wall, 0, 1), new Node.Node(NodeState.Goal, 0, 2) },
        //            new Node.Node[]{ new Node.Node(NodeState.Ground, 1, 0), new Node.Node(NodeState.Wall, 1, 1), new Node.Node(NodeState.Ground, 1, 2) }
        //        }
        //    };
        //    IMap astarMap = new AStarMap(map);
        //    IPathSolver pathfinder = new AStarPathSolver(ref astarMap);

        //    Assert.That(() => pathfinder.FindPath(),
        //        Throws.Exception
        //        .TypeOf<NoPathFoundException>());
        //}
        //[Test]
        //public void FindPath_MapIsNull_ThrowsError()
        //{
        //    AStarMap map = null;
        //    IPathSolver pathfinder = new AStarPathSolver(ref map);

        //    Assert.That(() => pathfinder.FindPath(),
        //        Throws.Exception
        //        .TypeOf<ArgumentNullException>());
        //}
        //[Test]
        //public void FindPath_MapIsTooSmall_ThrowsError()
        //{
        //    var map = new Map.Map(1, 2)
        //    {
        //        Data = new Node.Node[][]
        //        {
        //            new Node.Node[] { new Node.Node(NodeState.Start), new Node.Node(NodeState.Goal) }
        //        }
        //    };

        //    IPathSolver pathfinder = new AStarPathSolver(ref map);

        //    Assert.That(() => pathfinder.FindPath(),
        //        Throws.Exception
        //        .TypeOf<MapTooSmallException>());
        //}
        //[Test]
        //public void FindPath_NoWayPoints_ThrowsError()
        //{
        //    var map = new Map.Map(1, 2)
        //    {
        //        Data = new Node.Node[][]
        //        {
        //            new Node.Node[]{ new Node.Node(NodeState.Wall), new Node.Node(NodeState.Ground)},
        //            new Node.Node[]{ new Node.Node(NodeState.Start), new Node.Node(NodeState.Ground)},
        //        }
        //    };

        //    IPathSolver pathfinder = new AStarPathSolver(ref map);

        //    Assert.That(() => pathfinder.FindPath(),
        //        Throws.Exception
        //        .TypeOf<NoWayPointsException>());
        //}
    }
}
