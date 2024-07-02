
using Microsoft.Extensions.Logging;

namespace Elevator.Challenge.Domain.Elevator
{
    public class FreightElevator : Elevator
    {
        public FreightElevator(int id, int maxWeight, ILogger logger) : base(id, maxWeight,logger)
        {
            ElevatorType = ElevatorType.Freight;
        }
    }
}
