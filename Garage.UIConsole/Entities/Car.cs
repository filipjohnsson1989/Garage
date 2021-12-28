namespace Garage.UIConsole.Entities;

public class Car : Vehicle
{
    public bool HasOneLessWheelSuspension { get; init; }

    public Car(string registerNumber,
                    string color,
                    uint numberOfWheels,
                    bool hasOneLessWheelSuspension) : base(registerNumber, color, numberOfWheels) => HasOneLessWheelSuspension = hasOneLessWheelSuspension;
}