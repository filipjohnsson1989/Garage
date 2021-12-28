using Garage.UIConsole.Entities;

namespace Garage.UIConsole;

public class GarageHandler
{
    private IGarage<IVehicle> garage;
    public GarageHandler(uint capacity, IEnumerable<IVehicle>? vehicles = null)
    {
        if (vehicles is not null && vehicles.Count() > capacity)
            throw new ArgumentException();

        garage = new Garage<IVehicle>(capacity);

        if (vehicles is not null)
            foreach (var vehicle in vehicles)
            {
                AddVehicle(vehicle);
            }

    }


    public bool AddVehicle(IVehicle vehicle) => garage.Add(vehicle);

    public bool RemoveVehicle(IVehicle vehicle) => garage.Remove(vehicle);

    public IEnumerable<IVehicle> GetVehicles() => garage;
}
