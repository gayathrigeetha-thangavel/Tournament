using Microsoft.EntityFrameworkCore;
using Tournament.Api.Extensions;
using Tournament.Data.Data;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers(); // previous controller settings

        builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
            .AddNewtonsoftJson()
            .AddXmlDataContractSerializerFormatters();

        // DB connection establishment
        var connectionString = builder.Configuration.GetConnectionString("TournamentApiContext") 
            ?? throw new InvalidOperationException("Connection string 'TournamentApiContext' not found.");

        builder.Services.AddDbContext<TournamentApiContext>(options =>
            options.UseSqlServer(connectionString));
        

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            await app.SeedDataAsync();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}