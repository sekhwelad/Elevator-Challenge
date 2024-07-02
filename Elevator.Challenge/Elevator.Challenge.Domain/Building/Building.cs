
using Elevator.Challenge.Domain.Elevator;
using Microsoft.Extensions.Logging;

namespace Elevator.Challenge.Domain.Building
{
    public class Building
    {
        private readonly List<Elevator.Elevator> _elevators;
        private readonly IElevatorDispatcher _elevatorDispatcher;
        private readonly ILogger _logger;
        public int TotalFloors { get; }

        public Building(int totalFloors, int numberOfElevators, IElevatorDispatcher elevatorDispatcher, ILogger logger)
        {
            TotalFloors = totalFloors;
            _elevators = new List<Elevator.Elevator>();
            int x = 1;
            _logger = logger;

            for (int i = 0; i < numberOfElevators; i++)
            {
                _elevators.Add(new PassengerElevator(i, 10,_logger));
                _elevators.Add(new FreightElevator(i + x, 100,_logger));
                i++;
            }
            _elevatorDispatcher = elevatorDispatcher;

        }

        public void RequestElevator(ElevatorRequest request)
        {
            _logger.LogInformation("RequestElevator() Invoked");
            try
            {
                var elevator = _elevatorDispatcher.AssignElevator(_elevators, request);
                if (elevator != null)
                {
                    Console.WriteLine($"\nFloor selection registered, Elevator Id {elevator.Id} heading your way");
                    Console.WriteLine($"");
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ERROR at {typeof(Building)} RequestElevator() {ex.Message}",ex.InnerException);
            }
        }

        public void ShowElevatorStatus()
        {
            _logger.LogInformation("ShowElevatorStatus() Invoked");
            try
            {
                Console.WriteLine("\n ID  Type        Floor      Status        Direction         Capacity");
                Console.WriteLine("--------------------------------------------------------------------------");
                foreach (var elevator in _elevators)
                {
                    Console.WriteLine(elevator);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ERROR at {typeof(Building)} ShowElevatorStatus() {ex.Message}", ex.InnerException);
            }
        }
    }
}
