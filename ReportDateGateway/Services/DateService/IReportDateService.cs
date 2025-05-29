using ReportDateGateway.Models;

namespace ReportDateGateway.Services.DateService;

public interface IReportDateService
{
    Task<string> GetNextReportingDate(ReportDateHttpRequest request);
}