
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
            for (int i = 0; i < numberOfElevators; i++)
            {
                _elevators.Add(new PassengerElevator(i, 10)); // Example max passenger limit
            }
            _elevatorDispatcher = elevatorDispatcher;
        }

        public void RequestElevator(ElevatorRequest request)
        {
            var elevator = _elevatorDispatcher.AssignElevator(_elevators, request);
            if (elevator != null)
            {
                elevator.AddPassengers(request.PassengerNumber);
                //elevator.MoveToFloorNumber(request);  //Source Floor
                elevator.MoveToFloorNumber(request.SourceFloor);  //Source Floor
                Console.WriteLine($"{elevator}");

                elevator.OffloadPassengers(request.PassengerNumber);
                elevator.SetStationary(request.PassengerNumber);
                elevator.MoveToFloorNumber(request.DestinationFloor);  //Destination Floor
               // elevator.MoveToFloorNumber(request);  //Destination Floor
                Console.WriteLine($"{elevator}");
            }
            else
            {
                Console.WriteLine("No available elevators at the moment.");
            }
        }

        public void ShowElevatorStatus()
        {
            foreach (var elevator in _elevators)
            {
                Console.WriteLine(elevator);
            }
        }
    }
}
