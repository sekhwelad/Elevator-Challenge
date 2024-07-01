using Elevator.Challenge.Domain.Exceptions;

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
        public List<ElevatorRequest> Requests { get; set; }

        protected Elevator(int id, int maxPassengers)
        {
            Id = id;
            CurrentFloor = 0;
            Status = ElevatorStatus.Stationary;
            Direction = ElevatorDirection.NotMoving;
            PassengerNumber = 0;
            MaxPassengers = maxPassengers;
            IsDoorOpen = false;
        }

        public void MoveToFloorNumber(int floor)
        {
            Status = ElevatorStatus.Moving;

            Direction = CurrentFloor < floor ? ElevatorDirection.Up : ElevatorDirection.Down;
            CurrentFloor = floor;
        }

        private void SimulateElevatorMovement(ElevatorDirection direction)
        {

        }

        public void AddLoad(int count)
        {
            if (PassengerNumber + count > MaxPassengers)
                throw new CapacityExceededException("Exceeds maximum limit."); 
            IsDoorOpen = true;
            PassengerNumber += count;
            IsDoorOpen = false;
        }

        public void Offload(int count)
        {
            if (PassengerNumber - count < 0)
                throw new InvalidOperationException("Load cannot be negative.");
            IsDoorOpen = true;
            PassengerNumber -= count;
            IsDoorOpen = false;
        }

        public void SetStationary(int passengerNumber)
        {
            Status = ElevatorStatus.Stationary;
            Direction = ElevatorDirection.NotMoving;

            if (ElevatorType == ElevatorType.Passenger)
                Console.WriteLine($"Offloaded {passengerNumber} Passengers, Elevator now {ElevatorStatus.Stationary}");

            if (ElevatorType == ElevatorType.Freight)
                Console.WriteLine($"Offloaded {passengerNumber} KGs of Goods, Elevator now {ElevatorStatus.Stationary}");
        }

        public override string ToString()
        {
            return $" {Id}   {ElevatorType}     {CurrentFloor}      {Status}         {Direction}        {PassengerNumber}/{MaxPassengers}";
        }
    }
}
