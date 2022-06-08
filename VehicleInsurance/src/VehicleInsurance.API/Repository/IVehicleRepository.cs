using VehicleInsurance.API.Contracts;

namespace VehicleInsurance.API.Repository
{
    public interface IVehicleRepository
    {
        bool LookupVehicle(VehicleInformation vehicleInformation);
    }
}