//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using PathFindingVisualization.Core.Node;
//using PathFindingVisualization.DataStructures;

//namespace PathFindingVisualization.Core.PathSolvers.AStar
//{
//    public class kahoot
//    {
//        private AStarMap _algorithmSpecificMap;
//        private readonly bool _diagonalsAllowed;
//        private AStarNode _startNode;
//        private AStarNode _goalNode;
//        private AStarNode _currentNode;

//        private MinPriorityQueue<double, AStarNode> openSet;
//        private HashSet<AStarNode> closedSet;

//        public async Task<List<INode>> FindPath()
//        {

//            while ()
//            {
//            }

//            if (_currentNode.State != NodeState.Goal)
//                throw new NoPathFoundException();

//            List<INode> path = ReconstructPath(_currentNode);

//            return path;
//        }

//        private List<INode> ReconstructPath(INode node)
//        {
//            var path = new List<INode>();

//            while (node.Parent != null)
//            {
//                path.Add(node);
//                node = node.Parent;
//            }
//            path.Add(node);
//            path.Reverse();

//            return path;
//        }


//    }
//}
