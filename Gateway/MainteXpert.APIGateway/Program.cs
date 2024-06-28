var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("IdentityApiKey", options =>
    {
        options.Authority = builder.Configuration["IdentityServerURL"];
        options.Audience = "resource_gateway";
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddOcelot();

var app = builder.Build();

app.UseAuthentication();
app.UseOcelot().Wait();

app.Run();
