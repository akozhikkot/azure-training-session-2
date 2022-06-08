using Microsoft.Extensions.Caching.Distributed;
using VehicleInsurance.API.Contracts;

namespace VehicleInsurance.API.Services
{
    public class UnderwritingService : IUnderwritingService
    {
        private readonly IDistributedCache _cache;

        public UnderwritingService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<UnderwritingResponse> PerformUnderwriting(
            UnderwritingRequest request)
        {
            UnderwritingResponse response;

            // Some mocked Rules to simulate Rejection / acceptance / referral
            await Task.Delay(10);

            if (request.PolicyHolder.Age <= 18)
            {
                response = new UnderwritingResponse(
                    request.RequestId,
                    UnderwritingResult.DECLINED,
                    new List<string>
                    {
                            "PolicyHolder must be over 18 years old"
                    });
            }
            else if (request.PolicyHolder.Age >= 65)
            {
                response = new UnderwritingResponse(
                        request.RequestId,
                        UnderwritingResult.REFERRED,
                        new List<string>
                        {
                            "Referred for Manual Underwriting"
                        });
            }
            else
            {
                response = new UnderwritingResponse(
                        request.RequestId,
                        UnderwritingResult.ACCEPTED,
                        new List<string>());
            }
             
            return response;
        }
    }
}