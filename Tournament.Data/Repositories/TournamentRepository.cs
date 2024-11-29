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

        private readonly TournamentApiContext _context;
        public TournamentRepository(TournamentApiContext context) 
        {
            _context = context;
        }

        public void Add(TournamentDetails tournament)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TournamentDetails>> GetAllAsync()
        {
            return await _context.TournamentDetails.ToListAsync();
        }

        public Task<TournamentDetails> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(TournamentDetails tournament)
        {
            throw new NotImplementedException();
        }

        public void Update(TournamentDetails tournament)
        {
            throw new NotImplementedException();
        }
    }
}
