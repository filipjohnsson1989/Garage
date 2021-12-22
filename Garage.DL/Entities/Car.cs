namespace Garage.DL.Entities;

public class Car : Vehicle
{
    public bool HasOneLessWheelSuspension { get; init; }

    public Car(Guid registerNumber,
                    string color,
                    uint numberOfWheels,
                    bool hasOneLessWheelSuspension) : base(registerNumber, color, numberOfWheels) => HasOneLessWheelSuspension = hasOneLessWheelSuspension;
}