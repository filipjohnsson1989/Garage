namespace Garage.DL.Entities;

public class Motorcycle : Vehicle
{
    public uint TopBoxCapacity { get; init; }

    public Motorcycle(Guid registerNumber,
                    string color,
                    uint numberOfWheels,
                    uint topBoxCapacity) : base(registerNumber, color, topBoxCapacity) => TopBoxCapacity = topBoxCapacity;
}