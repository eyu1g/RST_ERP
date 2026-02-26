using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Lead.Utility.Persistence;

public sealed class LeadDbContextFactory : IDesignTimeDbContextFactory<LeadDbContext>
{
    public LeadDbContext CreateDbContext(string[] args)
    {
        var cwd = Directory.GetCurrentDirectory();

        var candidates = new[]
        {
            Path.Combine(cwd, "Program"),
            cwd,
            Path.Combine(cwd, "Lead.API", "Program"),
            Path.Combine(cwd, "Svc.CRMM", "CRMM.Lead", "Lead.API", "Program"),
            Path.Combine(cwd, "..", "..", "..", "Lead.API", "Program"),
            Path.Combine(cwd, "..", "..", "..", "..", "Lead.API", "Program")
        };

        var basePath = candidates
            .Select(Path.GetFullPath)
            .FirstOrDefault(p => File.Exists(Path.Combine(p, "appsettings.json")))
            ?? Path.GetFullPath(candidates[0]);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString =
            configuration.GetConnectionString("LeadDb")
            ?? Environment.GetEnvironmentVariable("ConnectionStrings__LeadDb")
            ?? throw new InvalidOperationException("Connection string 'LeadDb' not found.");

        var optionsBuilder = new DbContextOptionsBuilder<LeadDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new LeadDbContext(optionsBuilder.Options);
    }
}
