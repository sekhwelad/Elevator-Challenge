
using Elevator.Challenge.Domain.Elevator;

namespace Elevator.Challenge.Infrastructure.Elevator
{
    public class ElevatorDispatcher : IElevatorDispatcher
    {
        public Domain.Elevator.Elevator AssignElevator(List<Domain.Elevator.Elevator> elevators, ElevatorRequest request)
        {
            // Implement a strategy to assign the optimal elevator

            var availableElevators = elevators
                .Where(x => x.ElevatorType == request.ElevatorType && (x.Status == ElevatorStatus.Stationary || x.Direction == ElevatorDirection.NotMoving))
                .OrderBy(e => Math.Abs(e.CurrentFloor - request.SourceFloor))
                .FirstOrDefault();

            //var availableElevators = elevators
            //    .Where(e => e.ElevatorType == request.ElevatorType && (e.Status == ElevatorStatus.Stationary || e.Direction == ElevatorDirection.NotMoving))
            //    .OrderBy(e => Math.Abs(e.CurrentFloor - request.SourceFloor))
            //    .FirstOrDefault();

       

            if (availableElevators == null)
              return null;
            
            return availableElevators;          
        }
    }
}
