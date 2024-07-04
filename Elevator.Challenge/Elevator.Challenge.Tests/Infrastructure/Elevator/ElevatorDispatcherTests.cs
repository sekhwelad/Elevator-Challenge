using Elevator.Challenge.Domain.Elevator;
using Elevator.Challenge.Infrastructure.Elevator;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Reflection;

namespace Elevator.Challenge.Tests.Infrastructure.Elevator
{
    public class ElevatorDispatcherTests
    {
        private readonly List<Challenge.Domain.Elevator.Elevator> _elevators;
        private readonly ILogger _loggerMock;
        private readonly IElevatorDispatcher _elevatorDispatcher;

        public ElevatorDispatcherTests()
        {
            _loggerMock = Substitute.For<ILogger>();
            _elevators = new();
            _elevatorDispatcher = new ElevatorDispatcher();
        }

        [Fact]
        public void AssignElevator_ShouldReturn_AtleastOneElevator()
        {
            var request = new ElevatorRequest(0, 3, 5, ElevatorType.Passenger);
            _elevators.Add(new PassengerElevator(1, 10, _loggerMock));

            var elevator = _elevatorDispatcher.AssignElevator(_elevators, request);

            elevator.Should().NotBeNull();
            elevator.CurrentFloor.Should().Be(0);
            elevator.IsDoorOpen.Should().BeFalse(); 
        }

        [Fact]
        public void AssignElevator_ShouldReturn_NullWhenElevatorDoorIsOpen()
        {
            var request = new ElevatorRequest(0, 3, 5, ElevatorType.Passenger);
           var openDoorElevator = new PassengerElevator(1, 10, _loggerMock);

            var field = typeof(Challenge.Domain.Elevator.Elevator).GetField($"<{nameof(openDoorElevator.IsDoorOpen)}>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(openDoorElevator, true);

            _elevators.Add(openDoorElevator);

            var elevator = _elevatorDispatcher.AssignElevator(_elevators, request);

            elevator.Should().BeNull();
        }
        
    }
}
