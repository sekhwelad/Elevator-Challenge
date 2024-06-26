
namespace Elevator.Challenge.Domain.Elevator
{
    public class PassengerElevator : Elevator
    {
        public PassengerElevator(int id, int maxPassengers) : base(id, maxPassengers)
        {
            ElevatorType = (int)Domain.Elevator.ElevatorType.Passenger;
        }
    }

}
