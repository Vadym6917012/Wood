using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wood.Application.Common.Behaviours;
using Wood.Application.Common.Mappings;

namespace Wood.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehaviour<,>));

            return services;

        }
    }
}
