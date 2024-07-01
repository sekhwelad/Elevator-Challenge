
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
               
        }
    }
}
