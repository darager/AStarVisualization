using System;

namespace PathFindingVisualization.Core.Exceptions
{
    public class NoPathFoundException : Exception
    {
        public NoPathFoundException() : base() { }
        public NoPathFoundException(string message) : base(message) { }
    }
}
