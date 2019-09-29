using System;
using System.Collections;

namespace AStarVisualization.WPF.AStarAlgorithm.AStarImplementation.Algorithmthread.Helpers
{
    public class PathFoundEventArgs : EventArgs
    {
        public ArrayList Path { get; set; }
        public PathFoundEventArgs(ArrayList path)
        {
            this.Path = path;
        }
    }
}
