using VehicleInsurance.API.Contracts;

namespace VehicleInsurance.API.Services
{
    public interface IUnderwritingService
    {
        Task<UnderwritingResponse> PerformUnderwriting(UnderwritingRequest request);
    }
}