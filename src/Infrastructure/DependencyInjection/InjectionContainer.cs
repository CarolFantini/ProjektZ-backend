using Application.Interfaces;
using Application.Services;
using Application.Settings;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.DependencyInjection
{
    public static class InjectionContainer
    {
        public static IServiceCollection InfraInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSettings();
            services.RegisterServices();
            services.ConfigureDatabase();
            services.RegisterRepositories();

            return services;
        }

        private static IServiceCollection ConfigureSettings(this IServiceCollection services)
        {
            services.AddOptions<ConnectionStringsSettings>().BindConfiguration("ConnectionStrings");

            return services;
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IReadingJournalService, ReadingJournalService>();
            services.AddScoped<IHouseholdBudgetService, HouseholdBudgetService>();

            return services;
        }

        private static IServiceCollection ConfigureDatabase(this IServiceCollection services)
        {
            var connectionStringSettings = services.BuildServiceProvider().GetRequiredService<IOptions<ConnectionStringsSettings>>().Value;

            var provider = connectionStringSettings.Provider?.ToLower();

            if (provider == "sqlserver" || provider == "postgresql")
            {
                services.AddDbContext<RelationalDbContext>(options =>
                {
                    if (provider == "sqlserver")
                        options.UseSqlServer(connectionStringSettings.Database,
                            b => b.MigrationsAssembly(typeof(InjectionContainer).Assembly.FullName));
                    else
                        options.UseNpgsql(connectionStringSettings.Database,
                            b => b.MigrationsAssembly(typeof(InjectionContainer).Assembly.FullName));
                }, ServiceLifetime.Scoped);

                services.AddScoped<IDatabaseContext>(sp =>
                    sp.GetRequiredService<RelationalDbContext>());
            }
            else if (provider == "mongodb")
            {
                services.AddSingleton<IMongoClient>(sp =>
                    new MongoClient(connectionStringSettings.Database));

                services.AddScoped<IMongoDatabase>(sp =>
                {
                    var client = sp.GetRequiredService<IMongoClient>();
                    return client.GetDatabase(connectionStringSettings.MongoDbName);
                });

                services.AddScoped<IDatabaseContext, MongoDbContext>();
            }
            else
            {
                throw new Exception("Unsupported database provider!");
            }

            return services;
        }

        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}