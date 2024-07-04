
using Elevator.Challenge.Application.Extensions;
using Elevator.Challenge.Domain.Building;
using Elevator.Challenge.Infrastructure.Elevator;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Reflection;

namespace Elevator.Challenge.Tests.Architecture
{
    public abstract class BaseTests
    {
        public static readonly Assembly DomainAssembly = typeof(Building).Assembly;
        public static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
        public static readonly Assembly ApplicationAssembly = typeof(ApplicationExtensions).Assembly;
        public static readonly Assembly InfrastructureAssembly = typeof(ElevatorDispatcher).Assembly;
    }
}
