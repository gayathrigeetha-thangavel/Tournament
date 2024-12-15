using Tournament.Core.IRepositories;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        ITournamentService TournamentService { get; }
        IGameService GameService { get; }
    }
}
