using Elevator.Challenge.Domain.Elevator;

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
    }
}
