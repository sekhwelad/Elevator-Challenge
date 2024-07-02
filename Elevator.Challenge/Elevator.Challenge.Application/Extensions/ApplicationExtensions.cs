using Elevator.Challenge.Domain.Building;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Elevator.Challenge.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
               .WriteTo.File("C:\\Logs\\Elevator\\Elevator-.log", rollingInterval: RollingInterval.Day)
               .CreateLogger();

            services.AddLogging(configure => configure.AddSerilog());

            services.AddTransient<Building>();
        }
    }
}
