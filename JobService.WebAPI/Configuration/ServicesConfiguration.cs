using FluentValidation;
using FluentValidation.AspNetCore;
using JobService.ApplicationServices;
using JobService.Domain;
using JobService.WebAPI.Mappings;
using JobService.WebAPI.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobService.WebAPI.Configuration;

public static class ServicesConfiguration
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSettings(configuration)
            .AddInfrastructure()
            .AddScoped<IJobCostCalculatorService<Job>, JobCostCalculatorService>();
    }

    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<JobCostSettings>(configuration.GetSection(JobCostSettings.SectionName));
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<JobTotalRequestValidator>(ServiceLifetime.Singleton)
                .AddAutoMapper(typeof(MappingProfile));
    }
}