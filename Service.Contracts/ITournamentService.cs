using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.DTO;

namespace Service.Contracts
{
    public interface ITournamentService 
    {
        Task<IEnumerable<TournamentDto>> GetTournamentsAsync();
        Task<TournamentDto> GetTournamentAsyncByID(int id);
    }
}
