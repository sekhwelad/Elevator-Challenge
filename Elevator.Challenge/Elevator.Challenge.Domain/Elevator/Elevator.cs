using Elevator.Challenge.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Elevator.Challenge.Domain.Elevator
{
    public abstract class Elevator
    {
        public int Id { get; private set; }
        public ElevatorStatus Status { get; private set; }
        public ElevatorDirection Direction { get; private set; }
        public int CurrentFloor { get; private set; }
        public int PassengerNumber { get; private set; }
        public int MaxPassengers { get; }
        public ElevatorType ElevatorType { get; set; }
        public bool IsDoorOpen { get; set; }

        private readonly ILogger _logger;

        protected Elevator(int id, int maxPassengers, ILogger logger)
        {
            Id = id;
            CurrentFloor = 0;
            Status = ElevatorStatus.Stationary;
            Direction = ElevatorDirection.NotMoving;
            PassengerNumber = 0;
            MaxPassengers = maxPassengers;
            IsDoorOpen = false;

            _logger = logger;   
        }

        public void MoveToFloorNumber(int floor,bool isDestination)
        {
            _logger.LogInformation("MoveToFloorNumber() Invoked");
            try
            {
               
                Status =  CurrentFloor == floor ? ElevatorStatus.Stationary : ElevatorStatus.Moving;
                Direction = CurrentFloor < floor ? ElevatorDirection.Up :
                            CurrentFloor == floor ? ElevatorDirection.NotMoving :
                            ElevatorDirection.Down;
                CurrentFloor = floor;

                Console.WriteLine($"\n Elevator {Id} Is {Status} Direction :{Direction}");
                Console.WriteLine(!isDestination ? $" Elevator {Id} at Floor {floor} Fetching {PassengerNumber} Passengers" : $" Elevator {Id} at Floor {floor} Dropping {PassengerNumber} Passengers");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ERROR at {typeof(Elevator)} MoveToFloorNumber() {ex.Message}", ex.InnerException);
            }
        }

        public void AddLoad(int count)
        {
            _logger.LogInformation("AddLoad() Invoked");

            try
            {
                
                if (PassengerNumber + count > MaxPassengers)
                    throw new CapacityExceededException("Exceeds maximum limit.");
                IsDoorOpen = true;
                PassengerNumber += count;
                IsDoorOpen = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ERROR at {typeof(Elevator)} AddLoad() {ex.Message}", ex.InnerException);
                throw;
            }
        }

        public void Offload(int count)
        {
            _logger.LogInformation("Offload() Invoked");
            try
            {
                if (PassengerNumber - count < 0)
                    throw new InvalidOperationException("Load cannot be negative.");

                IsDoorOpen = true;
                PassengerNumber -= count;
                IsDoorOpen = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ERROR at {typeof(Elevator)} Offload() {ex.Message}", ex.InnerException);
                throw;
            }
        }

        public void SetStationary(int passengerNumber)
        {
            _logger.LogInformation("SetStationary() Invoked");
            try
            {
                Status = ElevatorStatus.Stationary;
                Direction = ElevatorDirection.NotMoving;

                if (ElevatorType == ElevatorType.Passenger)
                    Console.WriteLine($"\nOffloaded {passengerNumber} Passengers, Elevator now {ElevatorStatus.Stationary}");

                if (ElevatorType == ElevatorType.Freight)
                    Console.WriteLine($"\nOffloaded {passengerNumber} KGs of Goods, Elevator now {ElevatorStatus.Stationary}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ERROR at {typeof(Elevator)} SetStationary() {ex.Message}", ex.InnerException);
            }
        }

        public override string ToString()
        {
            return $" {Id}   {ElevatorType}     {CurrentFloor}      {Status}         {Direction}        {PassengerNumber}/{MaxPassengers}";
        }
    }
}
