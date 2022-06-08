using System.Text.Json;
using VehicleInsurance.API.Contracts;

namespace VehicleInsurance.API.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IWebHostEnvironment _environment;

        public VehicleRepository(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public bool LookupVehicle(VehicleInformation vehicleInformation)
        {
            // Lookup vehicle information from database
            // and populate vehicleInformation.Status
            var filePath = Path.Combine(_environment.ContentRootPath, @"\Data\vehicle-list.json");
            var vehicleListInfo = File.ReadAllText(filePath);
            var vehicleList =
                JsonSerializer.Deserialize<List<VehicleInformation>>(vehicleListInfo) ??
                new List<VehicleInformation>();
            return vehicleList.Any(x => x.Equals(vehicleInformation));
        }
    }
}