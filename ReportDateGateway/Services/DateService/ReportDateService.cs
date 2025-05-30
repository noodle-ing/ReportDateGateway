using Grpc.Net.Client;
using ReportDateGateway.Models;
using ReportDateService; 

namespace ReportDateGateway.Services.DateService;

public class ReportDateService : IReportDateService
{
    /// <summary>
    /// Implementation of <see cref="IReportDateService"/> that communicates with a gRPC service to retrieve the next reporting date.
    /// </summary>
    private readonly IConfiguration _configuration;
    /// <summary>
    /// Initializes a new instance of the <see cref="ReportDateService"/> class.
    /// </summary>
    /// <param name="configuration">Application configuration used to get the gRPC service URL.</param>
    public ReportDateService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    /// <summary>
    /// Gets the next reporting date by calling the gRPC service with the provided request data.
    /// </summary>
    /// <param name="request">The HTTP request model containing the day of month, optional date, and optional adjustment flag.</param>
    /// <returns>The next reporting date as a string in "yyyy-MM-dd" format.</returns>
    public async Task<string> GetNextReportingDate(ReportDateHttpRequest request)
    {
        var grpcUrl = _configuration["GrpcSettings:ServiceUrl"];
        using var channel = GrpcChannel.ForAddress(grpcUrl);
        var client = new ReportingDate.ReportingDateClient(channel); 

        var dateToUse = request.Date ?? DateTime.UtcNow.ToString("yyyy-MM-dd");
        var adjust = request.Adjust ?? true;

        
        var grpcRequest = new ReportingDateRequest
        {
            DayOfMonth = request.DayOfMonth,
            Date = dateToUse,
            Adjust = adjust
        };

        var response = await client.GetNextReportingDateAsync(grpcRequest); 
        return response.NextDate;
    }
}