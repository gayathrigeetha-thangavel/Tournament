using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.DTO;
using Tournament.Core.IRepositories;
using Tournament.Data.Data;
using Tournament.Data.Repositories;
using Tournament.Core.Exceptions;

namespace Tournament.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly IUnitOfWork uow;

        private readonly IMapper _mapper;

        public TournamentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TournamentDto>> GetTournamentsAsync()
        {
            var tournaments = await uow.TournamentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TournamentDto>>(tournaments);
        }

        public async Task<TournamentDto> GetTournamentAsyncByID(int id)
        {
            var tournamentDetails = await uow.TournamentRepository.GetAsync(id);

            if (tournamentDetails == null)
            {
                throw new TournamentNotFoundException(id);
            }

            return _mapper.Map<TournamentDto>(tournamentDetails);
        }
    }
}
