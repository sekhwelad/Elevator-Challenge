
using Elevator.Challenge.Domain.Elevator;

namespace Elevator.Challenge.Domain.Building
{
    public class Building
    {
        private readonly List<Elevator.Elevator> _elevators;
        private readonly IElevatorDispatcher _elevatorDispatcher;
        public int TotalFloors { get; }

        public Building(int totalFloors, int numberOfElevators, IElevatorDispatcher elevatorDispatcher)
        {
            TotalFloors = totalFloors;
            _elevators = new List<Elevator.Elevator>();
            int x = 1;
            for (int i = 0; i < numberOfElevators; i++)
            {
                _elevators.Add(new PassengerElevator(i, 10));
                _elevators.Add(new FreightElevator(i + x, 100));
                i++;
            }
            _elevatorDispatcher = elevatorDispatcher;
        }

        public void RequestElevator(ElevatorRequest request)
        {
            var elevator = _elevatorDispatcher.AssignElevator(_elevators, request);
            if (elevator != null)
            {
                elevator.AddLoad(request.PassengerNumber);
                elevator.MoveToFloorNumber(request.SourceFloor);
                Console.WriteLine($"{elevator}");

                elevator.MoveToFloorNumber(request.DestinationFloor);
                Console.WriteLine($"{elevator}");

                elevator.Offload(request.PassengerNumber);
                elevator.SetStationary(request.PassengerNumber);
            }
            else
            {
                Console.WriteLine("No available elevators at the moment.");
            }
        }

        public void ShowElevatorStatus()
        {
            Console.WriteLine("\n ID  Type        Floor      Status        Direction         Capacity");
            Console.WriteLine("--------------------------------------------------------------------------");
            foreach (var elevator in _elevators)
            {
                Console.WriteLine(elevator);
            }
        }
    }
}
