using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Tournament.Api.Extensions;
using Tournament.Data.Data;
using AutoMapper;



public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        //builder.Services.AddControllers(); // previous controller settings

        builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
            .AddNewtonsoftJson()
            //.AddXmlDataContractSerializerFormatters()
            .AddApplicationPart(typeof(AssemblyReference).Assembly);

        // DB connection establishment
        var connectionString = builder.Configuration.GetConnectionString("TournamentApiContext") 
            ?? throw new InvalidOperationException("Connection string 'TournamentApiContext' not found.");

        builder.Services.AddDbContext<TournamentApiContext>(options =>
            options.UseSqlServer(connectionString));

        //newly added service extension
        builder.Services.ConfigureServiceLayerServices();
        builder.Services.ConfigureRepositories();

        builder.Services.ConfigureCors();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //Auto mapper
        builder.Services.AddAutoMapper(typeof(TournamentMappings));


        //Unit of work call added to the service
        /*builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IServiceManager, ServiceManager>();
        builder.Services.AddScoped<ITournamentService, TournamentService>(); */

       

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            await app.SeedDataAsync();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("AllowAll");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    
}

