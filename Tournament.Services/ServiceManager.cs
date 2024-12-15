using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.IRepositories;

namespace Tournament.Services
{
    public class ServiceManager : IServiceManager
    {

        private readonly Lazy<ITournamentService> tournamentService;
        private readonly Lazy<IGameService> gameService;

        

        public ServiceManager(Lazy<ITournamentService> tournamentService, Lazy<IGameService> gameService)
        {
            this.tournamentService = tournamentService;
            this.gameService = gameService;
        
        }

        public ITournamentService TournamentService => tournamentService.Value;

        public IGameService GameService => gameService.Value;
    }
}
