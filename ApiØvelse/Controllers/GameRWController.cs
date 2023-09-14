using ApiØvelse.Models;
using ApiØvelse.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ApiØvelse.Controllers
{
    [Route("api1/[controller]")]
    [ApiController]
    public class GameRWController : ControllerBase
    {

        private readonly GameDict gameDict;
        public GameRWController(GameDict catDict)
        {
            this.gameDict = catDict;
        }

        
        //static Dictionary<int, Game> games = new Dictionary<int, Game>();

        [HttpGet("GetLatestGame")]
        public ActionResult<GameDTO> GetGame(int id)
        {
            if (gameDict.ContainsKey(id))
            {
                Game game = gameDict[id];
                GameDTO gameDTO = new GameDTO()
                {
                    Title = game.Title,
                    Genre = game.Genre,
                    ReleaseYear = game.ReleaseYear
                };
                return Ok(gameDTO);
            }
            return NotFound();
            
            
        }
        [HttpPost("PostGame")]
        public ActionResult PostGame(GameDTO gameDTO)
        {
            Game newGame = new Game()
            {
                Title = gameDTO.Title,
                Genre = gameDTO.Genre,
                ReleaseYear = gameDTO.ReleaseYear,
                CreatedAt = DateTime.UtcNow,
                Id = gameDict.Count + 1
            };

            gameDict.Add(newGame.Id, newGame);

            return CreatedAtAction(nameof(GetGame), new { id = newGame.Id }, null);
        }

        [HttpDelete("DeleteGame")]
        public ActionResult DeleteGame(int id)
        {
            foreach(var game in gameDict)
            {
                if(game.Key == id)
                {
                    gameDict.Remove(id);
                }
                return Ok(game);
            }
            return NotFound();

            //return CreatedAtAction(nameof(GetGame), new { id = newGame.Id }, null);
        }

    }
}
