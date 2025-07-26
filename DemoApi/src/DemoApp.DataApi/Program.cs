
using Application.Framework;
using Application.Framework.Logging;
using Demo.InMemoryDb.Repository;

namespace Demo.DataApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Config logger
        var consoleLoggerOptions = builder.Configuration.GetSection("AppConsoleLogger").Get<AppConsoleLoggerOptions>();
        var inMemoryDbLoggerOptions = builder.Configuration.GetSection("InMemoryDbLogger").Get<InMemoryDbLoggerOptions>();
        builder.Logging
            .ClearProviders()
            .SetMinimumLevel(LogLevel.Information)
            .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Error);
        if (consoleLoggerOptions != null) builder.Logging.AddProvider(new AppConsoleLoggerProvider(consoleLoggerOptions));
        if (inMemoryDbLoggerOptions != null) builder.Logging.AddProvider(new InMemoryDbLoggerProvider(inMemoryDbLoggerOptions));
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        // Add services to the container.
        // builder.Services.AddControllers();
        builder.Services.AddControllersWithViews();

        builder.Services
            .AddInMemoryDbRepositoryService(builder.Configuration)
            .AddHealthCheckRepositoryService(builder.Configuration)
            .AddLoggerInMemoryDbRepositoryService(builder.Configuration);

        builder.Services
            .AddCors(options =>
            {
                options.AddPolicy("allowAny", policy =>
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                    );
                options.AddDefaultPolicy(policy =>
                    policy
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
                options.AddPolicy("myPolicy", policy =>
                    policy
                        .WithOrigins("https://localhost:44304", "http://localhost:4200")
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                    );
            });

        // Init inMemoryDb
        await DataSeedRepository.DataSeedAsync("DemoDb").ConfigureAwait(false);
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //}
       
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseHttpsRedirection();
        app.UseCors(p => p.WithOrigins("http://localhost:4200", "http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
        app.UseCors("allowAny");
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
