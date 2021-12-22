// See https://aka.ms/new-console-template for more information
using Garage.UIConsole;

Console.WriteLine("Hej, Garage!");

GarageManager garageManager = new();

foreach (var vehicle in garageManager.ListOfVehicles())
{
    Console.WriteLine(vehicle);
}


