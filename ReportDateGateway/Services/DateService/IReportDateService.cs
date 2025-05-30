using ReportDateGateway.Models;

namespace ReportDateGateway.Services.DateService;
/// <summary>
/// Interface for retrieving the next reporting date based on request parameters.
/// </summary>
public interface IReportDateService
{
    /// <summary>
    /// Gets the next reporting date based on the specified request.
    /// </summary>
    /// <param name="request">The request containing day of month, optional base date, and adjust flag.</param>
    /// <returns>A task representing the asynchronous operation, containing the next reporting date as a string in "yyyy-MM-dd" format.</returns>
    Task<string> GetNextReportingDate(ReportDateHttpRequest request);
}