namespace Garage.UIConsole.Entities;

public class Motorcycle : Vehicle
{
    public uint TopBoxCapacity { get; init; }

    public Motorcycle(string registerNumber,
                    string color,
                    uint numberOfWheels,
                    uint topBoxCapacity) : base(registerNumber, color, topBoxCapacity) => TopBoxCapacity = topBoxCapacity;
}