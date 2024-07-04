using Elevator.Challenge.Domain.Elevator;
using FluentAssertions;
using NetArchTest.Rules;

namespace Elevator.Challenge.Tests.Architecture.Infrastructure
{
    public class InfrastructureTests : BaseTests
    {
        [Fact]
        public void ElevatorDispatcher_ShouldImplement_Elevator()
        {

            var result = Types.InAssembly(InfrastructureAssembly)
                .That()
                .HaveName(nameof(Challenge.Infrastructure.Elevator.ElevatorDispatcher))
                .Should()
                .ImplementInterface(typeof(IElevatorDispatcher))
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void ElevatorDispatcherImplementation_ShouldHave_ElevatorDispatcherPostFix()
        {

            var result = Types.InAssembly(InfrastructureAssembly)
               .That()
               .ImplementInterface(typeof(IElevatorDispatcher))
               .Should()
               .HaveNameEndingWith("ElevatorDispatcher")
               .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }
    }
}
