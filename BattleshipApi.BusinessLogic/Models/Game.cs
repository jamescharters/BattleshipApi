using BattleshipApi.BusinessLogic.Interfaces;
using BattleshipApi.Common.Enums;
using BattleshipApi.Common.Models;

namespace BattleshipApi.BusinessLogic.Models;

public class Game
{
    private readonly IPlayerFactory playerFactory;

    public Guid GameId { get; private set; } = Guid.NewGuid();
    public List<Player> Players { get; set; } = new();

    public Game(IPlayerFactory playerFactory)
    {
        this.playerFactory = playerFactory;
    }

    public void Initialise(List<string> playerNames)
    {
        if (!playerNames.Any())
        {
            throw new ArgumentOutOfRangeException($"{nameof(playerNames)}");
        }

        Players.AddRange(playerNames.Select(_ => playerFactory.Create(_)));
    }

    public void Initialise(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName)) throw new ArgumentNullException($"{nameof(playerName)}");

        Players.Add(playerFactory.Create(playerName));
    }

    public FireResult FireAt(Guid playerId, Coordinate target)
    {
        var currentPlayer = GetPlayer(playerId);

        var vesselTile = currentPlayer.VesselBoard.TileAt(target);

        if (vesselTile == null)
        {
            // Thar be some wild shootin' there son...
            Console.WriteLine($"Target co-ordinate ({target.Row}, {target.Column}) is out of bounds!");

            return FireResult.OutOfBounds;
        }

        if (vesselTile.Type is TileType.Hit or TileType.Miss)
        {
            Console.WriteLine($"Target co-ordinate ({target.Row}, {target.Column}) has already been fired at!");

            return FireResult.AlreadyFiredAt;
        }

        currentPlayer.FireAt(target);

        if (currentPlayer.IsDefeated)
        {
            Console.WriteLine($"Captain {currentPlayer.Name} has been defeated!");
        }

        vesselTile.Type = vesselTile.Type == TileType.Vessel ? TileType.Hit : TileType.Miss;

        return vesselTile.Type == TileType.Hit ? FireResult.Hit : FireResult.Miss;
    }

    #region Private 
    public Player? GetPlayer(Guid playerId)
    {
        return Players.SingleOrDefault(_ => _.Id == playerId);
    }
    #endregion
}