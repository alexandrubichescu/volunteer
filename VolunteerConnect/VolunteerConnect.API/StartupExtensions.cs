using VolunteerConnect.Api.Middleware;
using VolunteerConnect.Api.Services;
using VolunteerConnect.Application;
using VolunteerConnect.Application.Contracts;
using VolunteerConnect.Identity;
using VolunteerConnect.Infrastructure;
using VolunteerConnect.Persistance;


namespace VolunteerConnect.Api;
/// <summary>
/// Startup extensions for the web API application.
/// </summary>
public static class StartupExtensions
{
    /// <summary>
    /// Configure services.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplication ConfigureServices(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddPersistanceServices(builder.Configuration);
        builder.Services.AddIdentityServices(builder.Configuration);

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();

        builder.Services.AddControllers();

        builder.Services.AddCors(
            options => options.AddPolicy(
                "open",
                policy => policy.WithOrigins([builder.Configuration["ApiUrl"] ?? "https://localhost:7081"])
        .AllowAnyMethod()
        .SetIsOriginAllowed(pol => true)
        .AllowAnyHeader()
        .AllowCredentials()));

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        return builder.Build();
    }
    /// <summary>
    /// Configure pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {

        app.UseCors("open");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
                        }

        app.UseCustomExceptionHandler();
        app.UseAuthorization();

        app.UseHttpsRedirection();
        app.MapControllers();
        // Seed roles and admin user
        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            serviceProvider.SeedIdentityData().Wait(); // Call the seeder
        }

        return app;
    }

}
