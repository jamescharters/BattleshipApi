using BattleshipApi.BusinessLogic.Factories;
using BattleshipApi.Models.Request;
using BattleshipApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BattleshipController : ControllerBase
{
    private readonly IGameRepository gameRepository;
    private readonly IGameFactory gameFactory;

    public BattleshipController(IGameRepository gameRepository, IGameFactory gameFactory)
    {
        this.gameRepository = gameRepository;
        this.gameFactory = gameFactory;
    }

    [HttpPost("game")]
    public IActionResult NewGame([FromBody] NewGameRequest request)
    {
        // TODO: Perhaps some kind of token authorisation to protect?

        if (!ModelState.IsValid) return BadRequest();

        var newGame = gameFactory.Create(request.Players);
        
        gameRepository.Add(newGame);

        return Ok(newGame.GameId);
    }

    [HttpPost]
    public IActionResult FireAt([FromBody] FireAtRequest request)
    {
        // // TODO: Perhaps some kind of token authorisation to protect?
        //
        // if (!ModelState.IsValid) return BadRequest();
        //
        // game.FireAtTarget(request.PlayerId, request.Coordinates);
        //
        // // TODO: indicate hit or miss

        return Ok();
    }

    [HttpGet("game/{gameId}/board")]
    public IActionResult GetBoard(Guid gameId, Guid playerId)
    {
        // TODO: Perhaps some kind of token authorisation to protect?
        var game = gameRepository.Get(gameId);

        if (game == null)
        {
            return BadRequest();
        }
        
        // TODO: method to get a player
        var player = game.Players.Single(_ => _.Id == playerId);

        return Ok(player.VesselBoard.ToString());
    }

    [HttpGet("game/{gameId}/players")]
    public IActionResult GetPlayers(Guid gameId)
    {
        // TODO: Perhaps some kind of token authorisation to protect?
        var game = gameRepository.Get(gameId);

        if (game == null)
        {
            return BadRequest();
        }
        
        return Ok(game.Players.Select(_ => new {_.Id, _.Name}));
    }
}