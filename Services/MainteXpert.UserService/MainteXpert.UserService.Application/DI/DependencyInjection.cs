﻿namespace MainteXpert.UserService.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppInfrastructure(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddAutoMapper(assembly);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

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

        public static IServiceCollection AddLoggingConfigration(this IServiceCollection services, IConfiguration configuration)
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
