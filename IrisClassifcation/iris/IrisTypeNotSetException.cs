using System;

namespace iris
{
    public class IrisTypeNotSetException : Exception
    {
        public IrisTypeNotSetException() : base("iris type was not set")
        {
        }
    }
}