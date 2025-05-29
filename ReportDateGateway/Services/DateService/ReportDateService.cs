using Grpc.Net.Client;
using ReportDateGateway.Models;
using ReportDateService; 

namespace ReportDateGateway.Services.DateService;

public class ReportDateService : IReportDateService
{
    private readonly IConfiguration _configuration;

    public ReportDateService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

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