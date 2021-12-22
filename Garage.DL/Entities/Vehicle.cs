namespace Garage.DL.Entities;

public abstract class Vehicle
{
    public Guid RegisterNumber { get; init; }
    public string Color { get; init; }
    public uint NumberOfWheels { get; init; }

    public Vehicle(Guid registerNumber, string color, uint numberOfWheels)
    {
        RegisterNumber = registerNumber;
        Color = color;
        NumberOfWheels = numberOfWheels;
    }

    public virtual string Stats() => $"Register Number:{RegisterNumber} Color:{Color} Vehicle:{this.GetType().Name}";
}