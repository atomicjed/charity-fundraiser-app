using CharityFundraiserApp.ApplicationDatabase.Config;
using CharityFundraiserApp.ApplicationDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CharityFundraiserApp.ApplicationDatabase.EfEnhancements;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationContext(this IServiceCollection services, ApplicationDatabaseSettings applicationDatabaseSettings)
        {
            ForcePostgresDriverToNotCareAboutTimezones();
            return services
                .AddDbContext<ApplicationContext>(
                    options =>
                    {
                        options.UseNpgsql(
                            applicationDatabaseSettings.CreateConnectionString(),
                            builder => builder.MigrationsAssembly(
                                    typeof(ApplicationContext).Assembly.GetName()
                                        .Name)
                                .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                    });
        }

        private static void ForcePostgresDriverToNotCareAboutTimezones()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
}