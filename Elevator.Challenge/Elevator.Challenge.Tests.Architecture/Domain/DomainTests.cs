using FluentAssertions;
using NetArchTest.Rules;

namespace Elevator.Challenge.Tests.Architecture.Domain
{
    public class DomainTests : BaseTests
    {
        [Fact]
        public void Elevator_ShouldBe_Abstract()
        {
            var result = Types.InAssembly(DomainAssembly)
                .That()
                .HaveName("Elevator")
                .Should()
                .BeAbstract()
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void PassengerElevator_ShouldInherit_Elevator()
        {
            var result = Types.InAssembly(DomainAssembly)
                .That()
                .HaveName("PassengerElevator")
                .Should()
                .Inherit(typeof(Challenge.Domain.Elevator.Elevator))
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void FreightElevator_ShouldInherit_Elevator()
        {
            var result = Types.InAssembly(DomainAssembly)
                .That()
                .HaveName("FreightElevator")
                .Should()
                .Inherit(typeof(Challenge.Domain.Elevator.Elevator))
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }
    }
}
