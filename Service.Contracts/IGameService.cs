using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.DTO;

namespace Service.Contracts
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetGamesAsync();
        Task<GameDto> GetGameAsyncByID(int id);
    }
}
