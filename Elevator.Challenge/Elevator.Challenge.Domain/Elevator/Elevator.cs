﻿
namespace Elevator.Challenge.Domain.Elevator
{
    public abstract class Elevator
    {
        public int Id { get; private set; }
        public ElevatorStatus Status { get; private set; }
        public ElevatorDirection Direction { get; private set; }
        public int CurrentFloor { get; private set; }
        public int DestinationFloor { get; private set; }
        public int PassengerNumber { get; private set; }
        public int MaxPassengers { get; }

        protected Elevator(int id, int maxPassengers)
        {
            Id = id;
            CurrentFloor = 0;
            DestinationFloor = 0;
            Status = ElevatorStatus.Stationary;
            Direction = ElevatorDirection.NotMoving;
            PassengerNumber = 0;
            MaxPassengers = maxPassengers;
        }

        public void MoveToFloorNumber(int floor)
        {
           
            Status = ElevatorStatus.Moving;

            if(CurrentFloor == floor) 
            { 
                Direction = ElevatorDirection.NotMoving;
            }
            else
            {
                Direction = CurrentFloor < floor ? ElevatorDirection.Up : ElevatorDirection.Down;
            }
           
            CurrentFloor = floor;

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

        public void SetStationary(int passengerNumber)
        {
            Status = ElevatorStatus.Stationary;
            Direction = ElevatorDirection.NotMoving;
            Console.WriteLine($"Offloaded {passengerNumber} Passengers, Elevator now {ElevatorStatus.Stationary}");
        }

        public override string ToString()
        {
            //return $"Elevator {Id}: Is {Status}, {Direction} ----> Floor {CurrentFloor} with {PassengerNumber}/{MaxPassengers} Passengers ";
            return $"Elevator {Id}: Floor {CurrentFloor}, Status {Status}, Direction {Direction}, Passengers {PassengerNumber}/{MaxPassengers}";

        }
    }
}
