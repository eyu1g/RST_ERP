using Lead.App.Service;
using Lead.App.Interfaces;
using Lead.Utility.Persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Lead.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Path.Combine(builder.Environment.ContentRootPath, "Program"))
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.WebHost.UseUrls(
    "https://localhost:1301",
    "http://localhost:1300");

builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = Asp.Versioning.ApiVersionReader.Combine(
        new Asp.Versioning.UrlSegmentApiVersionReader());
});
builder.Services.AddOpenApi();

builder.Services.AddDbContext<LeadDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LeadDb")));

builder.Services.AddScoped<ILeadDbContext>(sp => sp.GetRequiredService<LeadDbContext>());
builder.Services.AddScoped<LeadService>();
builder.Services.AddScoped<LeadGroupService>();
builder.Services.AddScoped<LeadLookupService>();
builder.Services.AddSingleton<LeadGroupSettingsService>();
builder.Services.AddScoped<LeadImportService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LeadDbContext>();

    db.Database.Migrate();

    if (!db.LeadSources.Any())
    {
        db.LeadSources.Add(new LeadSource(Guid.Parse("33333333-3333-3333-3333-333333333333"), "Manual"));
    }

    if (!db.LeadStatuses.Any())
    {
        db.LeadStatuses.AddRange(
            new LeadStatus(Guid.Parse("11111111-1111-1111-1111-111111111111"), "New", true, 0),
            new LeadStatus(Guid.Parse("11111111-1111-1111-1111-111111111112"), "Contacted", true, 1),
            new LeadStatus(Guid.Parse("11111111-1111-1111-1111-111111111113"), "Qualified", true, 2),
            new LeadStatus(Guid.Parse("11111111-1111-1111-1111-111111111114"), "Nurturing", true, 3),
            new LeadStatus(Guid.Parse("11111111-1111-1111-1111-111111111115"), "Disqualified", true, 4),
            new LeadStatus(Guid.Parse("11111111-1111-1111-1111-111111111116"), "Converted", true, 5));
    }

    if (!db.LeadStages.Any())
    {
        db.LeadStages.AddRange(
            new LeadStage(Guid.Parse("22222222-2222-2222-2222-222222222221"), "Capture", true, 0),
            new LeadStage(Guid.Parse("22222222-2222-2222-2222-222222222222"), "Profiling", true, 1),
            new LeadStage(Guid.Parse("22222222-2222-2222-2222-222222222223"), "Qualification", true, 2),
            new LeadStage(Guid.Parse("22222222-2222-2222-2222-222222222224"), "Assignment", true, 3),
            new LeadStage(Guid.Parse("22222222-2222-2222-2222-222222222225"), "Nurturing", true, 4),
            new LeadStage(Guid.Parse("22222222-2222-2222-2222-222222222226"), "Conversion", true, 5));
    }

    db.SaveChanges();
}

app.MapOpenApi();
app.MapScalarApiReference();

app.MapControllers();

app.Run();
