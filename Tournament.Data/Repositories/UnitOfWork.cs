using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //dbcontext
        public readonly TournamentApiContext _dbContext;

        //repo
        public ITournamentRepository TournamentRepository { get; }
        public IGameRepository GameRepository { get; }


        //constructor
        public UnitOfWork(TournamentApiContext context) 
        {
            _dbContext = context;
            TournamentRepository = new TournamentRepository(_dbContext);
            GameRepository = new GameRepository(_dbContext);
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
