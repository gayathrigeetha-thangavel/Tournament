using Tournament.Core.Entites;

namespace Tournament.Core.IRepositories
{
    public interface IGameRepository
    {
        public Task<IEnumerable<Game>> GetAllAsync();
        public Task<Game> GetAsync(int id);
        public Task<bool> AnyAsync(int id);

        public void Add(Game game);
        public void Update(Game game);
        public void Remove(Game game);
    }
}
