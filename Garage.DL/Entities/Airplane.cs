namespace Garage.DL.Entities;

public class Airplane : Vehicle
{
    public uint WingSpan { get; init; }

    public Airplane(Guid registerNumber,
                    string color,
                    uint numberOfWheels,
                    uint wingSpan) : base(registerNumber, color, numberOfWheels) => WingSpan = wingSpan;

    public override string Stats() => $"{base.Stats()} Wing span:{WingSpan}";
}