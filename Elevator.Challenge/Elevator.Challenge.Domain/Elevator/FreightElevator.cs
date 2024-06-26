﻿
namespace Elevator.Challenge.Domain.Elevator
{
    public class FreightElevator : Elevator
    {
        public FreightElevator(int id, int maxWeight) : base(id, maxWeight)
        {
            ElevatorType = ElevatorType.Freight;
        }
    }
}
