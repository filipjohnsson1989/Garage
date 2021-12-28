using Garage.Common;
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

    private void ParkVehicle(string registerNumber,
                           string color,
                           uint numberOfWheels,
                           VehicleType vehicleType,
                           uint? wingSpan,
                           uint? hullType,
                           uint? busType,
                           bool? hasOneLessWheelSuspension,
                           uint? topBoxCapacity)
    {
        ArgumentNullException.ThrowIfNull(garageHandler);

        var result = garageHandler.CreateVehicle(registerNumber, color, numberOfWheels, vehicleType, wingSpan, hullType, busType, hasOneLessWheelSuspension, topBoxCapacity);

        garageHandler.AddVehicle(result);
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
        consoleUI.Clear();
        consoleUI.AddMessage("Instansieringen av ett nytt garage");
        consoleUI.AddMessage("Kapacitet?");


        string? input = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(input);


        if (string.IsNullOrEmpty(input) || !uint.TryParse(input, out uint capacity))
            throw new ArgumentException();

        garageHandler = new(capacity);

        consoleUI.AddMessage($"Garaget har {capacity} kapacitet kvar");

    }

    void CreateGarageWithVehicles()
    {
        consoleUI.Clear();
        consoleUI.AddMessage("Instansieringen av ett nytt garage med ett antal fordon");
        throw new NotImplementedException();
    }

    void ListOfVehicles()
    {
        ArgumentNullException.ThrowIfNull(garageHandler);

        consoleUI.Clear();
        consoleUI.AddMessage("Lista samtliga parkerade fordon");

        var vehicles = garageHandler.GetVehicles();
        foreach (var vehicle in vehicles)
        {
            consoleUI.AddMessage(vehicle.Stats() + "\n\r");
        }
        consoleUI.AddMessage("Något att gå tillbaka till huvudmeny");
        Console.ReadKey();
    }

    void ListOfVehicleTypes()
    {
        consoleUI.Clear();
        consoleUI.AddMessage("Lista fordonstyper och hur många av varje som står i garaget");
        throw new NotImplementedException();
    }

    void AddVehicle()
    {
        consoleUI.Clear();
        consoleUI.AddMessage("Lägga till fordon garaget");
        do
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
                break;

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

            this.ParkVehicle(registerNumber, color, uint.Parse(numberOfWheels), vehicleType, wingSpan, hullType, busType, hasOneLessWheelSuspension, topBoxCapacity);


            consoleUI.AddMessage("Fordonet läggs till garaget");

        } while (true);
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

    void CreateGarage(uint capacity)
    {
        garageHandler = new(capacity);
    }





}
