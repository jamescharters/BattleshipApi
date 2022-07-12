using BattleshipApi.Core.Interfaces;
using BattleshipApi.Common.Models;
using BattleshipApi.Models.Request;
using BattleshipApi.Models.Response;
using BattleshipApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;

namespace BattleshipApi.Controllers;

[ApiController]
[Route("[controller]/v1")]
public class BattleshipController : ControllerBase
{
    private readonly IGameRepository gameRepository;
    private readonly IGameFactory gameFactory;
    private readonly IVesselFactory vesselFactory;

    public BattleshipController(IGameRepository gameRepository, IGameFactory gameFactory, IVesselFactory vesselFactory)
    {
        this.gameRepository = gameRepository;
        this.gameFactory = gameFactory;
        this.vesselFactory = vesselFactory;
    }

    /// <summary>
    /// Create a new battleship game
    /// </summary>
    /// <param name="request">Parameters for the new game</param>
    /// <returns></returns>
    [HttpPost("game")]
    [SwaggerOperation("Create a new battleship game")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public IActionResult NewGame([FromBody] NewGameRequest request)
    {
        if (!ModelState.IsValid) return BadRequest();

        try
        {
            var newGame = gameFactory.Create(request.Players);

            gameRepository.Add(newGame);

            return Ok(new NewGameResponse
            {
                Id = newGame.GameId,
                Players = newGame.Players.Select(_ => new PlayerInformation {Id = _.Id, Name = _.Name})
            });
        }
        catch (Exception e)
        {
            return InternalServerError(e.Message);
        }
    }

    /// <summary>
    /// Add a vessel to a player's board
    /// </summary>
    /// <param name="gameId">The game id</param>
    /// <param name="playerId">The player id relative to the game</param>
    /// <param name="request">Parameters for the new vessel</param>
    /// <returns></returns>
    [HttpPost("game/{gameId:Guid}/player/{playerId:Guid}/vessel")]
    [SwaggerOperation("Add a vessel to a player's board")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public IActionResult AddVessel(Guid gameId, Guid playerId, [FromBody] AddVesselRequest request)
    {
        if (!ModelState.IsValid) return BadRequest();

        var game = gameRepository.Get(gameId);
        
        if (game == null)
        {
            return NotFound($"Game {gameId} not found!");
        }

        var player = game.GetPlayer(playerId);

        if (player == null)
        {
            return NotFound($"Player {playerId} not found!");
        }

        var newVessel = vesselFactory.Create(request.Size);

        try
        {
            player.AddVessel(new CartesianCoordinates(request.Row, request.Column), request.Orientation, newVessel);

            return Ok(new AddVesselResponse
            {
                Id = newVessel.Id,
                Damage = newVessel.Damage,
                Name = newVessel.Name,
                Size = request.Size
            });
        }
        catch (Exception e)
        {
            return InternalServerError(e.Message);
        }
    }

    /// <summary>
    /// Fire at a specific set of co-ordinates on the player's board
    /// </summary>
    /// <param name="gameId">The game id</param>
    /// <param name="playerId">The player id relative to the game</param>
    /// <param name="request">Targetting instructions for the shot</param>
    /// <returns></returns>
    [SwaggerOperation("Fire at a specific set of co-ordinates on the player's board")]
    [HttpPost("game/{gameId:Guid}/player/{playerId:Guid}/fire")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public IActionResult FireAt(Guid gameId, Guid playerId, [FromBody] FireAtRequest request)
    {
        if (!ModelState.IsValid) return BadRequest();

        var game = gameRepository.Get(gameId);

        if (game == null)
        {
            return NotFound($"Game {gameId} not found!");
        }

        var player = game.GetPlayer(playerId);

        if (player == null)
        {
            return NotFound($"Player {playerId} not found!");
        }

        try
        {
            var result = player.FireAt(request.Coordinates);

            return Ok(new FireAtResponse
            {
                Result = result.ToString("G")
            });
        }
        catch (Exception e)
        {
            return InternalServerError(e.Message);
        }
    }

    /// <summary>
    /// Dump a visual representation of the player's board
    /// </summary>
    /// <param name="gameId">The game id</param>
    /// <param name="playerId">The player id relative to the game</param>
    /// <returns></returns>
    [SwaggerOperation("Dump a visual representation of the player's board")]
    [HttpGet("game/{gameId:Guid}/player/{playerId:Guid}/board")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public IActionResult GetBoard(Guid gameId, Guid playerId)
    {
        // TODO: Perhaps some kind of token authorisation to protect?
        var game = gameRepository.Get(gameId);
        
        if (game == null)
        {
            return NotFound($"Game {gameId} not found!");
        }

        var player = game.GetPlayer(playerId);

        if (player == null)
        {
            return NotFound($"Player {playerId} not found!");
        }

        return Ok(player.VesselBoard.ToString());
    }
    
    #region Private
    private ObjectResult InternalServerError([ActionResultObjectValue] string message)
    {
        return new ObjectResult(new
        {
            Message = message
        })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
    #endregion
}