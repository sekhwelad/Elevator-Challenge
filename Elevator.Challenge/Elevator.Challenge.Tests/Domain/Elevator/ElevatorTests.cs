using Elevator.Challenge.Domain.Elevator;
using Elevator.Challenge.Domain.Exceptions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Reflection;

namespace Elevator.Challenge.Tests.Domain
{
    public class ElevatorTests
    {
        private readonly ILogger _loggerMock;

        public ElevatorTests()
        {
            _loggerMock = Substitute.For<ILogger>();
        }
        [Fact]
        public void AddLoad_Should_ThrowAnException_WhenLoadExceedsMaxPassengers()
        {
            int maxPassengers = 10;
            int load = 11;
            var elevator = new PassengerElevator(1, maxPassengers, _loggerMock);

            var exception = Assert.Throws<CapacityExceededException>(() => elevator.AddLoad(load));

            Assert.Equal("Exceeds maximum limit.", exception.Message);
        }

        [Fact]
        public void AddLoad_Should_Succeed_WhenLoadDoesNotExceedsMaxPassengers()
        {
            int maxPassengers = 10;
            int load = 6;
            int currentPassengers = 0;
            int passengerNumber = currentPassengers + load;
            var elevator = new PassengerElevator(1, maxPassengers, _loggerMock);
           
            elevator.AddLoad(load);

            elevator.PassengerNumber.Should().Be(passengerNumber);  
        }

        [Fact]
        public void OffLoad_Should_ThrowAnException_WhenLoadIsLessThanZero()
        {
            int maxPassengers = 10;
            int load = 5;
            var elevator = new PassengerElevator(1, maxPassengers, _loggerMock);

            var exception = Assert.Throws<InvalidOperationException>(() => elevator.Offload(load));

            Assert.Equal("Load cannot be negative.", exception.Message);
        }

        [Fact]
        public void OffLoad_Should_Succeed_WhenLoadIsGreaterThanZeroAndLessThanAvailableLoad()
        {
            int maxPassengers = 10;
            int subtractLoad = 5;
            var elevator = new PassengerElevator(1, maxPassengers, _loggerMock);
            SetPrivateProperty(elevator, 5,nameof(elevator.PassengerNumber));

            elevator.Offload(subtractLoad);

            elevator.PassengerNumber.Should().Be(0);
        }

        [Fact]
        public void SetStationary_Should_SetStatusToStationery()
        {
            var elevator = new PassengerElevator(1, 10, _loggerMock);
            SetPrivateProperty(elevator, ElevatorStatus.Stationary,nameof(elevator.PassengerNumber));

            elevator.SetStationary(5);

            elevator.Status.Should().Be(ElevatorStatus.Stationary);
        }

        [Fact]
        public void SetStationary_Should_SetStatusToNotMoving()
        {
            var elevator = new PassengerElevator(1, 10, _loggerMock);

            elevator.SetStationary(5);

            elevator.Direction.Should().Be(ElevatorDirection.NotMoving);
        }
        [Fact]
        public void MoveToFloorNumber_Should_SetDirectionToUp()
        {
            var elevator = new PassengerElevator(1, 10, _loggerMock);
            SetPrivateProperty(elevator, elevator.CurrentFloor, nameof(elevator.CurrentFloor));

            elevator.MoveToFloorNumber(8, true);

            elevator.Direction.Should().Be(ElevatorDirection.Up);

        }

        [Fact]
        public void MoveToFloorNumber_Should_SetDirectionToDown()
        {
            var elevator = new PassengerElevator(1, 10, _loggerMock);
            SetPrivateProperty(elevator, 8, nameof(elevator.CurrentFloor));

            elevator.MoveToFloorNumber(1,true);

            elevator.Direction.Should().Be(ElevatorDirection.Down);

        }

        [Fact]
        public void MoveToFloorNumber_Should_SetElevatorStatusToMoving()
        {
            var elevator = new PassengerElevator(1, 10, _loggerMock);

            elevator.MoveToFloorNumber(1,true);

            elevator.Status.Should().Be(ElevatorStatus.Moving);
        }


        private void SetPrivateProperty<T>(PassengerElevator elevator, T value,string property)
        {

            var field = typeof(Challenge.Domain.Elevator.Elevator).GetField($"<{property}>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);

            field.SetValue(elevator, value);
        }
    }
}
