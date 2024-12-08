using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Repositories;

namespace Tournament.Services
{
    public class ServiceManager : IServiceManager
    {

        private readonly Lazy<ITournamentService> tournamentService;
        private readonly Lazy<IGameService> gameService;

        public ITournamentRepository TournamentService => throw new NotImplementedException();

        public IGameRepository GameService => throw new NotImplementedException();

        public ServiceManager(Lazy<ITournamentService> tournamentService, Lazy<IGameService> gameService)
        {
            this.tournamentService = tournamentService;
            this.gameService = gameService;
        
        }
    }
}
