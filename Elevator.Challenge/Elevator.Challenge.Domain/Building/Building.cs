
using Elevator.Challenge.Domain.Elevator;
using Microsoft.Extensions.Logging;

namespace Elevator.Challenge.Domain.Building
{
    public class Building
    {
        private readonly List<Elevator.Elevator> _elevators = new();
        private readonly IElevatorDispatcher _elevatorDispatcher;
        private readonly ILogger _logger;

        public const int totalFloors = 11;
        public int TotalFloors { get; private set; }
        public int TotalElevators { get; private set; }

        public Building(IElevatorDispatcher elevatorDispatcher, ILogger logger)
        {
            _logger = logger;
            AddElevators();
            TotalFloors = totalFloors;
            TotalElevators = _elevators.Count;
           
            _elevatorDispatcher = elevatorDispatcher;
        }

        private void AddElevators()
        { 
           _elevators.Add(new PassengerElevator(1, 10, _logger));
           _elevators.Add(new PassengerElevator(2, 10, _logger));
           _elevators.Add(new FreightElevator(3, 100, _logger));
        }

        public void RequestElevator(ElevatorRequest request)
        {
            _logger.LogInformation("RequestElevator() Invoked");
            try
            {
                var elevator = _elevatorDispatcher.AssignElevator(_elevators, request);
                if (elevator != null)
                {
                    var status = elevator.Status == ElevatorStatus.Moving ? " Moving from" : "Stationery at" ;
                    Console.WriteLine($"\nFloor selection registered, Elevator Id {elevator.Id} {status} floor {elevator.CurrentFloor}");
                    Console.WriteLine($"");
                    elevator.AddLoad(request.PassengerNumber);
                    elevator.MoveToFloorNumber(request.PickUpFloor,false);

                    elevator.MoveToFloorNumber(request.DestinationFloor,true);

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
