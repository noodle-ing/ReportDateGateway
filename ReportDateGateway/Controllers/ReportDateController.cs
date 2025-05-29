using Microsoft.AspNetCore.Mvc;
using ReportDateGateway.Models;
using ReportDateGateway.Services.DateService;

namespace ReportDateGateway.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ReportDateController : ControllerBase
{
    private readonly IReportDateService _reportDateService;

    public ReportDateController(IReportDateService reportDateService)
    {
        _reportDateService = reportDateService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ReportDateHttpRequest request)
    {
        if (request.DayOfMonth < 1 || request.DayOfMonth > 31)
            return BadRequest("dayOfMonth must be between 1 and 31");

        try
        {
            var resultDate = await _reportDateService.GetNextReportingDate(request);
            return Ok(new { resultDate });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"gRPC call failed: {ex.Message}");
        }
    }
}