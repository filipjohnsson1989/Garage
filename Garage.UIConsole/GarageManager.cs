using Garage.Common;
using Garage.UIConsole.Entities;
using Garage.UIConsole.UserInterface;

namespace Garage.UIConsole;

public class GarageManager
{

    private IUI consoleUI;
    private GarageHandler? garageHandler = default!;

    public GarageManager()
    {
        consoleUI = new ConsoleUI();

    }

    public void Run()
    {
        do
        {
            // Huvudmeny
            ShowMainMenu();
            GetUserInput();
        } while (true);
    }

    void ShowMainMenu()
    {
        consoleUI.Clear();
        consoleUI.AddMessage("Vänligen navigera genom menyn genom att ange numret \n(1, 2, 3, ..., 8, 0) som du väljer med hjälp av numeriska tangenter"
                + "\n1.Instansieringen av ett nytt garage"
                + "\n2.Instansieringen av ett nytt garage med ett antal fordon"
                + "\n3.Lista samtliga parkerade fordon"
                + "\n4.Lista fordonstyper och hur många av varje som står i garaget"
                + "\n5.Lägga till fordon garaget"
                + "\n6.Ta bort fordon ur garaget"
                + "\n7.Hitta ett specifikt fordon"
                + "\n8.Filtrera fordon"
                + "\n0.Stänga av applikationen");
    }

    void GetUserInput()
    {
        var keyPressed = consoleUI.GetKey();//Creates the Key input to be used with the switch-case below.

        var actionMeny = new Dictionary<ConsoleKey, Action>()
                {
                    {ConsoleKey.NumPad1,CreateGarage },
                    {ConsoleKey.NumPad2,CreateGarageWithVehicles },
                    {ConsoleKey.NumPad3,ListOfVehicles },
                    {ConsoleKey.NumPad4,ListOfVehicleTypes},
                    {ConsoleKey.NumPad5,AddVehicle },
                    {ConsoleKey.NumPad6,RemoveVehicle},
                    {ConsoleKey.NumPad7,FindVehicle},
                    {ConsoleKey.NumPad8,FilterVehicles },
                    {ConsoleKey.NumPad0,ClosePrograme },
                };

        if (actionMeny.ContainsKey(keyPressed))
            actionMeny[keyPressed]?.Invoke();

    }

    void CreateGarage()
    {
        CreateGarage(vehicles: null);
    }

    void CreateGarage(IEnumerable<IVehicle>? vehicles = null)
    {
        consoleUI.Clear();
        consoleUI.AddMessage("Instansieringen av ett nytt garage");
        consoleUI.AddMessage("Kapacitet?");


        string? input = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(input);


        if (string.IsNullOrEmpty(input) || !uint.TryParse(input, out uint capacity))
            throw new ArgumentException();

        garageHandler = new(capacity, vehicles);

        var remaindeCapacity = capacity - (vehicles is null ? 0 : vehicles.Count());

        consoleUI.AddMessage($"Garaget har {remaindeCapacity} kapacitet kvar");

        consoleUI.AddMessage("Något att gå tillbaka till huvudmeny");
        Console.ReadKey();

    }

    void CreateGarageWithVehicles()
    {
        consoleUI.Clear();
        consoleUI.AddMessage("Instansieringen av ett nytt garage med ett antal fordon");

        var vehicles = this.CreateVehicles();
        CreateGarage(vehicles);
    }

    void ListOfVehicles()
    {
        ArgumentNullException.ThrowIfNull(garageHandler);

        consoleUI.Clear();
        consoleUI.AddMessage("Lista samtliga parkerade fordon");

        var vehicles = garageHandler.GetVehicles();
        foreach (var vehicle in vehicles)
        {
            consoleUI.AddMessage(vehicle.Stats() + " \n\r");
        }
        consoleUI.AddMessage("Något att gå tillbaka till huvudmeny");
        Console.ReadKey();
    }

    void ListOfVehicleTypes()
    {
        ArgumentNullException.ThrowIfNull(garageHandler);

        consoleUI.Clear();
        consoleUI.AddMessage("Lista fordonstyper och hur många av varje som står i garaget");

        var vehicleGroups = garageHandler.GetVehicles().GroupBy(x => x.GetType().Name).Select(x => new { Type = x.Key, Count = x.Count() });
        foreach (var vehicleGroup in vehicleGroups)
        {
            consoleUI.AddMessage($"Fordonstyp: {vehicleGroup.Type}, {vehicleGroup.Count} st\n\r");
        }

        consoleUI.AddMessage("Något att gå tillbaka till huvudmeny");
        Console.ReadKey();
    }

    void AddVehicle()
    {
        ArgumentNullException.ThrowIfNull(garageHandler);

        consoleUI.Clear();
        consoleUI.AddMessage("Lägga till fordon");

        var vehicle = this.CreateVehicle();
        ArgumentNullException.ThrowIfNull(vehicle);

        garageHandler.AddVehicle(vehicle);

        consoleUI.AddMessage("Fordonet läggs till garaget");

        consoleUI.AddMessage("Något att gå tillbaka till huvudmeny");
        Console.ReadKey();
    }

