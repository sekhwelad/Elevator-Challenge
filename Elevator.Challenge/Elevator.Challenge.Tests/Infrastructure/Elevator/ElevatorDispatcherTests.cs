using Elevator.Challenge.Domain.Elevator;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Elevator.Challenge.Tests.Infrastructure.Elevator
{
    public class ElevatorDispatcherTests
    {
        private readonly List<Challenge.Domain.Elevator.Elevator> _elevators;
        private readonly ILogger _loggerMock;



        public ElevatorDispatcherTests()
        {
            _loggerMock = Substitute.For<ILogger>();
        }
        public void AddElevators()
        {
            //var freight = new FreightElevator()


            //_elevators.Add(new PassengerElevator(1, 10, _loggerMock));

        }
    }
}
