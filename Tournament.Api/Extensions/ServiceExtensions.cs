using Service.Contracts;
using Tournament.Core.IRepositories;
using Tournament.Data.Repositories;
using Tournament.Services;

namespace Tournament.Api.Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(builder =>
            {
                builder.AddPolicy("AllowAll", p =>
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    );
            });
        }
        public static void ConfigureServiceLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<IGameService, GameService>();
            
            services.AddLazy<ITournamentService>();
            services.AddLazy<IGameService>();
            
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITournamentRepository, TournamentRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
           
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddLazy<ITournamentRepository>();
            services.AddLazy<IGameRepository>();
            
        }


    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLazy<TService>(this IServiceCollection services) where TService : class
        {
            services.AddScoped(provider => new Lazy<TService>(() => provider.GetRequiredService<TService>()));
            return services;
        }
    }
}
