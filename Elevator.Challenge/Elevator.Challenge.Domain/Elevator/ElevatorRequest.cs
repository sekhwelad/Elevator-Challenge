namespace Elevator.Challenge.Domain.Elevator
{
    public class ElevatorRequest
    {
        public int PickUpFloor { get; }
        public int DestinationFloor { get; }
        public int PassengerNumber { get; }
        public ElevatorType ElevatorType { get; set; }


        public ElevatorRequest(int pickUpFloor, int destinationFloor, int passengerNumber, ElevatorType elevatorType)
        {
            PickUpFloor = pickUpFloor;
            DestinationFloor = destinationFloor;
            PassengerNumber = passengerNumber;
            ElevatorType = elevatorType;
        }
    }
}
