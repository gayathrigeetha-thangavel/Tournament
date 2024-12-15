using Bogus;
using Humanizer;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entites;
using Tournament.Data.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Tournament.Api.Extensions
{
    public static class SeedData
    {
        public static async Task SeedDataAsync(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var serviceprovider = scope.ServiceProvider;
                var db = serviceprovider.GetRequiredService<TournamentApiContext>();

                await db.Database.MigrateAsync();
                if (await db.TournamentDetails.AnyAsync()) return;

                try
                {
                    var tournaments = GenerateTournament(4);
                    db.AddRange(tournaments);
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private static IEnumerable<TournamentDetails> GenerateTournament(int nrOfTournaments)
        {
            var faker = new Faker<TournamentDetails>("sv").Rules(static (f, t) =>
            {
                t.Title = f.Music.Genre() + "Championship";
                t.StartDate = f.Date.Future();
                
                t.Games = GenerateGames(f.Random.Int(min: 2, max: 5));
            });

            return faker.Generate(nrOfTournaments);
        }

        private static ICollection<Game> GenerateGames(int noOfGames)
        {
            var faker = new Faker<Game>("sv").Rules((f, g) =>
            {
                g.Title = f.Commerce.ProductName();
                g.Time = f.Date.Future();
                
            });

            return faker.Generate(noOfGames);
        }
    }
}
