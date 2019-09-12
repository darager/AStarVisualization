using System;
using System.Collections;
using System.Collections.Generic;

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
