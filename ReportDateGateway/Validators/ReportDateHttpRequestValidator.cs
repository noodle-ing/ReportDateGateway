using System.Globalization;
using FluentValidation;
using ReportDateGateway.Models;

namespace ReportDateGateway.Validators;
/// <summary>
/// Validates incoming HTTP requests for reporting date calculations.
/// </summary>
public class ReportDateHttpRequestValidator : AbstractValidator<ReportDateHttpRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReportDateHttpRequestValidator"/> class.
    /// Sets validation rules for the <see cref="ReportDateHttpRequest"/> model.
    /// </summary>
    public ReportDateHttpRequestValidator()
    {
        // Validates that DayOfMonth is between 1 and 31
        RuleFor(x => x.DayOfMonth)
            .InclusiveBetween(1, 31);
        // Validates that Date, if provided, is a valid ISO 8601 calendar date (yyyy-MM-dd)
        RuleFor(x => x.Date)
            .Cascade(CascadeMode.Stop)
            .Must(BeValidIsoDate)
            .When(x => !string.IsNullOrWhiteSpace(x.Date))
            .WithMessage("date must be valid ISO 8601: yyyy-MM-dd and valid calendar date");
    }
    /// <summary>
    /// Validates that the date string is in ISO 8601 format and is a valid calendar date.
    /// </summary>
    /// <param name="date">The date string to validate.</param>
    /// <returns><c>true</c> if the string is a valid ISO date; otherwise, <c>false</c>.</returns>
    private static bool BeValidIsoDate(string? date) =>
        DateOnly.TryParseExact(date!, "yyyy-MM-dd",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
}