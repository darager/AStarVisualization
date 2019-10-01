using System;

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
