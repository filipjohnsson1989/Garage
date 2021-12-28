using Garage.Common;
using System;

namespace Garage.UIConsole
{
    class Program
    {
        private static GarageManager garageManager = new GarageManager();

        /// <summary>
        /// The main method, vill handle the menues for the program
        /// </summary>
        /// <param name="args"></param>
        static void Main()
        {
            do
            {
                // Huvudmeny
                ShowMainMenu();
                GetUserInput();
            } while (true);
        }
        static void ShowMainMenu()
        {
            UI.Clear();
            UI.AddMessage("Vänligen navigera genom menyn genom att ange numret \n(1, 2, 3, ..., 8, 0) som du väljer med hjälp av numeriska tangenter"
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

        static void GetUserInput()
        {
            var keyPressed = UI.GetKey();//Creates the Key input to be used with the switch-case below.

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


        static void CreateGarage()
        {
            UI.Clear();
            UI.AddMessage("Instansieringen av ett nytt garage");
            UI.AddMessage("Kapacitet?");


            string? input = Console.ReadLine();
            ArgumentNullException.ThrowIfNull(input);


            if (string.IsNullOrEmpty(input) || !uint.TryParse(input, out uint capacity))
                throw new ArgumentException();

            garageManager.CreateGarage(capacity);

            UI.AddMessage($"Garaget har {capacity} kapacitet kvar");

        }

        static void CreateGarageWithVehicles()
        {
            UI.Clear();
            UI.AddMessage("Instansieringen av ett nytt garage med ett antal fordon");
            throw new NotImplementedException();
        }

        static void ListOfVehicles()
        {
            UI.Clear();
            UI.AddMessage("Lista samtliga parkerade fordon");

            var vehicles = garageManager.ListOfVehicles();
            foreach (var vehicle in vehicles)
            {
                UI.AddMessage(vehicle.Stats() + "\n\r");
            }
            UI.AddMessage("Något att gå tillbaka till huvudmeny");
            Console.ReadKey();
        }

        static void ListOfVehicleTypes()
        {
            UI.Clear();
            UI.AddMessage("Lista fordonstyper och hur många av varje som står i garaget");
            throw new NotImplementedException();
        }

        static void AddVehicle()
        {
            UI.Clear();
            UI.AddMessage("Lägga till fordon garaget");
            do
            {
                UI.AddMessage("Vilket fordon vill du läga till?"
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

                UI.AddMessage("Registreringsnummer?");
                string? registerNumber = Console.ReadLine();
                ArgumentNullException.ThrowIfNull(registerNumber);

                UI.AddMessage("Färg?");
                string? color = Console.ReadLine();
                ArgumentNullException.ThrowIfNull(color);

                UI.AddMessage("Antal hjul?");
                string? numberOfWheels = Console.ReadLine();
                ArgumentNullException.ThrowIfNull(numberOfWheels);

                dynamic vehicle = new System.Dynamic.ExpandoObject();

                vehicle.RegisterNumber = registerNumber;
                vehicle.Color = color;
                vehicle.NumberOfWheels = uint.Parse(numberOfWheels);


                uint? wingSpan = null;
                uint? hullType = null;
                uint? busType = null;
                bool? hasOneLessWheelSuspension = null;
                uint? topBoxCapacity = null;
                // ToDo: ArgumentException
                if (vehicleType == VehicleType.Airplain) { UI.AddMessage("ving spann?"); wingSpan = uint.Parse(Console.ReadLine()!); }
                else if (vehicleType == VehicleType.Boat) { UI.AddMessage("Skrov typ?"); hullType = uint.Parse(Console.ReadLine()!); }
                else if (vehicleType == VehicleType.Bus) { UI.AddMessage("Buss typ?"); busType = uint.Parse(Console.ReadLine()!); }
                else if (vehicleType == VehicleType.Car) { UI.AddMessage("Har en hjulupphängning mindre?"); hasOneLessWheelSuspension = bool.Parse(Console.ReadLine()!); }
                else if (vehicleType == VehicleType.Motorcycle) { UI.AddMessage("Toppboxens kapacitet?"); topBoxCapacity = uint.Parse(Console.ReadLine()!); }
                else throw new NotImplementedException();

                garageManager.ParkVehicle(vehicle, vehicleType, wingSpan, hullType, busType, hasOneLessWheelSuspension, topBoxCapacity);


                UI.AddMessage("Fordonet läggs till garaget");

            } while (true);
        }

        static void RemoveVehicle()
        {
            UI.Clear();
            UI.AddMessage("Ta bort fordon ur garaget");

            UI.AddMessage("RegisterNumber?");
            string? registerNumber = Console.ReadLine();
            ArgumentNullException.ThrowIfNull(registerNumber);

            var vehicle = garageManager.FindVehicle(registerNumber);
            garageManager.UnparkVehicle(vehicle);

            UI.AddMessage("Fordonet tas bort ur garaget");

            UI.AddMessage("Något att gå tillbaka till huvudmeny");
            Console.ReadKey();
        }

        static void FindVehicle()
        {
            UI.Clear();
            UI.AddMessage("Hitta ett specifikt fordon");

            UI.AddMessage("RegisterNumber?");
            string? registerNumber = Console.ReadLine();
            ArgumentNullException.ThrowIfNull(registerNumber);

            var vehicle = garageManager.FindVehicle(registerNumber);
            UI.AddMessage(vehicle.Stats());


            UI.AddMessage("Något att gå tillbaka till huvudmeny");
            Console.ReadKey();
        }

        static void FilterVehicles()
        {
            UI.Clear();
            UI.AddMessage("Filtrera fordon");
            throw new NotImplementedException();
        }

        static void ClosePrograme()
        {
            UI.Clear();
            UI.AddMessage("Stängs");
            Environment.Exit(0);
        }
    }
}

