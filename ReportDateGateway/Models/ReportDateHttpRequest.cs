using System.ComponentModel.DataAnnotations;

namespace ReportDateGateway.Models;

/// <summary>
/// Represents the HTTP request model for retrieving the next reporting date.
/// </summary>
public class ReportDateHttpRequest
{
    /// <summary>
    /// The day of the month on which the report is expected (e.g., 15 for the 15th day).
    /// Must be between 1 and 31.
    /// </summary>
    [Required]
    public int DayOfMonth { get; set; }

    /// <summary>
    /// The base date (in yyyy-MM-dd format) from which the next reporting date is calculated.
    /// If not specified, the current date will be used.
    /// </summary>
    public string? Date { get; set; }

    /// <summary>
    /// Indicates whether the result should adjust the date if the day does not exist in the target month.
    /// If true, it will use the last valid day of that month.
    /// </summary>
    public bool? Adjust { get; set; }
}