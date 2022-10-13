using System;

namespace Generics
{
    public class BoxOverException : Exception
    {
        public BoxOverException(string message) : base(message)
        { }
    }
}