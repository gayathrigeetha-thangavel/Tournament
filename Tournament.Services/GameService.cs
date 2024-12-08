using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.DTO;

namespace Tournament.Services
{
    public class GameService : IGameService
    {
        public Task<IEnumerable<GameDto>> GetCompaniesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GameDto> GetCompanyAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
