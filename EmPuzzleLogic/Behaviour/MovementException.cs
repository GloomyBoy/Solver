using System;

namespace EmPuzzleLogic.Behaviour
{
    public class MovementException : Exception
    {
        public MovementException(string message): base(message)
        {
        }
    }
}