
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
            _logger = logger;

            AddElevators(numberOfElevators);
            _elevatorDispatcher = elevatorDispatcher;

        }

        private void AddElevators(int numberOfElevators)
        {
            for (int i = 1; i <= numberOfElevators; i++)
            {
                if(i<=2)
                _elevators.Add(new PassengerElevator(i, 10, _logger));

                if(i>2)
                 _elevators.Add(new FreightElevator(i, 100, _logger));
            }

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
                    elevator.MoveToFloorNumber(request.SourceFloor,false);
                    //Console.WriteLine($"{elevator}");

                    elevator.MoveToFloorNumber(request.DestinationFloor,true);
                    //Console.WriteLine($"{elevator}");

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
