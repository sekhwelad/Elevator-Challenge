

namespace Elevator.Challenge.Domain.Elevator
{
    internal interface IElevatorDispatcher
    {
        Elevator AssignElevator(List<Elevator> elevators, ElevatorRequest request);
    }
}
