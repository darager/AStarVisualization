using System;

namespace PathFindingVisualization.Core.Exceptions
{
    public class MapTooSmallException : Exception
    {
        public MapTooSmallException() : base() { }
        public MapTooSmallException(string message) : base(message) { }
    }
}
