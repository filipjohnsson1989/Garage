using Garage.Common;
using Garage.UIConsole.Entities;

namespace Garage.UIConsole;

public class GarageManager
{

    private GarageHandler? garageHandler = default!;

    public void CreateGarage(uint capacity)
    {
        garageHandler = new(capacity);
    }

    public IVehicle CreateVehicle(string registerNumber,
                               string color,
                               uint numberOfWheels,
                               VehicleType vehicleType,
                               uint? wingSpan = null,
                               uint? hullType = null,
                               uint? busType = null,
                               bool? hasOneLessWheelSuspension = null,
                               uint? topBoxCapacity = null)
    {
        dynamic vehicle = new System.Dynamic.ExpandoObject();

        vehicle.RegisterNumber = registerNumber;
        vehicle.Color = color;
        vehicle.NumberOfWheels = numberOfWheels;

        var result = vehicleType switch
        {
            VehicleType.Airplain => CreateAirplane(vehicle, wingSpan!.Value),
            VehicleType.Boat => CreateBoat(vehicle, hullType!.Value),
            VehicleType.Bus => CreateBus(vehicle, busType!.Value),
            VehicleType.Car => CreateCar(vehicle, hasOneLessWheelSuspension!.Value),
            VehicleType.Motorcycle => CreateMotorcycle(vehicle, topBoxCapacity!.Value),
            _ => throw new NotImplementedException(),
        };

        return result;
    }
    public IVehicle CreateAirplane(dynamic vehicle,
                                   uint wingSpan) => new Airplane(vehicle.RegisterNumber, vehicle.Color, vehicle.NumberOfWheels, wingSpan);

    public IVehicle CreateBoat(dynamic vehicle,
                               uint hullType) => new Boat(vehicle.RegisterNumber, vehicle.Color, vehicle.NumberOfWheels, hullType);

    public IVehicle CreateBus(dynamic vehicle,
                              uint busType) => new Bus(vehicle.RegisterNumber, vehicle.Color, vehicle.NumberOfWheels, busType);

    public IVehicle CreateCar(dynamic vehicle,
                              bool hasOneLessWheelSuspension) => new Car(vehicle.RegisterNumber, vehicle.Color, vehicle.NumberOfWheels, hasOneLessWheelSuspension);

    public IVehicle CreateMotorcycle(dynamic vehicle,
                                     uint topBoxCapacity) => new Motorcycle(vehicle.RegisterNumber, vehicle.Color, vehicle.NumberOfWheels, topBoxCapacity);


    public void ParkVehicle(IVehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(garageHandler);

        garageHandler.AddVehicle(vehicle);
        //new Bus(registerNumber: new string(), color: "Red", numberOfWheels: 6, busType: 1)
        //garageHandler.AddVehicle(new Bus(registerNumber: new string(), color: "Blue", numberOfWheels: 4, busType: 2));
    }


    public void UnparkVehicle(IVehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(garageHandler);

        garageHandler.RemoveVehicle(vehicle);
    }
    public IEnumerable<IVehicle> ListOfVehicles()
    {
        ArgumentNullException.ThrowIfNull(garageHandler);

        return garageHandler.GetVehicles();
    }
}
