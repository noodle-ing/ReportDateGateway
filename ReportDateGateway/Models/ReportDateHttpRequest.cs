using System.ComponentModel.DataAnnotations;

namespace ReportDateGateway.Models;

public class ReportDateHttpRequest
{
    [Required]
    public int DayOfMonth { get; set; }
    [Required]
    public string? Date { get; set; }
    public bool? Adjust { get; set; }
}