namespace ReportDateGateway.Models;

public class ReportDateHttpRequest
{
    public int DayOfMonth { get; set; }
    public string? Date { get; set; }
    public bool? Adjust { get; set; }
}