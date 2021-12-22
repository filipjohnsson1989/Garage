namespace Garage.DL.Entities;

public class Bus : Vehicle
{
    public uint BusType { get; init; }

    public Bus(Guid registerNumber,
                    string color,
                    uint numberOfWheels,
                    uint busType) : base(registerNumber, color, busType) => BusType = busType;

}