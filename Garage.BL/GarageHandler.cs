
using Garage.Common;

namespace Garage.BL;

public class GarageHandler
{
    private IGarage<IVehicle> garage;
    public GarageHandler(uint capacity, IEnumerable<IVehicle>? vehicles = null)
    {
        if (vehicles is not null && vehicles.Count() > capacity)
            throw new ArgumentException();

        garage = new Garage<IVehicle>(capacity);
    }
    public bool AddVehicle(IVehicle vehicle) => garage.Add(vehicle);

    public bool RemoveVehicle(IVehicle vehicle) => garage.Remove(vehicle);

    public IEnumerable<IVehicle> GetVehicles() => garage;
}
