

namespace Elevator.Challenge.Domain.Elevator
{
    public interface IElevatorDispatcher
    {
        Elevator AssignElevator(List<Elevator> elevators, ElevatorRequest request);
    }
}
