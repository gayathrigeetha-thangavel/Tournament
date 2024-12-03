using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entites;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly TournamentApiContext _context;

        private readonly IUnitOfWork uow;

        public GamesController(TournamentApiContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            uow = unitOfWork;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            //var games = await _context.Games.ToListAsync(); //Old version

            var games = await uow.GameRepository.GetAllAsync();

            if (games == null)
            {
                return NotFound(new { Message = $"Games not found." });
            }
            return Ok(games); 
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            // var game = await _context.Games.FindAsync(id);//Old version

            var game = await uow.GameRepository.GetAsync(id);

            if (game == null)
            {
                return NotFound(new { Message = $"Game with ID {id} not found." });
            }
            return game;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.Id)
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
                existingGame.Title = game.Title;
                existingGame.Time = game.Time;
                existingGame.TournamentDetailsId = game.TournamentDetailsId;

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
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            //_context.Games.Add(game);
            //await _context.SaveChangesAsync(); //Old verison

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
    }
}
