
namespace Garage.Common
{
    public interface IVehicle
    {
        string Color { get; init; }
        uint NumberOfWheels { get; init; }
        Guid RegisterNumber { get; init; }

        string Stats();
    }
}