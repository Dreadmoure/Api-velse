using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiØvelse.Models;

namespace ApiØvelse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GameInteractor _context;

        public GamesController(GameInteractor context)
        {
            _context = context;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGameInfo()
        {
          if (_context.GameInfo == null)
          {
              return NotFound();
          }

            var gameList = await _context.GameInfo.ToListAsync();
            var gameDTOList = gameList.Select(game => new GameDTO
            {
                Title = game.Title,
                Genre = game.Genre,
                ReleaseYear = game.ReleaseYear
            }).ToList();
            return gameDTOList;
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(string id)
        {
          if (_context.GameInfo == null)
          {
              return NotFound();
          }
            var game = await _context.GameInfo.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(string id, Game game)
        {
            if (id != game.Title)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
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
        public async Task<ActionResult<Game>> PostGame([ FromBody] GameDTO game)
        {
            if (_context.GameInfo == null)
            {
                return Problem("Entity set 'GameInteractor.GameInfo'  is null.");
            }
            Game newGame = new Game()
            {
                Title = game.Title,
                Genre = game.Genre,
                ReleaseYear = game.ReleaseYear,
                CreatedAt = DateTime.Now
            };
            _context.GameInfo.Add(newGame);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetGame", new { id = newGame.Id }, newGame);

        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(string id)
        {
            if (_context.GameInfo == null)
            {
                return NotFound();
            }
            var game = await _context.GameInfo.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.GameInfo.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(string id)
        {
            return (_context.GameInfo?.Any(e => e.Title == id)).GetValueOrDefault();
        }
    }
}
