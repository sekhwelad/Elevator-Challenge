
using FluentValidation;

namespace Elevator.Challenge.Domain.Elevator
{
    public class ElevatorRequestValidator : AbstractValidator<ElevatorRequest>
    {
        public ElevatorRequestValidator()
        {
            RuleFor(request => request)
              .Must(request => request.PickUpFloor != request.DestinationFloor)
              .WithMessage("Source and Destination Floor Can't be The same.");

            RuleFor(request => (int)request.ElevatorType)
                .InclusiveBetween(1, 2)
                .WithMessage("Elevator type must be either 1 or 2");

            RuleFor(request => request.PickUpFloor)
                .GreaterThanOrEqualTo(0)
                 .WithMessage("Must be greater than or Equal to 0");

            RuleFor(request => request.DestinationFloor)
                .GreaterThanOrEqualTo(0)
                  .WithMessage("Must be greater than or Equal to 0");

            RuleFor(request => request.PassengerNumber)
               .GreaterThan(0)
               .WithMessage("Elevator Load must be Greater than 0");

        }
    }
}
