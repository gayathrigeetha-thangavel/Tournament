using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entites;

namespace Tournament.Data.Data
{
    public class TournamentApiContext : DbContext
    {
        public TournamentApiContext(DbContextOptions<TournamentApiContext> options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<TournamentDetails> TournamentDetails { get; set; }
    }
}
