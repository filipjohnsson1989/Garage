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


    private IVehicle CreateAirplane(dynamic vehicle,
                                   uint wingSpan) => new Airplane(vehicle.RegisterNumber, vehicle.Color, vehicle.NumberOfWheels, wingSpan);

    private IVehicle CreateBoat(dynamic vehicle,
                               uint hullType) => new Boat(vehicle.RegisterNumber, vehicle.Color, vehicle.NumberOfWheels, hullType);

    private IVehicle CreateBus(dynamic vehicle,
                              uint busType) => new Bus(vehicle.RegisterNumber, vehicle.Color, vehicle.NumberOfWheels, busType);

    private IVehicle CreateCar(dynamic vehicle,
                              bool hasOneLessWheelSuspension) => new Car(vehicle.RegisterNumber, vehicle.Color, vehicle.NumberOfWheels, hasOneLessWheelSuspension);

    private IVehicle CreateMotorcycle(dynamic vehicle,
                                     uint topBoxCapacity) => new Motorcycle(vehicle.RegisterNumber, vehicle.Color, vehicle.NumberOfWheels, topBoxCapacity);


    public void ParkVehicle(dynamic vehicle, VehicleType vehicleType, uint? wingSpan, uint? hullType, uint? busType, bool? hasOneLessWheelSuspension, uint? topBoxCapacity)
    {
        ArgumentNullException.ThrowIfNull(garageHandler);

        IVehicle result = vehicleType switch
        {
            VehicleType.Airplain => CreateAirplane(vehicle, wingSpan!.Value),
            VehicleType.Boat => CreateBoat(vehicle, hullType!.Value),
            VehicleType.Bus => CreateBus(vehicle, busType!.Value),
            VehicleType.Car => CreateCar(vehicle, hasOneLessWheelSuspension!.Value),
            VehicleType.Motorcycle => CreateMotorcycle(vehicle, topBoxCapacity!.Value),
            _ => throw new NotImplementedException(),
        };


        garageHandler.AddVehicle(result);
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

    public IVehicle FindVehicle(string registerNumber)
    {
        ArgumentNullException.ThrowIfNull(garageHandler);

        var vehicle= garageHandler.GetVehicle(registerNumber);
        ArgumentNullException.ThrowIfNull(vehicle);

        return vehicle;
    }
}
