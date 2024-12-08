using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.DTO;

namespace Tournament.Services
{
    public class TournamentService : ITournamentService
    {
        public Task<TournamentDto> GetCompanyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TournamentDto>> GetTournamentAsync()
        {
            throw new NotImplementedException();
        }
    }
}
