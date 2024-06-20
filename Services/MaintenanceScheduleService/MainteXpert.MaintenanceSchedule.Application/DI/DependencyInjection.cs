namespace MainteXpert.MaintenanceSchedule.Application.DI
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
            #endregion
            return services;
        }

        public static IServiceCollection AddLoggingConfigration(this IServiceCollection services, IConfiguration configration)
        {

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configration)
                .CreateLogger();

            services.AddLogging(l => l.AddSerilog(logger));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            return services;
        }
    }
}
