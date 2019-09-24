using System;

namespace AStarVisualization.Core.Exceptions
{
    public class NoPathFoundException : Exception
    {
        public NoPathFoundException() : base()
        {

        }
        public NoPathFoundException(string message) : base(message)
        {

        }
    }
}
