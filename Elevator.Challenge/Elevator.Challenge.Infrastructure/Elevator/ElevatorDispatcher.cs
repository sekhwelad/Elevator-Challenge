using Elevator.Challenge.Domain.Elevator;

namespace Elevator.Challenge.Infrastructure.Elevator
{
    public class ElevatorDispatcher : IElevatorDispatcher
    {
        public Domain.Elevator.Elevator AssignElevator(List<Domain.Elevator.Elevator> elevators, ElevatorRequest request)
        {
            
            var closestElevator = elevators
                .Where(x => x.ElevatorType == request.ElevatorType && !x.IsDoorOpen &&(x.Status == ElevatorStatus.Stationary || x.Direction == ElevatorDirection.NotMoving))
                .OrderBy(e => Math.Abs(e.CurrentFloor - request.PickUpFloor))
                .FirstOrDefault();

            return closestElevator;          
        }
    }
}
