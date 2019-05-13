using AStarVisualization.AStarAlgorithm.AStarImplementation.Algorithmthread.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AStarVisualization.AStarAlgorithm.AStarImplementation.Algorithmthread
{
    public class AStarAlgorithm
    {
        private OpenSet openSet;
        private ClosedSet closedSet = new ClosedSet();
        private Node[,] map;
        private Node StartNode;
        private Node GoalNode;
        private Node currentNode;
        private bool IsPaused;
        private bool AlgorithmDone;
        private bool DiagonalsEnabled = false;
        private int stepNumber = 0;

        public AStarAlgorithm(Node[,] map, Node StartNode, Node GoalNode, bool DiagonalPathsEnabled, double ManhattanDistanceConstant = 1000.0)
        {
            this.openSet = new OpenSet(map.GetLength(0) * map.GetLength(1));
            this.map = map;
            this.StartNode = StartNode;
            this.GoalNode = GoalNode;
            this.DiagonalsEnabled = DiagonalPathsEnabled;

            IsPaused = false;
            AlgorithmDone = false;
            ComputeHeuristicCosts(ManhattanDistanceConstant);

            PerformAlgorithm();
        }

        public void Pause()
        {
            IsPaused = true;
        }
        public void Continue()
        {
            IsPaused = false;
            PerformAlgorithm();
        }

        private async void PerformAlgorithm()
        {
            while (!AlgorithmDone)
            {
                if (IsPaused)
                {
                    return;
                }

                PerformAlgorithmStep();

                await Task.Delay((int)AStarValues.Delay);
            }
        }
        private void PerformAlgorithmStep()
        {
            if (stepNumber == 0)
                PerformFirstStep();
            else
                PerformStep();

            stepNumber++;
        }

        #region AlgorithmSteps
        private void PerformFirstStep()
        {
            currentNode = StartNode;
            currentNode.MovementCost = 0;

            if (IsPathFound(currentNode))
                HandleFoundPath(currentNode);

            openSet.Add(currentNode);
        }
        private void PerformStep()
        {
            if (openSet.Count == 0)
            {
                SetStateToFinished();
                return;
            }

            currentNode = openSet.Pop();

            if (IsPathFound(currentNode))
            {
                HandleFoundPath(currentNode);
                SetStateToFinished();
                return;
            }

            List<Node> successors = GetSuccessors(currentNode, DiagonalsEnabled);

            foreach (Node successor in successors)
            {
                successor.MovementCost = currentNode.MovementCost + MovementCost(successor, currentNode);
                successor.Parent = currentNode;

                openSet.Add(successor);
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
        }
        #endregion

        #region AlgorithmHelperMethods
        private void ComputeHeuristicCosts(double D)
        {
            foreach (Node node in map)
            {
                node.Heuristic = ManhattanDistance(node, GoalNode);
            }

            double ManhattanDistance(Node node, Node goal)
            {
                int dx = Math.Abs(node.ColumnIndex - goal.ColumnIndex);
                int dy = Math.Abs(node.RowIndex - goal.RowIndex);
                return D * (dx + dy);
            }
        }
        private bool IsPathFound(Node node)
        {
            if (node == GoalNode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void HandleFoundPath(Node node)
        {
            IsPaused = true;

            ArrayList path = ReconstructPath(node);
            CallPathFoundEvent(path);
        }
        private ArrayList ReconstructPath(Node node)
        {
            var path = new ArrayList();

            while (node.Parent != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            path.Add(StartNode);

            return path;
        }
        private List<Node> GetSuccessors(Node node, bool DiagonalEnabled = false)
        {
            List<Node> successors = new List<Node>();

            for (int row = node.RowIndex - 1; row <= node.RowIndex + 1; row++)
            {
                for (int col = node.ColumnIndex - 1; col <= node.ColumnIndex + 1; col++)
                {
                    try
                    {
                        Node successor = map[row, col];

                        if (openSet.Contains(successor) || closedSet.Contains(successor))
                        {
                            continue;
                        }

                        if (!successor.IsWalkable)
                        {
                            continue;
                        }

                        if (successor == node)
                        {
                            continue;
                        }

                        if (!DiagonalEnabled && IsDiagonalNeighbor(node, successor))
                        {
                            continue;
                        }

                        successors.Add(successor);
                    }
                    catch { }
                }
            }

            return successors;
        }
        private bool IsDiagonalNeighbor(Node firstNode, Node secondNode)
        {
            return (firstNode.RowIndex == secondNode.RowIndex - 1 || firstNode.RowIndex == secondNode.RowIndex + 1)
                && (firstNode.ColumnIndex == secondNode.ColumnIndex - 1 || firstNode.ColumnIndex == secondNode.ColumnIndex + 1);
        }
        private int MovementCost(Node firstNode, Node secondNode)
        {
            int dx = firstNode.ColumnIndex - secondNode.ColumnIndex;
            int dy = firstNode.RowIndex - secondNode.RowIndex;

            return (int)Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
        }
        #endregion

        private void SetStateToFinished()
        {
            AlgorithmDone = true;

            EventHandler handler = AlgorithmIsDone;
            if (handler != null)
            {
                AlgorithmIsDone(this, new EventArgs());
            }
        }
        public static event EventHandler AlgorithmIsDone;
        private void CallPathFoundEvent(ArrayList path)
        {
            var args = new PathFoundEventArgs(path);

            PathFoundHandler handler = PathFound;

            if (handler != null)
            {
                handler.DynamicInvoke(this, args);
            }
        }
        public delegate void PathFoundHandler(object sender, PathFoundEventArgs args);
        public static event PathFoundHandler PathFound;
    }
}
