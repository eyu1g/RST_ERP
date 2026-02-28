using Lead.App.Interfaces;
using Lead.App.Service;
using Lead.Api.Command;
using Lead.Api.Queries;
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
builder.Services.AddSingleton<LeadGroupSettingsService>();
builder.Services.AddScoped<LeadImportService>();

builder.Services.AddScoped<LeadQueries>();
builder.Services.AddScoped<LeadGroupQueries>();
builder.Services.AddScoped<LookupQueries>();

builder.Services.AddScoped<CreateLeadCommand>();
builder.Services.AddScoped<UpdateLeadCommand>();
builder.Services.AddScoped<ChangeLeadStatusCommand>();
builder.Services.AddScoped<AssignLeadCommand>();
builder.Services.AddScoped<AddLeadScoreCommand>();
builder.Services.AddScoped<ConvertLeadCommand>();
builder.Services.AddScoped<DeleteLeadCommand>();
builder.Services.AddScoped<ImportLeadsCommand>();

builder.Services.AddScoped<CreateLeadGroupCommand>();
builder.Services.AddScoped<UpdateLeadGroupCommand>();
builder.Services.AddScoped<DeleteLeadGroupCommand>();
builder.Services.AddScoped<CreateLeadGroupConditionCommand>();
builder.Services.AddScoped<UpdateLeadGroupConditionCommand>();
builder.Services.AddScoped<DeleteLeadGroupConditionCommand>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LeadDbContext>();

    db.Database.Migrate();

    if (!db.LeadSources.Any())
    {
        db.LeadSources.Add(new LeadSource
        {
            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Name = "Manual"
        });
    }

    if (!db.LeadStatuses.Any())
    {
        db.LeadStatuses.AddRange(
            new LeadStatus
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "New",
                IsActive = true,
                SortOrder = 0
            },
            new LeadStatus
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                Name = "Contacted",
                IsActive = true,
                SortOrder = 1
            },
            new LeadStatus
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111113"),
                Name = "Qualified",
                IsActive = true,
                SortOrder = 2
            },
            new LeadStatus
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111114"),
                Name = "Nurturing",
                IsActive = true,
                SortOrder = 3
            },
            new LeadStatus
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111115"),
                Name = "Disqualified",
                IsActive = true,
                SortOrder = 4
            },
            new LeadStatus
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111116"),
                Name = "Converted",
                IsActive = true,
                SortOrder = 5
            });
    }

    if (!db.LeadStages.Any())
    {
        db.LeadStages.AddRange(
            new LeadStage
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222221"),
                Name = "Capture",
                IsActive = true,
                SortOrder = 0
            },
            new LeadStage
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Profiling",
                IsActive = true,
                SortOrder = 1
            },
            new LeadStage
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222223"),
                Name = "Qualification",
                IsActive = true,
                SortOrder = 2
            },
            new LeadStage
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222224"),
                Name = "Assignment",
                IsActive = true,
                SortOrder = 3
            },
            new LeadStage
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222225"),
                Name = "Nurturing",
                IsActive = true,
                SortOrder = 4
            },
            new LeadStage
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222226"),
                Name = "Conversion",
                IsActive = true,
                SortOrder = 5
            });
    }

    db.SaveChanges();
}

app.MapOpenApi();
app.MapScalarApiReference();

app.MapControllers();

app.Run();
