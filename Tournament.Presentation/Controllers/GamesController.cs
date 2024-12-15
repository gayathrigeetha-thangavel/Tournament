using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Tournament.Core.DTO;
using Tournament.Core.Entites;
using Tournament.Core.IRepositories;
using Tournament.Data.Data;

namespace Tournament.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly TournamentApiContext _context;

        private readonly IUnitOfWork uow;

        private readonly IMapper _mapper;

        private readonly IServiceManager serviceManager;

        /*public GamesController(TournamentApiContext context, IUnitOfWork unitOfWork, IMapper mapper)
         {
             _context = context;
             uow = unitOfWork;
             _mapper = mapper;
         }*/

        public GamesController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
        {
            //var games = await _context.Games.ToListAsync(); //Old version

            /* var games = await uow.GameRepository.GetAllAsync();

            if (games == null)
            {
                return NotFound(new { Message = $"Games not found." });
            } */

            var gameDtos = await serviceManager.GameService.GetGamesAsync();

            if (gameDtos == null)
            {
                return NotFound(new { Message = $"Tournament not found." });
            }
            

            return Ok(gameDtos);
        }

        

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            // var game = await _context.Games.FindAsync(id);//Old version

            /*var game = await uow.GameRepository.GetAsync(id);

            if (game == null)
            {
                return NotFound(new { Message = $"Game with ID {id} not found." });
            }

            var gameDtos = _mapper.Map<GameDto>(game);
            gameDtos.StartDate = game.Time;

            return Ok(gameDtos);*/

            return Ok(await serviceManager.GameService.GetGameAsyncByID(id));
        }


        /* before restructuring codes
        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameUpdateDto gameUpdateDto)
        {
            if (id != gameUpdateDto.Id)
            {
                return BadRequest();
            }

            //_context.Entry(game).State = EntityState.Modified; //Old version

            var existingGame = await uow.GameRepository.GetAsync(id);
            if (existingGame == null)
            {
                return NotFound(new { Message = $"Game with ID {id} not found." });
            }

            try
            {
                //await _context.SaveChangesAsync();//Old version
                existingGame.Title = gameUpdateDto.Title;
                existingGame.Time = gameUpdateDto.StartDate;
                existingGame.TournamentDetailsId = gameUpdateDto.TournamentDetailsId;

                _mapper.Map(gameUpdateDto, existingGame);

                uow.GameRepository.Update(existingGame);
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (await GameExists(id) == false)
                {
                    return StatusCode(500, new { Message = "An error occurred while updating the game.", Details = ex.Message });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(GameCreateDto gameCreateDto)
        {
            //_context.Games.Add(game);
            //await _context.SaveChangesAsync(); //Old verison
            //Mapping DTO
            if (gameCreateDto == null)
            {
                return BadRequest(new { Message = "Invalid tournament data." });
            }

            var game = _mapper.Map<Game>(gameCreateDto);

            uow.GameRepository.Add(game);
            await uow.CompleteAsync();

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            //var game = await _context.Games.FindAsync(id);

            var game = await uow.GameRepository.GetAsync(id);
            if (game == null)
            {
                return NotFound(new { Message = $"Game with ID {id} not found." });
            }

            //_context.Games.Remove(game);
            //await _context.SaveChangesAsync(); //Old version

            uow.GameRepository.Remove(game);
            await uow.CompleteAsync();

            return NoContent();
        }

        private Task<bool> GameExists(int id)
        {
            //return _context.Games.Any(e => e.Id == id); // Old version

            return uow.GameRepository.AnyAsync(id);
        }
        */
    }
}
