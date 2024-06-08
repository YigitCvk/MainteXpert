var configBuilder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false)
             ;


IConfiguration config = configBuilder.Build();


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.Configure<MongoDbSettings>(config.GetSection("MongoDbSettings"));
        services.AddSingleton<IMongoDbSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
        .CreateLogger();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


        services.AddLogging(l => l.AddSerilog(logger));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        services.AddTransient(typeof(IMongoRepository<>), typeof(MongoRepository<>));


        #region RabbitMQ

        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();
            var factory = new ConnectionFactory()
            {
                HostName = config["EventBus:HostName"]
            };
            if (!string.IsNullOrWhiteSpace(config["EventBus:UserName"]))
            {
                factory.UserName = config["EventBus:UserName"];
            }
            if (!string.IsNullOrWhiteSpace(config["EventBus:Password"]))
            {
                factory.Password = config["EventBus:Password"];
            }
            var retryCount = 5;
            if (!string.IsNullOrWhiteSpace(config["EventBus:RetryCount"]))
            {
                retryCount = int.Parse(config["EventBus:RetryCount"]);
            }
            return new RabbitMQPersistentConnection(factory, retryCount, logger);
        });
        services.AddSingleton<EventBusProducer>();

        #endregion




    })
    .UseWindowsService()
    .Build();

host.Run();