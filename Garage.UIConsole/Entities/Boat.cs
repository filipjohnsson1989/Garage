namespace Garage.UIConsole.Entities;

public class Boat : Vehicle
{
    public uint HullType { get; init; }

    public Boat(string registerNumber,
                    string color,
                    uint numberOfWheels,
                    uint hullType) : base(registerNumber, color, hullType) => HullType = hullType;
}