using System.Globalization;
using FluentValidation;
using ReportDateGateway.Models;

namespace ReportDateGateway.Validators;

public class ReportDateHttpRequestValidator : AbstractValidator<ReportDateHttpRequest>
{
    public ReportDateHttpRequestValidator()
    {
        RuleFor(x => x.DayOfMonth)
            .InclusiveBetween(1, 31);

        RuleFor(x => x.Date)
            .Cascade(CascadeMode.Stop)
            .Must(BeValidIsoDate)
            .When(x => !string.IsNullOrWhiteSpace(x.Date))
            .WithMessage("date must be valid ISO 8601: yyyy-MM-dd and valid calendar date");
    }

    private static bool BeValidIsoDate(string? date) =>
        DateOnly.TryParseExact(date!, "yyyy-MM-dd",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
}