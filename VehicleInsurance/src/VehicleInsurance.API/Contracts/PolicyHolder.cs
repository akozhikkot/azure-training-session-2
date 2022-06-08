using System.ComponentModel.DataAnnotations;

namespace VehicleInsurance.API.Contracts
{
    public record PolicyHolder(
        [Required]
        string Title,
        [Required]
        string FirstName,
        [Required]
        string LastName,
        [Required]
        [EmailAddress]
        string Email,
        [Required]
        int Age,
        [Required]
        Address Address);
}