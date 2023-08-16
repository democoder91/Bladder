using Bladder.Data;
using Bladder.Localization;
using Bladder.Services;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Localization;
using Serilog;
using Serilog.Events;
using Volo.Abp.Data;

namespace Bladder;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        var loggerConfiguration = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console());


        if (IsMigrateDatabase(args))
        {
            loggerConfiguration.MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning);
            loggerConfiguration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
        }

        Log.Logger = loggerConfiguration.CreateLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddScoped<ILocalizationServiceCustom, LocalizationServiceCustom>();
            builder.Services.AddScoped<IBuildingMachineService, BuildingMachineService>();
            builder.Services.AddScoped<IBuildingBladderService, BuildingBladderService>();
            builder.Services.AddScoped<IFindingService, FindingService>();
            builder.Services.AddScoped<IBladderTransactionService, BladderTransactionService>();
            builder.Services.AddScoped<IBladderExpirationService, BladderExpirationService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(builder.Configuration.GetConnectionString("Default"), new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        }));
            builder.Services.AddHangfireServer();

            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            
            if (IsMigrateDatabase(args))
            {
                builder.Services.AddDataMigrationEnvironment();
            }
            await builder.AddApplicationAsync<BladderModule>();
            var app = builder.Build();
            app.UseHangfireDashboard();
            RecurringJob.AddOrUpdate<IBladderExpirationService>("CheckBladderExpiration", bladderExpirationService => bladderExpirationService.CheckAndSendNotificationsAsync(), Cron.Minutely);
            await app.InitializeApplicationAsync();

            if (IsMigrateDatabase(args))
            {
                await app.Services.GetRequiredService<BladderDbMigrationService>().MigrateAsync();
                return 0;
            }
            Log.Information("Starting Bladder.");
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException)
            {
                throw;
            }

            Log.Fatal(ex, "Bladder terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static bool IsMigrateDatabase(string[] args)
    {
        return args.Any(x => x.Contains("--migrate-database", StringComparison.OrdinalIgnoreCase));
    }
}
