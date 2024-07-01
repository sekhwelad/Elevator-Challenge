
namespace Elevator.Challenge.Application.Exceptions
{
    public sealed class InvalidLoadException :Exception
    {
        public InvalidLoadException(string message)
             : base(message)
        {

        }
    }
}
