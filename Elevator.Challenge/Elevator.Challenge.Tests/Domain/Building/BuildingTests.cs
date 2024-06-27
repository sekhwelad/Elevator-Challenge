
using Elevator.Challenge.Domain.Elevator;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using NSubstitute;
using NSubstitute.Core;
using Elevator = Elevator.Challenge.Domain.Elevator.Elevator;

namespace Elevator.Challenge.Tests.Domain.Building
{
    public class BuildingTests
    {
        private readonly List<Challenge.Domain.Elevator.Elevator> _elevators;
        private readonly IElevatorDispatcher _elevatorDispatcherMock;

        public BuildingTests()
        {
           
            _elevatorDispatcherMock = Substitute.For<IElevatorDispatcher>();
            _elevators = new List<Challenge.Domain.Elevator.Elevator>();

            AddElevatorData();
        }

        [Fact]
        public void ShowElevatorStatus_Should_ShowStatusOfAnElevator()
        {
            var elevator = Substitute.For<Challenge.Domain.Elevator.Elevator>(1, 10);
            var request = new ElevatorRequest(1, 6, 5, ElevatorType.Passenger);

            _elevatorDispatcherMock.AssignElevator(Arg.Any<List<Challenge.Domain.Elevator.Elevator>>(), Arg.Any<ElevatorRequest>())
              .Returns(elevator);

            _elevators.Add(elevator);

            elevator.ToString().Returns("Elevator 0: Type : Passenger Floor 0, Status Stationary, Direction NotMoving, Passengers 0/10");

            var outputResults = new StringWriter();
            Console.SetOut(outputResults);


            var building = new Challenge.Domain.Building.Building(10, 3, _elevatorDispatcherMock);
            building.ShowElevatorStatus();

            var results = outputResults.ToString();
            Assert.Contains("Elevator 0: Type : Passenger Floor 0, Status Stationary, Direction NotMoving, Passengers 0/10", results);
        }

        [Fact]
        public void RequestElevator_Should_AddLoadIntoAnElevatorAndMove()
        {
            var elevator = Substitute.For<Challenge.Domain.Elevator.Elevator>(1, 10);
            var request = new ElevatorRequest(1, 6, 5, ElevatorType.Passenger);

            _elevatorDispatcherMock.AssignElevator(Arg.Any<List<Challenge.Domain.Elevator.Elevator>>(), Arg.Any<ElevatorRequest>())
              .Returns(elevator);

            var building = new Challenge.Domain.Building.Building(10, 3, _elevatorDispatcherMock);
            building.RequestElevator(request);

            elevator.Received().AddLoad(request.PassengerNumber);
            elevator.Received().MoveToFloorNumber(request.SourceFloor);
            elevator.Received().MoveToFloorNumber(request.DestinationFloor);
            elevator.Received().Offload(request.PassengerNumber);
            elevator.Received().SetStationary(request.PassengerNumber);
        }


        public void AddElevatorData()
        {
            _elevators.Add(new PassengerElevator(1, 10));
            _elevators.Add(new FreightElevator(2, 100));
        }
    }
}