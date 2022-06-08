using System.ComponentModel.DataAnnotations;

namespace VehicleInsurance.API.Contracts
{
    public record VehicleInformation(
        [Required]
        string Make,
        [Required]
        string Model,
        [Required]
        int Year,
        [Required]
        TransmissionType TransmissionType,
        [Required]
        FuelType FuelType,
        [Required]
        string Trim);
}