
using FluentValidation;

namespace Elevator.Challenge.Domain.Elevator
{
    public class ElevatorRequestValidator : AbstractValidator<ElevatorRequest>
    {
        public ElevatorRequestValidator()
        {
            RuleFor(request => (int)request.ElevatorType)
                .InclusiveBetween(1, 2)
                .WithMessage("Elevator type must be either 1 or 2");

            RuleFor(request => request.SourceFloor)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(request => request.DestinationFloor);

            RuleFor(request => request.DestinationFloor)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(request => request.DestinationFloor);

            RuleFor(request => request.PassengerNumber)
               .GreaterThan(0)
               .WithMessage("Elevator Load must be Greater than 0");

        }
    }
}
