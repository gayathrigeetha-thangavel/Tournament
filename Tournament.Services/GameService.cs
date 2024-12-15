using AutoMapper;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.DTO;
using Tournament.Core.Exceptions;
using Tournament.Core.IRepositories;


namespace Tournament.Services
{
    public class GameService : IGameService
    {

        private readonly IUnitOfWork uow;

        private readonly IMapper _mapper;

        public GameService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            uow = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GameDto>> GetGamesAsync()
        {
            var games = await uow.GameRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GameDto>>(games);
        }

        public async Task<GameDto> GetGameAsyncByID(int id)
        {
            var gameDetails = await uow.GameRepository.GetAsync(id);

            if (gameDetails == null)
            {
                throw new GameNotFoundException(id);
            }

            return _mapper.Map<GameDto>(gameDetails);
        }
    }
}
