namespace Garage.UIConsole.Entities;

public class Bus : Vehicle
{
    public uint BusType { get; init; }

    public Bus(string registerNumber,
                    string color,
                    uint numberOfWheels,
                    uint busType) : base(registerNumber, color, busType) => BusType = busType;

}