    IEnumerable<IVehicle> CreateVehicles()
    {
        var result = new List<IVehicle>();
        do
        {
            var vehicle = this.CreateVehicle();

            if (vehicle is null)
                break;

            result.Add(vehicle);
            //yield return vehicle;

        } while (true);

        return result;
    }

    IVehicle? CreateVehicle()
    {
        var vehicle = AskForVehicle();

        if (vehicle is null)
            return null;

        var result = GarageHandler.CreateVehicle(registerNumber: vehicle.Value.Item1,
                       color: vehicle.Value.Item2,
                       numberOfWheels: vehicle.Value.Item3,
                       vehicleType: vehicle.Value.Item4,
                       wingSpan: vehicle.Value.Item5,
                       hullType: vehicle.Value.Item6,
                       busType: vehicle.Value.Item7,
                       hasOneLessWheelSuspension: vehicle.Value.Item8,
                       topBoxCapacity: vehicle.Value.Item9);

        return result;

    }

    (string, string, uint, VehicleType, uint?, uint?, uint?, bool?, uint?)? AskForVehicle()
    {
        consoleUI.AddMessage("Vilket fordon vill du läga till?"
                    + "\n1.Airplain"
                    + "\n2.Boat"
                    + "\n3.Bus"
                    + "\n4.Car"
                    + "\n5.MotorCycle"
                    + "\nTomt att gå tillbaka till huvudmeny");

        string? input = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(input);

        if (string.IsNullOrEmpty(input))
            return null;

        // ToDo: out of range VehicleType
        var vehicleType = (VehicleType)(int.Parse(input) - 1);

        consoleUI.AddMessage("Registreringsnummer?");
        string? registerNumber = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(registerNumber);

        consoleUI.AddMessage("Färg?");
        string? color = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(color);

        consoleUI.AddMessage("Antal hjul?");
        string? numberOfWheels = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(numberOfWheels);


        uint? wingSpan = null;
        uint? hullType = null;
        uint? busType = null;
        bool? hasOneLessWheelSuspension = null;
        uint? topBoxCapacity = null;
        // ToDo: ArgumentException
        if (vehicleType == VehicleType.Airplain) { consoleUI.AddMessage("ving spann?"); wingSpan = uint.Parse(Console.ReadLine()!); }
        else if (vehicleType == VehicleType.Boat) { consoleUI.AddMessage("Skrov typ?"); hullType = uint.Parse(Console.ReadLine()!); }
        else if (vehicleType == VehicleType.Bus) { consoleUI.AddMessage("Buss typ?"); busType = uint.Parse(Console.ReadLine()!); }
        else if (vehicleType == VehicleType.Car) { consoleUI.AddMessage("Har en hjulupphängning mindre?"); hasOneLessWheelSuspension = bool.Parse(Console.ReadLine()!); }
        else if (vehicleType == VehicleType.Motorcycle) { consoleUI.AddMessage("Toppboxens kapacitet?"); topBoxCapacity = uint.Parse(Console.ReadLine()!); }
        else throw new NotImplementedException();

        // ToDo: fix converting
        (string registerNumber, string color, uint, VehicleType vehicleType, uint? wingSpan, uint? hullType, uint? busType, bool? hasOneLessWheelSuspension, uint? topBoxCapacity) result =
            (registerNumber, color, uint.Parse(numberOfWheels), vehicleType, wingSpan, hullType, busType, hasOneLessWheelSuspension, topBoxCapacity);

        return result;
    }

    void RemoveVehicle()
    {
        ArgumentNullException.ThrowIfNull(garageHandler);

        consoleUI.Clear();
        consoleUI.AddMessage("Ta bort fordon ur garaget");

        consoleUI.AddMessage("RegisterNumber?");
        string? registerNumber = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(registerNumber);

        // ToDo: Could not find the vehicle
        var vehicle = garageHandler.GetVehicle(registerNumber);
        ArgumentNullException.ThrowIfNull(vehicle);

        garageHandler.RemoveVehicle(vehicle);

        consoleUI.AddMessage("Fordonet tas bort ur garaget");

        consoleUI.AddMessage("Något att gå tillbaka till huvudmeny");
        Console.ReadKey();
    }

    void FindVehicle()
    {
        ArgumentNullException.ThrowIfNull(garageHandler);

        consoleUI.Clear();
        consoleUI.AddMessage("Hitta ett specifikt fordon");

        consoleUI.AddMessage("RegisterNumber?");
        string? registerNumber = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(registerNumber);

        // ToDo: Could not find the vehicle
        var vehicle = garageHandler.GetVehicle(registerNumber);
        ArgumentNullException.ThrowIfNull(vehicle);

        consoleUI.AddMessage(vehicle.Stats());
        consoleUI.AddMessage("Något att gå tillbaka till huvudmeny");
        Console.ReadKey();
    }

    void FilterVehicles()
    {
        consoleUI.Clear();
        consoleUI.AddMessage("Filtrera fordon");
        throw new NotImplementedException();
    }

    void ClosePrograme()
    {
        consoleUI.Clear();
        consoleUI.AddMessage("Stängs");
        Environment.Exit(0);
    }


}
