using System.ComponentModel.DataAnnotations;

namespace VehicleInsurance.API.Contracts
{
    public record Address(
        [Required]
        string Line1,
        [Required]
        string Line2,
        [Required]
        string City,
        [Required]
        string PostCode);
}