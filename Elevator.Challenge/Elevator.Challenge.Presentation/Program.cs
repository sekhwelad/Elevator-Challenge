using Elevator.Challenge.Domain.Building;
using Elevator.Challenge.Domain.Elevator;
using Elevator.Challenge.Infrastructure.Elevator;

class Program
{
    static void Main(string[] args)
    {
        var elevatorDispatcher = new ElevatorDispatcher();
        const int totalFloors = 10, numberOfElevators = 3;

        var building = new Building(totalFloors, numberOfElevators, elevatorDispatcher);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Real-Time Elevator Status:");
            Console.WriteLine($"Total Floors: {totalFloors}");
            building.ShowElevatorStatus();

            Console.WriteLine("\nEnter the Elevator type:");
            var elevatorType = (ElevatorType)int.Parse(Console.ReadLine());

            Console.WriteLine("\nEnter the source floor:");
            int sourceFloor = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the destination floor:");
            int destinationFloor = int.Parse(Console.ReadLine());

            if(destinationFloor > building.TotalFloors)
                throw new InvalidOperationException($"Building has only {building.TotalFloors} floors.");

            if (elevatorType == ElevatorType.Passenger)
                Console.WriteLine("Enter the number of passengers:");
            else
                Console.WriteLine("Enter the weight of goods:");

            int passengerCount = int.Parse(Console.ReadLine());

            var request = new ElevatorRequest(sourceFloor, destinationFloor, passengerCount, elevatorType);
            building.RequestElevator(request);

            Console.WriteLine("Press any key to make another request or 'q' to quit.");
            if (Console.ReadLine()?.ToLower() == "q")
            {
                break;
            }
        }
    }
}
