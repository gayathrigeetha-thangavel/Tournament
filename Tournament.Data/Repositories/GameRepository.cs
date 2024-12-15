using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entites;
using Tournament.Core.IRepositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        public readonly TournamentApiContext _dbContext;
        public GameRepository(TournamentApiContext context) 
        {
            _dbContext = context;
        }

        public void Add(Game game)
        {
            _dbContext.Games.Add(game);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return _dbContext.Games.Any(e => e.Id == id);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _dbContext.Games.ToListAsync();
        }

        public async Task<Game> GetAsync(int id)
        {
            return await _dbContext.Games.FindAsync(id);
        }

        public void Remove(Game game)
        {
            _dbContext.Games.Remove(game);
        }

        public void Update(Game game)
        {
            _dbContext.Entry(game).State = EntityState.Modified;
        }
    }
}
