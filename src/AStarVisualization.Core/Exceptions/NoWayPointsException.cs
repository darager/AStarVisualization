using System;
using System.Collections.Generic;
using System.Text;

namespace AStarVisualization.Core.Exceptions
{
    public class NoWayPointsException : Exception
    {
        public NoWayPointsException() : base()
        {

        }

        public NoWayPointsException(string message) : base(message)
        {

        }
    }
}
