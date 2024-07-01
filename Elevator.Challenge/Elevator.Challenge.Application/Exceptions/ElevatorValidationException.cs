using FluentValidation.Results;

namespace Elevator.Challenge.Application.Exceptions
{
    public class ElevatorValidationException : Exception
    {
        public List<ValidationFailure> Errors { get; }

        public ElevatorValidationException(string message, List<ValidationFailure> errors)
             : base(message)
        {
            Errors = errors;
        }
    }
}
