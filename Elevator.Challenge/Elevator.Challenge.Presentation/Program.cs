using Elevator.Challenge.Application.Exceptions;
using Elevator.Challenge.Application.Extensions;
using Elevator.Challenge.Domain.Building;
using Elevator.Challenge.Domain.Elevator;
using Elevator.Challenge.Infrastructure.Elevator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

class Program
{
    
    static async Task Main(string[] args)
    {
        
        // Creating a service collection to configure logging
        var serviceCollection = new ServiceCollection();
        ApplicationExtensions.ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var logger = serviceProvider.GetService<ILogger<Program>>();


        logger?.LogInformation($"Application Started at {DateTime.Now}");

        var elevatorDispatcher = new ElevatorDispatcher();

        var building = new Building(elevatorDispatcher, logger);


        string[] floors = new string[building.TotalFloors];

        for (int i = 0; i < floors.Length; i++)
        {
            floors[i]= i.ToString();
        }


        while (true)
        {
           
               // Console.Clear();
                Console.WriteLine($"\n*************************** Elevator Status ***************************");
                building.ShowElevatorStatus();

                Console.WriteLine($"\n******************* Elevator Types  *******************");
                Console.WriteLine($"\n 1: {ElevatorType.Passenger}          2: {ElevatorType.Freight}");
                Console.WriteLine($"\n*******************************************************");

            try
            {
                Console.WriteLine("\nEnter the Elevator type Number:");
                var elevatorType = (ElevatorType)int.Parse(await ReadLineAsync());

                if (!Enum.IsDefined(typeof(ElevatorType), elevatorType))
                    throw new InvalidElevatorException("Elevator exception, Please choose a valid elevator.");

                Console.WriteLine($"\n******************* Elevator Floors *******************");
                DrawRowOfBoxes(floors);

                Console.WriteLine("\nEnter pick up floor:");
                int PickUpFloor = int.Parse(await ReadLineAsync());

                if (PickUpFloor > building.TotalFloors)
                    throw new InvalidFloorException($"\nInvalid floor exception ,Please choose a valid floor.");

                Console.WriteLine("Enter the destination floor:");
                int destinationFloor = int.Parse(await ReadLineAsync());

                if (destinationFloor > building.TotalFloors)
                    throw new InvalidFloorException($"Invalid floor exception ,Please choose a valid floor.");

                if (elevatorType == ElevatorType.Passenger)
                    Console.WriteLine("Enter the number of passengers:");
                else
                    Console.WriteLine("Enter the weight of goods:");

                int passengerCount = int.Parse(await ReadLineAsync());

                if (passengerCount < 1)
                    throw new InvalidLoadException("Invalid load exception, Please enter load greater than 0");
                Console.WriteLine("\n");

                ElevatorRequestValidator validator = new ElevatorRequestValidator();

                var request = new ElevatorRequest(PickUpFloor, destinationFloor, passengerCount, elevatorType);
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ElevatorValidationException("Failed Validation", validationResult.Errors);
                }

                building.RequestElevator(request);

                Console.WriteLine("\n Press any key to make another request or 'q' to quit.");
                if (Console.ReadLine()?.ToLower() == "q")
                {
                    logger.LogInformation($"Application Stopping at {DateTime.Now}");
                    break;
                }
            }
            catch (ElevatorValidationException ex)
            {
                Console.Clear();
                Console.WriteLine("Invalid Request");
                foreach (var error in ex.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName} failed validation. Error: {error.ErrorMessage}");
                    logger.LogError(ex, $"Property {error.PropertyName} failed validation. Error: {error.ErrorMessage}",ex.InnerException);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR - {ex.Message}");
                logger.LogError(ex,$"ERROR {ex.Message}",ex.InnerException);
            }
           
        }
    }
    static Task<string> ReadLineAsync()
    {
        return Task.Run(()=> Console.ReadLine());
    }

    static void DrawRowOfBoxes(string[] values)
    {
        int boxWidth = 5; // Width of each box
        int boxHeight = 3; // Height of each box

        // Draw the top border of each box
        for (int i = 0; i < values.Length; i++)
        {
            DrawTopBorder(boxWidth);
        }
        Console.WriteLine();

        // Draw the middle part of each box with the value inside
        for (int row = 0; row < boxHeight - 2; row++)
        {
            for (int i = 0; i < values.Length; i++)
            {
                DrawMiddlePart(values[i], boxWidth);
            }
            Console.WriteLine();
        }

        // Draw the bottom border of each box
        for (int i = 0; i < values.Length; i++)
        {
            DrawBottomBorder(boxWidth);
        }
        Console.WriteLine();
    }

    static void DrawTopBorder(int width)
    {
        Console.Write("┌");
        for (int i = 0; i < width - 2; i++)
        {
            Console.Write("─");
        }
        Console.Write("┐");
    }

    static void DrawMiddlePart(string value, int width)
    {
        int padding = (width - 2 - value.Length) / 2;
        Console.Write("│");
        for (int i = 0; i < padding; i++)
        {
            Console.Write(" ");
        }
        Console.Write(value);
        for (int i = 0; i < width - 2 - padding - value.Length; i++)
        {
            Console.Write(" ");
        }
        Console.Write("│");
    }

    static void DrawBottomBorder(int width)
    {
        Console.Write("└");
        for (int i = 0; i < width - 2; i++)
        {
            Console.Write("─");
        }
        Console.Write("┘");
    }
}
