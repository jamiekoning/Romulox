using System;

namespace Romulox.Core.Exceptions
{
    internal class GameNotFoundException : Exception
    {
        public GameNotFoundException(string message) : base(message)
        {
           
        }
    }
}