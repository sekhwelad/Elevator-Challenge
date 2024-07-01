namespace Elevator.Challenge.Application.Exceptions
{
    public sealed class InvalidElevatorException: Exception
    {
        public InvalidElevatorException(string message)
             : base(message)
        {

        }
    }
}
