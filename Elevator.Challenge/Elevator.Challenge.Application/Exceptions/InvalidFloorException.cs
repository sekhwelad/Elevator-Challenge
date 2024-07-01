namespace Elevator.Challenge.Application.Exceptions
{
    public sealed class InvalidFloorException : Exception
    {
        public InvalidFloorException(string message)
             : base(message)
        {
                
        }
    }
}
