namespace MainteXpert.AuthService.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppInfrastructure(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            AutoMapper.Extensions.Microsoft.DependencyInjection.ServiceCollectionExtensions.AddAutoMapper(services, assembly); // Explicitly specify the namespace
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ActivityGroupBehavior<,>));

            #region Configure Mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<AutoMapperMappingProfile>();
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            #endregion
            return services;
        }

        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            services.AddLogging(l => l.AddSerilog(logger));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            return services;
        }
    }
}
