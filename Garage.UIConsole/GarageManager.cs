using Garage.Common;
using Garage.BL;
using Garage.DL.Entities;

namespace Garage.UIConsole;

public class GarageManager
{

    public GarageHandler garageHandler;

    public GarageManager()
    {
        List<IVehicle> vehicles = new List<IVehicle>();
        vehicles.Add((IVehicle)new Bus(registerNumber: new Guid(), color: "Red", numberOfWheels: 6, busType: 1));

        garageHandler = new(3, vehicles);
        garageHandler.AddVehicle((IVehicle)new Bus(registerNumber: new Guid("BusB"), color: "Blue", numberOfWheels: 4, busType: 2));
    }

    public IEnumerable<IVehicle> ListOfVehicles()
    {
        return garageHandler.GetVehicles();
    }
}
