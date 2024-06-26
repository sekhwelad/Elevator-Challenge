namespace Elevator.Challenge.Domain.Elevator
{
    public class ElevatorRequest
    {
        public int SourceFloor { get; }
        public int DestinationFloor { get; }
        public int PassengerNumber { get; }
        public int ElevatorType { get; set; }


        public ElevatorRequest(int sourceFloor, int destinationFloor, int passengerNumber, int elevatorType)
        {
            SourceFloor = sourceFloor;
            DestinationFloor = destinationFloor;
            PassengerNumber = passengerNumber;
            ElevatorType = elevatorType;
        }
    }
}
