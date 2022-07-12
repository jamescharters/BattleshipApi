using BattleshipApi.Common.Enums;
using BattleshipApi.Common.Exceptions;
using BattleshipApi.Common.Models;
using BattleshipApi.Core.Interfaces;

namespace BattleshipApi.Core.Models;

public class Game
{
    private readonly IPlayerFactory playerFactory;

    public Guid GameId { get; private set; } = Guid.NewGuid();
    public List<Player> Players { get; set; } = new();

    public Game(IPlayerFactory playerFactory)
    {
        this.playerFactory = playerFactory;
    }

    public void Initialise(params string[] playerNames)
    {
        if (playerNames == null || !playerNames.Any())
        {
            throw new ArgumentNullException($"{nameof(playerNames)} does not contain any elements!");
        }

        Players.AddRange(playerNames.Select(_ => playerFactory.Create(_)));
    }

    public void Initialise(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName)) throw new ArgumentNullException($"{nameof(playerName)}");

        Players.Add(playerFactory.Create(playerName));
    }

    public FireResult FireAt(Guid playerId, CartesianCoordinates target)
    {
        var currentPlayer = GetPlayer(playerId);

        if (currentPlayer == null)
        {
            throw new InvalidPlayerException($"Player {playerId} does not exist!");
        }

        var result = currentPlayer.FireAt(target);

        if (currentPlayer.IsDefeated)
        {
            Console.WriteLine($"Captain {currentPlayer.Name} has been defeated!");
        }

        return result;
    }

    #region Private 
    public Player? GetPlayer(Guid playerId)
    {
        return Players.SingleOrDefault(_ => _.Id == playerId);
    }
    #endregion
}