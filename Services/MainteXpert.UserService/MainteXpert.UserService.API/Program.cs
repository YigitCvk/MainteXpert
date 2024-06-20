using MainteXpert.UserService.Application.DI;

var builder = WebApplication.CreateBuilder(args);

var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false);

IConfiguration config = configBuilder.Build();

builder.Services.AddCors();

var mongoDbSettings = config.GetSection("MongoDbSettings").Get<MongoDbSettings>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.Configure<MongoDbSettings>(config.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<IMongoDbSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

builder.Services.AddLoggingConfigration(config); // Ensure the method name matches exactly
builder.Services.AddAppInfrastructure(); // Ensure the method name matches exactly

builder.Services.AddControllers();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MainteXpert.UserService",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Yiğit ÇEVİK",
            Email = "me@yigitcevik.dev"
        }
    });
    c.AddSecurityDefinition(config["JWT:ProviderKey"], new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = config["JWT:ProviderKey"],
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = config["JWT:ProviderKey"]
                }
            },
            new string[] {}
        }
    });
});

SymmetricSecurityKey signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecretKey"]));
string authenticationProviderKey = config["JWT:ProviderKey"];
builder.Services.AddAuthentication(option => option.DefaultAuthenticateScheme = authenticationProviderKey)
    .AddJwtBearer(authenticationProviderKey, options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signInKey,
            ValidateIssuer = false,
            ValidIssuer = config["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = config["JWT:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            RequireExpirationTime = true
        };
    });

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("NormalPolicy", policy => policy.RequireClaim(ClaimTypes.Authentication, true.ToString()));
    //  opt.AddPolicy("ResetPolicy", policy => policy.RequireClaim(ClaimTypes.Authentication, false.ToString()));
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors(builder =>
    builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
);

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MainteXpert.User v1"));
app.Run();
