namespace VehicleInsurance.API.Contracts
{
    public record UnderwritingResponse(
        Guid RequestId,
        UnderwritingResult Result,
        List<string> Observations);
}