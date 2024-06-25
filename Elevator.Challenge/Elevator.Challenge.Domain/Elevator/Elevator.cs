
namespace Elevator.Challenge.Domain.Elevator
{
    public sealed class Elevator
    {
        public int Id { get; }
        public ElevatorStatus Status { get; private set; }
        public ElevatorDirection Direction { get; private set; }
        public int CurrentFloor { get; private set; }      
        public int PassengerNumber { get; private set; }
        public int MaxPassengers { get; }

        protected Elevator(int id, int maxPassengers)
        {
            Id = id;
            CurrentFloor = 0;
            Status = ElevatorStatus.Stationary;
            Direction = ElevatorDirection.Stationary;
            PassengerNumber = 0;
            MaxPassengers = maxPassengers;
        }

        public void MoveToFloorNumber(int floor)
        {
            CurrentFloor = floor;
            Status = ElevatorStatus.Moving;
            Direction = CurrentFloor < floor ? ElevatorDirection.Up : ElevatorDirection.Down;
        }

        public void AddPassengers(int count)
        {
            if (PassengerNumber + count > MaxPassengers)
                throw new InvalidOperationException("Exceeds maximum passenger limit.");
            PassengerNumber += count;
        }

        public void OffloadPassengers(int count)
        {
            if (PassengerNumber - count < 0)
                throw new InvalidOperationException("Passenger count cannot be negative.");
            PassengerNumber -= count;
        }

        public void SetStationary()
        {
            Status = ElevatorStatus.Stationary;
            Direction = ElevatorDirection.Stationary;
        }

        public override string ToString()
        {
            return $"Elevator {Id}: Floor {CurrentFloor}, Status {Status}, Direction {Direction}, Passengers {PassengerNumber}/{MaxPassengers}";
        }
    }
}
