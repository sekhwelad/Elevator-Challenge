
using Elevator.Challenge.Domain.Elevator;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Elevator.Challenge.Tests.Domain.Building
{
    public class BuildingTests
    {
        private readonly List<Challenge.Domain.Elevator.Elevator> _elevators;
        private readonly IElevatorDispatcher _elevatorDispatcherMock;
        private readonly ILogger _loggerMock;

        public BuildingTests()
        {
           
            _elevatorDispatcherMock = Substitute.For<IElevatorDispatcher>();
            _loggerMock = Substitute.For<ILogger>();
            _elevators = new List<Challenge.Domain.Elevator.Elevator>();
        }

        //[Fact]
        //public void ShowElevatorStatus_Should_ShowStatusOfAnElevator()
        //{
        //    var elevator = Substitute.For<Challenge.Domain.Elevator.Elevator>(1, 10);
        //    var request = new ElevatorRequest(1, 6, 5, ElevatorType.Passenger);

        //    _elevatorDispatcherMock.AssignElevator(Arg.Any<List<Challenge.Domain.Elevator.Elevator>>(), Arg.Any<ElevatorRequest>())
        //      .Returns(elevator);

        //    _elevators.Add(elevator);

        //    elevator.ToString().Returns("Elevator 0: Type : Passenger Floor 0, Status Stationary, Direction NotMoving, Passengers 0/10");

        //    var outputResults = new StringWriter();
        //    Console.SetOut(outputResults);


        //    var building = new Challenge.Domain.Building.Building(10, 3, _elevatorDispatcherMock, _loggerMock);
        //    building.ShowElevatorStatus();

        //    var results = outputResults.ToString();
        //    Assert.Contains("Elevator 0: Type : Passenger Floor 0, Status Stationary, Direction NotMoving, Passengers 0/10", results);
        //}

        [Fact]
        public void RequestElevator_Should_AddLoadIntoAnElevatorAndMove()
        {
            var elevator = Substitute.For<Challenge.Domain.Elevator.Elevator>(1, 10, _loggerMock);
            var request = new ElevatorRequest(1, 6, 5, ElevatorType.Passenger);

            _elevatorDispatcherMock.AssignElevator(Arg.Any<List<Challenge.Domain.Elevator.Elevator>>(), Arg.Any<ElevatorRequest>())
              .Returns(elevator);

            var building = new Challenge.Domain.Building.Building(10, 3, _elevatorDispatcherMock, _loggerMock);
            building.RequestElevator(request);

            elevator.Received().AddLoad(request.PassengerNumber);
            elevator.Received().MoveToFloorNumber(request.SourceFloor);
            elevator.Received().MoveToFloorNumber(request.DestinationFloor);
            elevator.Received().Offload(request.PassengerNumber);
            elevator.Received().SetStationary(request.PassengerNumber);
        }


    }
}