
using Microsoft.Extensions.Logging;

namespace Elevator.Challenge.Domain.Elevator
{
    public class PassengerElevator : Elevator
    {
        public PassengerElevator(int id, int maxPassengers, ILogger logger) : base(id, maxPassengers,logger)
        {
            ElevatorType = ElevatorType.Passenger;  
        }

    }

}
