namespace Garage.DL.Entities;

public class Boat : Vehicle
{
    public uint HullType { get; init; }

    public Boat(Guid registerNumber,
                    string color,
                    uint numberOfWheels,
                    uint hullType) : base(registerNumber, color, hullType) => HullType = hullType;
}