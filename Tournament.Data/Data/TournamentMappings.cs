using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.DTO;
using Tournament.Core.Entites;

namespace Tournament.Data.Data
{
    public class TournamentMappings : Profile
    {
        public TournamentMappings() 
        {
            //Tournament DTO
            CreateMap<TournamentDetails, TournamentDto>().ReverseMap();
            CreateMap<TournamentDetails, TournamentCreateDto>().ReverseMap();
            CreateMap<TournamentUpdateDto, TournamentDetails>().ReverseMap();

            //Game DTO
            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<Game, GameCreateDto>().ReverseMap();
            CreateMap<Game, GameUpdateDto>().ReverseMap();
        }
        
    }
}
