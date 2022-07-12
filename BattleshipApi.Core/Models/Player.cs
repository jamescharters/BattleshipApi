using BattleshipApi.Common.Enums;
using BattleshipApi.Common.Models;

namespace BattleshipApi.Core.Models;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public VesselBoard VesselBoard { get; set; } = new();
    public List<Vessel> Vessels { get; set; } = new();
    public bool IsDefeated => Vessels.Any() && Vessels.All(_ => _.IsDead);

    public Player(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName)) throw new ArgumentNullException($"{nameof(playerName)}");

        Id = Guid.NewGuid();
        Name = playerName;
    }

    public void AddVessel(CartesianCoordinates start, VesselOrientation vesselOrientation, Vessel vessel)
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

    public FireResult FireAt(CartesianCoordinates cartesianCoordinates)
    {
        var tile = VesselBoard.TileAt(cartesianCoordinates.Row, cartesianCoordinates.Column);

        if (tile == null)
            return FireResult.OutOfBounds;

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
                    Console.WriteLine($"Vessel {tile.Occupant.Name} (captained by player {Name}) has been sunk!");
                }

                tile.Type = TileType.Hit;

                return FireResult.Hit;
            }
            default:
            {
                tile.Type = TileType.Miss;

                return FireResult.Miss;
            }
        }
    }
}