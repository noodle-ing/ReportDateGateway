using Grpc.Core;
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
    private IActionResult HandleRpcException(RpcException ex) =>
        ex.StatusCode switch
        {
            Grpc.Core.StatusCode.InvalidArgument => BadRequest(ex.Status.Detail),
            Grpc.Core.StatusCode.NotFound => NotFound(ex.Status.Detail),
            Grpc.Core.StatusCode.Unavailable => StatusCode(503, "gRPC service unavailable"),
            _ => StatusCode(500, $"gRPC error: {ex.Status.Detail}")
        };
}