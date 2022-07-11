using BattleshipApi.Common.Enums;
using BattleshipApi.Common.Models;

namespace BattleshipApi.BusinessLogic.Models;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public VesselBoard VesselBoard { get; set; } = new();
    public List<Vessel> Vessels { get; set; } = new();
    public bool IsDefeated => Vessels.All(_ => _.IsDead);

    public Player(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName)) throw new ArgumentNullException($"{nameof(playerName)}");

        Id = Guid.NewGuid();
        Name = playerName;
    }

    public void AddVessel(Coordinate start, VesselOrientation vesselOrientation, Vessel vessel)
    {
        VesselBoard.AddVessel(start, vesselOrientation, vessel);

        Vessels.Add(vessel);
    }

    public void RemoveVessel(Guid vesselId)
    {
        var targetVessel = Vessels.Single(_ => _.Id == vesselId);

        VesselBoard.RemoveVessel(vesselId);

        Vessels.Remove(targetVessel);
    }

    public FireResult FireAt(Coordinate coordinate)
    {
        var tile = VesselBoard.TileAt(coordinate);

        switch (tile.Type)
        {
            case TileType.Hit:
            case TileType.Miss:
                {
                    return FireResult.AlreadyFiredAt;
                }
            case TileType.Vessel:
                {
                    tile.Occupant.AddDamage();

                    if (tile.Occupant.IsDead)
                    {
                        Console.WriteLine($"{tile.Occupant.Name} has been sunk!");
                    }

                    return FireResult.Hit;
                }
            default:
                {
                    return FireResult.Miss;
                }
        }
    }
}