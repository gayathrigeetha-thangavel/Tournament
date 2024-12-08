using Tournament.Core.Entites;

namespace Tournament.Core.Repositories
{
    public interface ITournamentRepository
    {
        public Task<IEnumerable<TournamentDetails>> GetAllAsync();
        public Task<TournamentDetails> GetAsync(int id);
        public Task<bool> AnyAsync(int id);

        public void Add(TournamentDetails tournament);
        public void Update(TournamentDetails tournament);
        public void Remove(TournamentDetails tournament);
    }
}
