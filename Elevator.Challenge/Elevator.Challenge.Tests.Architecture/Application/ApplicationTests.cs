using FluentAssertions;
using NetArchTest.Rules;

namespace Elevator.Challenge.Tests.Architecture.Application
{
    public class ApplicationTests : BaseTests
    {
        [Fact]
        public void ExceptionTypes_ShouldBe_Sealed()
        {
            var result = Types.InAssembly(ApplicationAssembly)
                .That()
                .Inherit(typeof(Exception))
                .Should()
                .BeSealed()
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Extensions_ShouldBe_Static()
        {
            var result = Types.InAssembly(ApplicationAssembly)
                .That()
                .HaveNameEndingWith("Extensions")
                .Should()
                .BeStatic()
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }
    }
}
