namespace VehicleInsurance.API.Contracts
{
    public record UnderwritingRequest(
        Guid RequestId,
        VehicleInformation Vehicle,
        PolicyHolder PolicyHolder);
}