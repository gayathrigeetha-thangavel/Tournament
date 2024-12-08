using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Tournament.Core.Entites;
using Tournament.Core.Repositories;
using Tournament.Data.Data;
using Tournament.Core.DTO;

namespace Tournament.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentDetailsController : ControllerBase
    {
        private readonly TournamentApiContext _context;

        private readonly IUnitOfWork uow;

        private readonly IMapper _mapper;

        public TournamentDetailsController(TournamentApiContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            uow = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/TournamentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournamentDetails()
        {
            // var tournaments = await _context.TournamentDetails.ToListAsync();// direct action

            var tournaments = await uow.TournamentRepository.GetAllAsync();

            if (tournaments == null)
            {
                return NotFound(new { Message = $"Tournament not found." });
            }

            var tournamentDtos = _mapper.Map<IEnumerable<TournamentDto>>(tournaments);

            return Ok(tournamentDtos);
        }

        // GET: api/TournamentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetTournamentDetails(int id)
        {
            var tournamentDetails = await uow.TournamentRepository.GetAsync(id);

            if (tournamentDetails == null)
            {
                return NotFound(new { Message = $"Tournament with ID {id} not found." });
            }
            var tournamentDtos = _mapper.Map<TournamentDto>(tournamentDetails);

            return Ok(tournamentDtos);
        }

        // PUT: api/TournamentDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournamentDetails(int id, TournamentUpdateDto tournamentUpdateDto)
        {
            if (id != tournamentUpdateDto.Id)
            {
                return BadRequest(new { Message = "ID mismatch." });
            }

            //_context.Entry(tournamentDetails).State = EntityState.Modified; // direct action

            var existingTournament = await uow.TournamentRepository.GetAsync(id);
            if (existingTournament == null)
            {
                return NotFound(new { Message = $"Tournament with ID {id} not found." });
            }

            try
            {
                //await _context.SaveChangesAsync(); // direct action

                existingTournament.Title = tournamentUpdateDto.Title;
                existingTournament.StartDate = tournamentUpdateDto.StartDate;

                _mapper.Map(tournamentUpdateDto, existingTournament);

                uow.TournamentRepository.Update(existingTournament);
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (await TournamentDetailsExists(id) == false)
                {
                    return StatusCode(500, new { Message = "An error occurred while updating the tournament.", Details = ex.Message });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TournamentDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentDetails>> PostTournamentDetails(TournamentCreateDto tournamentCreateDto)
        {
            //_context.TournamentDetails.Add(tournamentDetails); //direct action
            //await _context.SaveChangesAsync();

            //Mapping DTO
            if (tournamentCreateDto == null)
            {
                return BadRequest(new { Message = "Invalid tournament data." });
            }

            var tournamentDetails = _mapper.Map<TournamentDetails>(tournamentCreateDto);

            uow.TournamentRepository.Add(tournamentDetails);
            await uow.CompleteAsync();

            return CreatedAtAction("GetTournamentDetails", new { id = tournamentDetails.Id }, tournamentDetails);
        }

        // DELETE: api/TournamentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournamentDetails(int id)
        {
            //var tournamentDetails = await _context.TournamentDetails.FindAsync(id); //direct action

            var tournamentDetails = await uow.TournamentRepository.GetAsync(id);
            if (tournamentDetails == null)
            {
                return NotFound();
            }

            //_context.TournamentDetails.Remove(tournamentDetails); //direct action
            //await _context.SaveChangesAsync();

            uow.TournamentRepository.Remove(tournamentDetails);
            await uow.CompleteAsync();

            return NoContent();
        }

        private Task<bool> TournamentDetailsExists(int id)
        {
            //return _context.TournamentDetails.Any(e => e.Id == id);

            return uow.TournamentRepository.AnyAsync(id);

        }

    }

}