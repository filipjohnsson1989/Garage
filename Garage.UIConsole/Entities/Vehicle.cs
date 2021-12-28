namespace Garage.UIConsole.Entities;

public abstract class Vehicle : IVehicle
{
    public string RegisterNumber { get; init; }
    public string Color { get; init; }
    public uint NumberOfWheels { get; init; }

    public Vehicle(string registerNumber, string color, uint numberOfWheels)
    {
        RegisterNumber = registerNumber;
        Color = color;
        NumberOfWheels = numberOfWheels;
    }

    public virtual string Stats() => $"Register Number:{RegisterNumber} Color:{Color} Vehicle:{GetType().Name}";
}