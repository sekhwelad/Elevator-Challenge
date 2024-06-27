using Elevator.Challenge.Domain.Elevator;
using FluentAssertions;
using System.Reflection;

namespace Elevator.Challenge.Tests.Domain
{
    public class ElevatorTests
    {
        [Fact]
        public void AddLoad_Should_ThrowAnException_WhenLoadExceedsMaxPassengers()
        {
            int maxPassengers = 10;
            int load = 11;
            var elevator = new PassengerElevator(1, maxPassengers);

            var exception = Assert.Throws<InvalidOperationException>(() => elevator.AddLoad(load));

            Assert.Equal("Exceeds maximum limit.", exception.Message);
        }

        [Fact]
        public void AddLoad_Should_Succeed_WhenLoadDoesNotExceedsMaxPassengers()
        {
            int maxPassengers = 10;
            int load = 6;
            int currentPassengers = 0;
            int passengerNumber = currentPassengers + load;
            var elevator = new PassengerElevator(1, maxPassengers);
           
            elevator.AddLoad(load);

            elevator.PassengerNumber.Should().Be(passengerNumber);  
        }

        [Fact]
        public void OffLoad_Should_ThrowAnException_WhenLoadIsLessThanZero()
        {
            int maxPassengers = 10;
            int load = 5;
            var elevator = new PassengerElevator(1, maxPassengers);

            var exception = Assert.Throws<InvalidOperationException>(() => elevator.Offload(load));

            Assert.Equal("Load cannot be negative.", exception.Message);
        }

        [Fact]
        public void OffLoad_Should_Succeed_WhenLoadIsGreaterThanZeroAndLessThanAvailableLoad()
        {
            int maxPassengers = 10;
            int subtractLoad = 5;
            var elevator = new PassengerElevator(1, maxPassengers);
            SetPrivateProperty(elevator, 5,nameof(elevator.PassengerNumber));

            elevator.Offload(subtractLoad);

            elevator.PassengerNumber.Should().Be(0);
        }

        [Fact]
        public void SetStationary_Should_SetStatusToStationery()
        {
            var elevator = new PassengerElevator(1, 10);
            SetPrivateProperty(elevator, ElevatorStatus.Stationary,nameof(elevator.PassengerNumber));

            elevator.SetStationary(5);

            elevator.Status.Should().Be(ElevatorStatus.Stationary);
        }

        [Fact]
        public void SetStationary_Should_SetStatusToNotMoving()
        {
            var elevator = new PassengerElevator(1, 10);
            SetPrivateProperty(elevator, ElevatorDirection.NotMoving, nameof(elevator.PassengerNumber));

            elevator.SetStationary(5);

            elevator.Direction.Should().Be(ElevatorDirection.NotMoving);
        }


        private void SetPrivateProperty<T>(PassengerElevator elevator, T value,string property)
        {

            var field = typeof(Challenge.Domain.Elevator.Elevator).GetField($"<{property}>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);

            field.SetValue(elevator, value);
        }
    }
}
