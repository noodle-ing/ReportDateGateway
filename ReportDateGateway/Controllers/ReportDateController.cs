using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using ReportDateGateway.Models;
using ReportDateGateway.Services.DateService;

namespace ReportDateGateway.Controllers;

/// <summary>
/// API controller for handling requests related to reporting dates.
/// </summary>
[ApiController]
[Route("api/[controller]")]

public class ReportDateController : ControllerBase
{
    private readonly IReportDateService _reportDateService;
    /// <summary>
    /// Initializes a new instance of the <see cref="ReportDateController"/> class.
    /// </summary>
    /// <param name="reportDateService">The service used to retrieve the next reporting date.</param>
    public ReportDateController(IReportDateService reportDateService)
    {
        _reportDateService = reportDateService;
    }
    /// <summary>
    /// Calculates the next reporting date based on the input parameters.
    /// </summary>
    /// <param name="request">The HTTP request containing day of month, base date, and adjustment option.</param>
    /// <returns>The next reporting date or an appropriate error response.</returns>
    /// <response code="200">Returns the calculated reporting date.</response>
    /// <response code="400">If the input is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ReportDateHttpRequest request)
    {
        try
        {
            var resultDate = await _reportDateService.GetNextReportingDate(request);
            return Ok(new { resultDate });
        }
        catch (RpcException rpcEx)  
        {
            return HandleRpcException(rpcEx);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal error: {ex.Message}");
        }
    }
    
    /// <summary>
    /// Maps gRPC exceptions to corresponding HTTP responses.
    /// </summary>
    /// <param name="ex">The gRPC exception thrown during the call.</param>
    /// <returns>Appropriate IActionResult based on gRPC status code.</returns>
    private IActionResult HandleRpcException(RpcException ex) =>
        ex.StatusCode switch
        {
            Grpc.Core.StatusCode.InvalidArgument => BadRequest(ex.Status.Detail),
            Grpc.Core.StatusCode.NotFound => NotFound(ex.Status.Detail),
            Grpc.Core.StatusCode.Unavailable => StatusCode(503, "gRPC service unavailable"),
            _ => StatusCode(500, $"gRPC error: {ex.Status.Detail}")
        };
}