using System;
using System.Collections.Generic;
using System.Text;

namespace AStarVisualization.Core
{
    public class NoPathFoundException : Exception
    {
        public NoPathFoundException(string message) : base(message)
        {

        }
    }
}
