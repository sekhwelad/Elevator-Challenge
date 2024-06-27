
namespace Elevator.Challenge.Domain.Exceptions
{
    public sealed class CapacityExceededException : Exception
    {
        public CapacityExceededException(string message)
             : base(message)
        {

        }
    }
}
