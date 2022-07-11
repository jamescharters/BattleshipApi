using BattleshipApi.BusinessLogic.Factories;

namespace BattleshipApi.BusinessLogic.Models;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public VesselBoard VesselBoard { get; set; } = new(new VesselFactory());
    public ShotBoard ShotBoard { get; set; } = new(new VesselFactory());
    public List<Vessel> Vessels { get; set; } = new();
    public bool IsLoser => Vessels.All(_ => _.IsDead);

    public Player(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName)) throw new ArgumentNullException($"{nameof(playerName)}");

        Id = Guid.NewGuid();
        Name = playerName;
    }

    public void FireAt(Coordinate coordinate)
    {
        var tile = VesselBoard.TileAt(coordinate);
        
        // 0. If target is not occupied... return false / miss
        tile.Occupant.AddDamage();

        if (tile.Occupant.IsDead)
        {
            Console.WriteLine($"{tile.Occupant.Name} has been sunk!");
        }
        
        // TODO: track on the shot board
    }
}