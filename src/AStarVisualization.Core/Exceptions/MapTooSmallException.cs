using System;

namespace AStarVisualization.Core.Exceptions
{
    public class MapTooSmallException : Exception
    {
        public MapTooSmallException() : base()
        {

        }
        public MapTooSmallException(string message) : base(message)
        {

        }
    }
}
