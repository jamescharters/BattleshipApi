using BattleshipApi.Common.Models;
using BattleshipApi.Core.Interfaces;

namespace BattleshipApi.Core.Models;

/// <summary>
/// Denotes a tile on the grid of tiles that make up a Battleship board
/// </summary>
public class Tile : IEquatable<Tile>
{
    public TileType Type { get; set; }
    public CartesianCoordinates CartesianCoordinateses { get; private set; }
    public ITileOccupant? Occupant { get; set; }

    public Tile(int row, int column, TileType type = TileType.Water)
    {
        Type = type;
        CartesianCoordinateses = new CartesianCoordinates(row, column);
    }

    public bool Equals(Tile? other)
    {
        if (other == null) return false;

        return Type == other.Type && CartesianCoordinateses.Row == other.CartesianCoordinateses.Row && CartesianCoordinateses.Column ==
            other.CartesianCoordinateses.Column;
    }
}