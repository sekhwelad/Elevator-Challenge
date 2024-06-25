namespace Elevator.Challenge.Domain.Elevator
{
    public class ElevatorRequest
    {
        public int SourceFloor { get; }
        public int DestinationFloor { get; }
        public int PassengerNumber { get; }

        public ElevatorRequest(int sourceFloor, int destinationFloor, int passengerNumber)
        {
            SourceFloor = sourceFloor;
            DestinationFloor = destinationFloor;
            PassengerNumber = passengerNumber;
        }
    }
}
