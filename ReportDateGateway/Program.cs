using FluentValidation;
using FluentValidation.AspNetCore;
using ReportDateGateway.Services.DateService;
using ReportDateGateway.Validators;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IReportDateService, ReportDateGateway.Services.DateService.ReportDateService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<ReportDateHttpRequestValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

