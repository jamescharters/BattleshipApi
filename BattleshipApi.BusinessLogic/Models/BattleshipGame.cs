using BattleshipApi.BusinessLogic.Exceptions;
using BattleshipApi.BusinessLogic.Factories;

namespace BattleshipApi.BusinessLogic.Models;

public class BattleshipGame
{
    private readonly IPlayerFactory playerFactory;

    public Guid GameId { get; private set; } = Guid.NewGuid();
    public List<Player> Players { get; set; } = new();

    public BattleshipGame(IPlayerFactory playerFactory)
    {
        this.playerFactory = playerFactory;
    }

    public void NewGame(List<string> playerNames)
    {
        if (!playerNames.Any())
        {
            throw new ArgumentOutOfRangeException($"{nameof(playerNames)}");
        }

        Players.AddRange(playerNames.Select(_ => playerFactory.Create(_)));
    }

    public void NewGame(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName)) throw new ArgumentNullException($"{nameof(playerName)}");

        Players.Add(playerFactory.Create(playerName));
    }

    public FireResult FireAt(Guid playerId, Coordinate target)
    {
        var currentPlayer = getPlayer(playerId);

        var vesselTile = currentPlayer.VesselBoard.TileAt(target);

        if (vesselTile == null)
        {
            // DEVNOTE: target is out of bounds? Thar be some wild shootin' there son...
            Console.WriteLine($"Target co-ordinate ({target.Row}, {target.Column}) is out of bounds!");

            return FireResult.OutOfBounds;
        }

        if (vesselTile.Type is TileType.Hit or TileType.Miss)
        {
            // DEVNOTE: this tile has already been fired upon
            Console.WriteLine($"Target co-ordinate ({target.Row}, {target.Column}) has already been fired at!");
            
            return FireResult.AlreadyFiredAt;
        }

        currentPlayer.FireAt(target);
        
        // if (vesselTile.Occupant != null)
        // {
        //     vesselTile.Occupant.AddDamage();   
        //     
        //     if (vesselTile.Occupant.IsDead)
        //     {
        //         Console.WriteLine($"Vessel {vesselTile.Occupant.Name} (captained by {currentPlayer.Name}) has been sunk!");
        //     }
        // }

        if (currentPlayer.IsLoser)
        {
            Console.WriteLine($"Captain {currentPlayer.Name} has been defeated!");
        }
        
        vesselTile.Type = vesselTile.Type == TileType.Vessel ? TileType.Hit : TileType.Miss;

        return vesselTile.Type == TileType.Hit ? FireResult.Hit : FireResult.Miss;
    }

    #region Private 
    private Player getPlayer(Guid playerId)
    {
        return Players.Single(_ => _.Id == playerId);
    }
    #endregion
}