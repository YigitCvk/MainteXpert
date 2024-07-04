

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

// Configuration for MongoDbSettings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection(nameof(MongoDbSettings)));

builder.Services.AddSingleton<IMongoDbSettings>(sp =>
    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString")));
builder.Services.AddControllers();
// Add IHttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MainteXpert.TpmService",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Yiğit ÇEVİK",
            Email = "me@yigitcevik.dev"
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = configuration["IdentityServerURL"];
    options.Audience = "resource_tpmservice";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("NormalPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "tpmservice_fullpermission");
    });
});
builder.Logging.ClearProviders(); // Standart logging yapılandırması
builder.Logging.AddConsole(); // Konsol loglarını ekler
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MainteXpert.TpmService v1"));
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors(builder =>
    builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod()
);

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
