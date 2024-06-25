namespace Elevator.Challenge.Domain.Elevator
{
    public class ElevatorRequest
    {
        public int SourceFloor { get; }
        public int DestinationFloor { get; }
        public int PassengerCount { get; }

        public ElevatorRequest(int sourceFloor, int destinationFloor, int passengerNumber)
        {
            SourceFloor = sourceFloor;
            DestinationFloor = destinationFloor;
            PassengerCount = passengerNumber;
        }
    }
}
