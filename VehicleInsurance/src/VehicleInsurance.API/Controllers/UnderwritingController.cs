using Microsoft.AspNetCore.Mvc;
using VehicleInsurance.API.Contracts;
using VehicleInsurance.API.Services;

namespace VehicleInsurance.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UnderwritingController : ControllerBase
{
    private readonly ILogger<UnderwritingController> _logger;
    private readonly IUnderwritingService _underwritingService;
    
    public UnderwritingController(
        ILogger<UnderwritingController> logger,
        IUnderwritingService underwritingService)
    {
        _logger = logger;
        _underwritingService = underwritingService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(UnderwritingRequest request)
    {
        var result = await _underwritingService.PerformUnderwriting(request);
        return Ok(result);
    }
}