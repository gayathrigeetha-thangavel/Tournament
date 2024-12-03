using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Api.Repositories;
using Tournament.Core;
using Tournament.Core.Entites;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {

        private readonly TournamentApiContext _dbContext;
        public TournamentRepository(TournamentApiContext context) 
        {
            _dbContext = context;
        }

        public void Add(TournamentDetails tournament)
        {
            _dbContext.TournamentDetails.Add(tournament);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return _dbContext.TournamentDetails.Any(e => e.Id == id);
        }

        public async Task<IEnumerable<TournamentDetails>> GetAllAsync()
        {
            return await _dbContext.TournamentDetails.ToListAsync();
        }

        public async Task<TournamentDetails> GetAsync(int id)
        {
            return await _dbContext.TournamentDetails.FindAsync(id);

        }

        public void Remove(TournamentDetails tournament)
        {
            _dbContext.TournamentDetails.Remove(tournament);
        }

        public void Update(TournamentDetails tournament)
        {
            _dbContext.Entry(tournament).State = EntityState.Modified;
        }
    }
}
