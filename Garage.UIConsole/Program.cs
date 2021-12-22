// See https://aka.ms/new-console-template for more information
using Garage.UIConsole;

Console.WriteLine("Hej, Garage!");

GarageManager garageManager = new();

var vehicles = garageManager.ListOfVehicles();
foreach (var vehicle in vehicles)
{
    Console.WriteLine(vehicle.Stats());
}

Console.ReadLine();
